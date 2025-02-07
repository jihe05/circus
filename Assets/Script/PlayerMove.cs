using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    float h;
    float v;
    bool isHorizontal = false;

    Rigidbody2D rigid;
  
    Animator moveAni;



    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        moveAni = GetComponent<Animator>();
    }

    private void Update()
    {

        //������ ��
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //���Ʒ� üũ
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        //isHorizontal üƮ ������
        if (hDown)
            isHorizontal = true;
        else if (vDown)
            isHorizontal = false;
        else if(hUp || vUp)
            isHorizontal = h != 0;

        //�ִϸ��̼�
        if (moveAni.GetInteger("InputH") != h){
            moveAni.SetBool("isChange", true);
            moveAni.SetInteger("InputH", (int)h);
        }
        else if (moveAni.GetInteger("InputV") != v){
            moveAni.SetBool("isChange", true);
            moveAni.SetInteger("InputV", (int)v);
        }
        else
            moveAni.SetBool("isChange", false);
    }

    private void FixedUpdate()
    {
        //������
        Vector2 moveVec = isHorizontal ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;
    }

    public InteractionData[] interactions = new InteractionData[]
    {
           new InteractionData { tag = "home", position = new Vector2(-80, -6) },
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


     