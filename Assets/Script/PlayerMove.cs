using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
   [SerializeField] private float speed = 5f;
    Vector2 direction;
   

    Rigidbody2D rigid;

    Camera cam;
    Animator moveAni;

    private enum PlayerState { Idle = 0, Up = 1, Down = 2, Side = 3 }
    private PlayerState currentState = PlayerState.Idle;

    void Awake()
    {
        cam = FindAnyObjectByType<Camera>();
        rigid = GetComponent<Rigidbody2D>();
        moveAni = GetComponent<Animator>();

    }

    void Update()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -17f); ;
        PlayerState newState = PlayerState.Idle;

        // 키 입력 처리
        if (Input.GetKey(KeyCode.W))
        {
            direction = Vector2.up;
            newState = PlayerState.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = Vector2.down;
            newState = PlayerState.Down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
            newState = PlayerState.Side;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
            newState = PlayerState.Side;
        }
        else
        {
            direction = Vector2.zero;
        }

        if (newState != currentState)
        {
            moveAni.SetInteger("State", (int)newState);
            currentState = newState;
        }
    }

    void FixedUpdate()
    {
        // 이동 처리
        rigid.velocity = direction * speed;

        if (direction.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(-direction.x), 1f, 1f);
        }
    }

        public InteractionData[] interactions = new InteractionData[]
        {
           new InteractionData { tag = "home", position = new Vector2(-80, -4) },
           new InteractionData { tag = "Backhome", position = new Vector2(-9.5f, -4.5f) },
           new InteractionData { tag = "Circus", position = new Vector2(101, -8) },
           new InteractionData { tag = "BackCircus", position = new Vector2(18f, 23f)},
           new InteractionData { tag = "clothes", narration = "옷 사이에 세미가 숨겨둔 돈이 있다." , checbox = "돈을 꺼내시겠습니까?"},
           new InteractionData { tag = "desk", narration = "세미의 일기장이 있다." , checbox = "일기장을 읽으시겠습니까?"}
        };

        private void OnCollisionEnter2D(Collision2D collision)
        {
            string collidedTag = collision.collider.tag;

            foreach (var interaction in interactions)
            {
                if (interaction.tag == collidedTag)
                {
                    if (interaction.position != Vector2.zero)
                    {
                        transform.position = interaction.position;
                    }

                    if (!string.IsNullOrEmpty(interaction.narration))
                    {
                        InteractionNarration.instance.Narration(interaction.narration, interaction.checbox);
                    }

                    break;
                }
            }
        }

    }


  public class InteractionData
  {
    public string tag;        
    public Vector2 position;  
    public string narration;
    public string checbox;
}


     