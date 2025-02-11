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

}




