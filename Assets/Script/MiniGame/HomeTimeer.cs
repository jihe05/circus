using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeTimeer : MonoBehaviour
{
    public bool startGame = false;
    float elapsedTime = 20;

    [SerializeField]
    private Text timerT;
    [SerializeField]
    private GameObject success;
    [SerializeField]
    private GameObject failure;

    //Ȱ��ȭ �Ǹ� ���� ����
    private void Awake()
    {
        startGame = true;
    }

    private void Update()
    {
        // Ÿ�̸� ����
        if (startGame)
        {
            elapsedTime -= Time.deltaTime;
            timerT.text = Mathf.Ceil(elapsedTime).ToString(); // UI�� Ÿ�̸� ǥ��
            if (elapsedTime < 10)
            {
                timerT.color = Color.red;
            }
            if (elapsedTime == 0)
            {
                startGame = false;
            }
        }
        else
        {
            // �ð��� ���� �ְ�, Ư�� �������� �κ��丮�� �ִ� ��� ����
            if (elapsedTime > 0 && Inventory.Instance.HasItem("Diary"))
            {
                success.SetActive(true);
                Invoke("GameObjectSetActivefalse", 3);
            }
            else if (elapsedTime == 0 || !Inventory.Instance.HasItem("Diary"))
            {
                // ���� ó��
                failure.SetActive(true);
                Invoke("GameObjectSetActivefalse", 3);
            }
            else
            {
                failure.SetActive(true);
                Invoke("GameObjectSetActivefalse", 3);
            }

        }

       
    }

    public void GameObjectSetActivefalse()
    { 
         gameObject.SetActive(false);
    }


}
