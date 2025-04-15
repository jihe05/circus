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

    //활성화 되면 게임 시작
    private void Awake()
    {
        startGame = true;
    }

    private void Update()
    {
        // 타이머 시작
        if (startGame)
        {
            elapsedTime -= Time.deltaTime;
            timerT.text = Mathf.Ceil(elapsedTime).ToString(); // UI에 타이머 표시
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
            // 시간이 남아 있고, 특정 아이템이 인벤토리에 있는 경우 성공
            if (elapsedTime > 0 && Inventory.Instance.HasItem("Diary"))
            {
                success.SetActive(true);
                Invoke("GameObjectSetActivefalse", 3);
            }
            else if (elapsedTime == 0 || !Inventory.Instance.HasItem("Diary"))
            {
                // 실패 처리
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
