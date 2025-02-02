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

    /*
    private Dictionary<string, Vector2> teleportPositions = new Dictionary<string, Vector2>()
    {
        { "home", new Vector2(-80, -4) },
        { "Backhome", new Vector2(-9.5f, -4.5f) },
        { "Circus", new Vector2(101, -8) },
        { "BackCircus", new Vector2(18f, 23f) },
        { "CuircusTent", new Vector2(127, -11) },
        { "BackCuircusTent", new Vector2(18f, 23f) }
    };

    private Dictionary<string , string> interaction = new Dictionary<string, string>()
    {
         { "clothes", "옷 사이에 세미가 숨겨둔 돈이 있다." },
          { "desk", "세미의 일기장이있다." }

    };
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (teleportPositions.TryGetValue(collision.collider.tag, out Vector2 newPosition))
        {
            transform.position = newPosition;
        }
        if (interaction.TryGetValue(collision.collider.tag, out string naration))
        {
            InteractionNarration.instance.Narration(naration);

        }
    }

    */
  
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
                    // 위치 변경 (텔레포트가 필요한 경우)
                    if (interaction.position != Vector2.zero)
                    {
                        transform.position = interaction.position;
                    }

                    // 대사 출력 (있는 경우만)
                    if (!string.IsNullOrEmpty(interaction.narration))
                    {
                        InteractionNarration.instance.Narration(interaction.narration, interaction.checbox);
                }

                    break; // 찾았으면 더 이상 반복할 필요 없음
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


     