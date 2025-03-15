using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    [Header("������")]
    [SerializeField] private float speed;
    float h;
    float v;
    bool isHorizontal = false;
    Rigidbody2D rigid;
    Animator moveAni;

    [Header("��ȣ�ۿ�")]
    Vector3 dirVec;
    GameObject scanObject;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        moveAni = GetComponent<Animator>();
    }

    private void Update()
    {
        //������ ��
        h = TalkManager.Instance.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = TalkManager.Instance.isAction ? 0 : Input.GetAxisRaw("Vertical");

        //���Ʒ� üũ
        bool hDown = TalkManager.Instance.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = TalkManager.Instance.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = TalkManager.Instance.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = TalkManager.Instance.isAction ? false : Input.GetButtonUp("Vertical");

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

        //Direction
        if (vDown && v == 1)
            dirVec = Vector3.up;
        else if (vDown && v == -1)
            dirVec = Vector3.down;
        else if (hDown && h == -1)
            dirVec = Vector3.left;
        else if(hDown && h == 1)
            dirVec = Vector3.right;

        //��ȣ�ۿ� Ű
        if (Input.GetKeyDown(KeyCode.F) && scanObject == null && TalkManager.Instance.isAction == true)
            TalkManager.Instance.Monologue();
        else if (Input.GetKeyDown(KeyCode.F) && scanObject != null)
            TalkManager.Instance.Action(scanObject);
        else if (Input.GetMouseButtonDown(0) && scanObject != null)
             Inventory.Instance.RemoveItem(scanObject);

    }

    private void FixedUpdate()
    {
        //������
        Vector2 moveVec = isHorizontal ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * speed;

        //����
        Debug.DrawRay(rigid.position, dirVec * 0.7f, Color.red);
        RaycastHit2D objRayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (objRayHit.collider != null)
            scanObject = objRayHit.collider.gameObject;
        else
            scanObject = null;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Movelocation.Instance.SetTriggerPlayer(collision.collider);

    }


}

     