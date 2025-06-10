using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour
{
    public Transform[] targets;        // 이동할 목표 지점들
    public float moveSpeed = 2f;       // 이동 속도
    public Transform player;           // 플레이어

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

            // 이동 방향 계산
            Vector2 direction = (currentTarget.position - transform.position).normalized;

            // 방향을 정수형으로 정제 (가장 큰 축만 사용)
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
                    moveAni.SetBool("IsMoving", false); // 애니메이션 중지
                    isMoving = false;
                    yield break;
                }

                transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // 도착 시 애니메이션 중지
            moveAni.SetBool("IsMoving", false);

            // 텔레포트 체크
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
