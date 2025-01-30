using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
  [SerializeField] private float speed = 5f;
    Vector2 direction;

    Rigidbody2D rigid;

    Camera cam;

    void Awake()
    {
        cam = FindAnyObjectByType<Camera>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -17f); ;

        // 키 입력 처리
        if (Input.GetKey(KeyCode.W))
            direction = Vector2.up;
        else if (Input.GetKey(KeyCode.S))
            direction = Vector2.down;
        else if (Input.GetKey(KeyCode.A))
            direction = Vector2.left;
        else if (Input.GetKey(KeyCode.D))
            direction = Vector2.right;
        else
            direction = Vector2.zero; // 키를 떼면 멈춤


    }

    void FixedUpdate()
    {
        // 이동 처리
        rigid.velocity = direction * speed * Time.deltaTime;

        // 방향에 따른 스프라이트 반전
        if (direction.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction.x), 1f, 1f);
        }
    }

    private Dictionary<string, Vector2> teleportPositions = new Dictionary<string, Vector2>()
    {
        { "home", new Vector2(-80, -4) },
        { "Backhome", new Vector2(-9.5f, -4.5f) },
        { "Circus", new Vector2(101, -8) },
        { "BackCircus", new Vector2(18f, 23f) },
        { "CuircusTent", new Vector2(127, -11) },
        { "BackCuircusTent", new Vector2(18f, 23f) }
    };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (teleportPositions.TryGetValue(collision.collider.tag, out Vector2 newPosition))
        {
            transform.position = newPosition;
        }
    }
}