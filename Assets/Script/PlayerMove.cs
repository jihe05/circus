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

        //움직임 값
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //위아래 체크
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        //isHorizontal 체트 움직임
        if (hDown)
            isHorizontal = true;
        else if (vDown)
            isHorizontal = false;
        else if(hUp || vUp)
            isHorizontal = h != 0;

        //애니메이션
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
        //움직임
        Vector2 moveVec = isHorizontal ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;
    }

    public InteractionData[] interactions = new InteractionData[]
    {
           new InteractionData { tag = "home", position = new Vector2(-80, -6) },
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


     