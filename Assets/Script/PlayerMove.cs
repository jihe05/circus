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

        // Ű �Է� ó��
        if (Input.GetKey(KeyCode.W))
            direction = Vector2.up;
        else if (Input.GetKey(KeyCode.S))
            direction = Vector2.down;
        else if (Input.GetKey(KeyCode.A))
            direction = Vector2.left;
        else if (Input.GetKey(KeyCode.D))
            direction = Vector2.right;
        else
            direction = Vector2.zero; // Ű�� ���� ����


    }

    void FixedUpdate()
    {
        // �̵� ó��
        rigid.velocity = direction * speed * Time.deltaTime;

        // ���⿡ ���� ��������Ʈ ����
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
         { "clothes", "�� ���̿� ���̰� ���ܵ� ���� �ִ�." },
          { "desk", "������ �ϱ������ִ�." }

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
           new InteractionData { tag = "clothes", narration = "�� ���̿� ���̰� ���ܵ� ���� �ִ�." , checbox = "���� �����ðڽ��ϱ�?"},
           new InteractionData { tag = "desk", narration = "������ �ϱ����� �ִ�." , checbox = "�ϱ����� �����ðڽ��ϱ�?"}
        };

        private void OnCollisionEnter2D(Collision2D collision)
        {
            string collidedTag = collision.collider.tag;

            foreach (var interaction in interactions)
            {
                if (interaction.tag == collidedTag)
                {
                    // ��ġ ���� (�ڷ���Ʈ�� �ʿ��� ���)
                    if (interaction.position != Vector2.zero)
                    {
                        transform.position = interaction.position;
                    }

                    // ��� ��� (�ִ� ��츸)
                    if (!string.IsNullOrEmpty(interaction.narration))
                    {
                        InteractionNarration.instance.Narration(interaction.narration, interaction.checbox);
                }

                    break; // ã������ �� �̻� �ݺ��� �ʿ� ����
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


     