using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    public Transform[] targets; // 이동할 목표 지점들
    public float moveSpeed = 2f; // 이동 속도
    public Transform player; // 플레이어

    private int currentTargetIndex = 0; // 현재 목표 인덱스
    private bool isMoving = false; // 이동 중인지 체크
    private bool onMove = false; // 플레이어가 근처에 있는지 체크
    Animator moveAni;

    private void Awake()
    {
        moveAni = GetComponent<Animator>(); 
    }

    void Update()
    {
        // 플레이어가 근처에 있고, 이동 중이 아닐 때만 이동 시작
        if (targets.Length > 0 && onMove && !isMoving)
        {
            StartCoroutine(MoveToNextTarget());

            
        }
    }

    IEnumerator MoveToNextTarget()
    {
        isMoving = true; // 이동 시작

        while (currentTargetIndex < targets.Length)
        {
            Transform currentTarget = targets[currentTargetIndex];

            // ? 애니메이션 인덱스 적용
            moveAni.SetInteger("MoveIndex", currentTargetIndex);

            // 목표 지점까지 이동
            while (Vector2.Distance(transform.position, currentTarget.position) > 0.1f)
            {
                if (!onMove)
                {
                    moveAni.SetInteger("MoveIndex", 0); // 대기 애니메이션으로
                    isMoving = false;
                    yield break;
                }

                transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // 도착 시 애니메이션 멈춤
            moveAni.SetInteger("MoveIndex", 0); // 대기

            // 텔레포트 체크
            Collider2D targetCollider = currentTarget.GetComponent<Collider2D>();
            if (targetCollider != null && targetCollider.CompareTag("telaport"))
            {
                yield return new WaitForSeconds(0.5f); // 순간이동 전 대기

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
        // 플레이어가 근처에 있을 때만 이동 시작
        if (collision.CompareTag("Player"))
        {
            onMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어가 멀어지면 이동 중지
        if (collision.CompareTag("Player"))
        {
            onMove = false;
        }
    }
}

