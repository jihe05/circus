using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour
{
    public Transform[] targets;        // �̵��� ��ǥ ������
    public float moveSpeed = 2f;       // �̵� �ӵ�
    public Transform player;           // �÷��̾�

    private int currentTargetIndex = 0;
    private bool isMoving = false;
    private bool onMove = false;
    private Animator moveAni;
    private Collider2D playerChackCol;

    private void Awake()
    {
        playerChackCol = GetComponent<Collider2D>();
        moveAni = GetComponent<Animator>();
    }

    void Update()
    {
        if (targets.Length > 0 && onMove && !isMoving)
        {
            StartCoroutine(MoveToNextTarget());
        }
    }

    IEnumerator MoveToNextTarget()
    {
        isMoving = true;

        while (currentTargetIndex < targets.Length)
        {
            Transform currentTarget = targets[currentTargetIndex];

            // �̵� ���� ���
            Vector2 direction = (currentTarget.position - transform.position).normalized;

            // ������ ���������� ���� (���� ū �ุ ���)
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                direction = new Vector2(Mathf.Sign(direction.x), 0);
            }
            else
            {
                direction = new Vector2(0, Mathf.Sign(direction.y));
            }

            moveAni.SetFloat("MoveX", direction.x);
            moveAni.SetFloat("MoveY", direction.y);
            moveAni.SetBool("IsMoving", true);

            while (Vector2.Distance(transform.position, currentTarget.position) > 0.1f)
            {
                if (!onMove)
                {
                    moveAni.SetBool("IsMoving", false); // �ִϸ��̼� ����
                    isMoving = false;
                    yield break;
                }

                transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // ���� �� �ִϸ��̼� ����
            moveAni.SetBool("IsMoving", false);

            // �ڷ���Ʈ üũ
            Collider2D targetCollider = currentTarget.GetComponent<Collider2D>();
            if (targetCollider != null && targetCollider.CompareTag("telaport"))
            {
                yield return new WaitForSeconds(0.5f);
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
        if (collision.CompareTag("Player"))
        {
            onMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onMove = false;
        }
    }
}
