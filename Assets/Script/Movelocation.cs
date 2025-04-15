using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movelocation : MonoBehaviour
{
    public static Movelocation Instance { get; private set; }

    [SerializeField] GameObject player;

    [SerializeField] public Dictionary<int, Vector3> playerMoveDic;

    [SerializeField] public List<Collider2D> colliders;

    [SerializeField] Animator sceanChangeAni;

    //트리거 상호작용
    [SerializeField] private GameObject scanObject;


    bool moveAble = false;

    private void Awake()
    {
        playerMoveDic = new Dictionary<int, Vector3>();
        Instance = this;
        GetDicVecter();
    }

    void GetDicVecter()
    {
        playerMoveDic.Add(0, new Vector3(-90f, 15f, 0f));
        playerMoveDic.Add(1, new Vector3(-125f, 8f, 0f));
        playerMoveDic.Add(2, new Vector3(-40f, 2.5f, 0f));
        playerMoveDic.Add(3, new Vector3(-90f, 0f, 0f));
        playerMoveDic.Add(4, new Vector3(52f, 43.5f, 0f));
        playerMoveDic.Add(5, new Vector3(-35f, 1f, 0f));
        playerMoveDic.Add(6, new Vector3(158f, 43f, 0f));
        playerMoveDic.Add(7, new Vector3(52f, 80f, 0f));
        playerMoveDic.Add(8, new Vector3(54f, 118f, 0f));
        playerMoveDic.Add(9, new Vector3(23f, 132f, 0f));
    }

    public void SetTriggerPlayer(Collider2D collider)
    {
        for (int i = 0; i < colliders.Count; i++)
        {
            if (colliders[i] == collider)
            {
                int index = i;

                if (playerMoveDic.TryGetValue(index, out Vector3 targetPosition) || !moveAble)
                {
                    StartCoroutine(SceanChange(targetPosition));
                }
                break;
            }
        }

    }

    public IEnumerator SceanChange(Vector3 targetPosition)
    {
        sceanChangeAni.SetTrigger("Scean");
        TalkManager.Instance.isAction = true;
        yield return new WaitForSeconds(0.5f);
        player.transform.position = targetPosition;
        TalkManager.Instance.isAction = false;
    }


    //트리거 상호작용
    public void Action(Collider2D collision)
    {
        if (collision.CompareTag("moveCol"))
        {
            InteractiveCollider interactiveCollider = collision.GetComponent<InteractiveCollider>();
            if (interactiveCollider == null) return;

            Interaction(interactiveCollider.activobj, interactiveCollider.scriptObj);
            collision.gameObject.tag = "Untagged";
        }
        else
            return;
    }

    public void Interaction(GameObject[] activobj, GameObject scriptObj)
    {
        HomeTimeer homeTimeer;
     
        if (scriptObj != null)
        { 
            homeTimeer = scriptObj.GetComponent<HomeTimeer>();
            homeTimeer.startGame = false;
        }

        if (activobj != null && activobj.Length > 0)
        {
            foreach (GameObject obj in activobj)
            {
                if (obj != null) obj.SetActive(!obj.activeSelf);
            }
        }
        else
            return;
         
    }
}




