using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    public Transform[] targets; // �̵��� ��ǥ ������
    public float moveSpeed = 2f; // �̵� �ӵ�
    public Transform player; // �÷��̾�

    private int currentTargetIndex = 0; // ���� ��ǥ �ε���
    private bool isMoving = false; // �̵� ������ üũ
    private bool onMove = false; // �÷��̾ ��ó�� �ִ��� üũ
    Animator moveAni;

    private void Awake()
    {
        moveAni = GetComponent<Animator>(); 
    }

    void Update()
    {
        // �÷��̾ ��ó�� �ְ�, �̵� ���� �ƴ� ���� �̵� ����
        if (targets.Length > 0 && onMove && !isMoving)
        {
            StartCoroutine(MoveToNextTarget());

            
        }
    }

    IEnumerator MoveToNextTarget()
    {
        isMoving = true; // �̵� ����

        while (currentTargetIndex < targets.Length)
        {
            Transform currentTarget = targets[currentTargetIndex];

            // ? �ִϸ��̼� �ε��� ����
            moveAni.SetInteger("MoveIndex", currentTargetIndex);

            // ��ǥ �������� �̵�
            while (Vector2.Distance(transform.position, currentTarget.position) > 0.1f)
            {
                if (!onMove)
                {
                    moveAni.SetInteger("MoveIndex", 0); // ��� �ִϸ��̼�����
                    isMoving = false;
                    yield break;
                }

                transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // ���� �� �ִϸ��̼� ����
            moveAni.SetInteger("MoveIndex", 0); // ���

            // �ڷ���Ʈ üũ
            Collider2D targetCollider = currentTarget.GetComponent<Collider2D>();
            if (targetCollider != null && targetCollider.CompareTag("telaport"))
            {
                yield return new WaitForSeconds(0.5f); // �����̵� �� ���

                if (currentTargetIndex + 1 < targets.Length)
                {
                    transform.position = targets[currentTargetIndex + 1].position;
                    currentTargetIndex++;
                }
            }
            else
            {
                currentTargetIndex++;
            }
        }

        isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾ ��ó�� ���� ���� �̵� ����
        if (collision.CompareTag("Player"))
        {
            onMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �÷��̾ �־����� �̵� ����
        if (collision.CompareTag("Player"))
        {
            onMove = false;
        }
    }
}

