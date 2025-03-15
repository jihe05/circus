using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public static TalkManager Instance { get; private set; }

    [SerializeField] private GameObject talkPanal;
    [SerializeField] private Image portraitImg;
    [SerializeField] private Text talkText;
    [SerializeField] private GameObject scanObject;
    [SerializeField] private GameObject checkbox;
    public bool isAction;
    public int talkindex;

    Dictionary<int, string[]> talkData = new Dictionary<int, string[]>();

    int monologueID;
    GameObject[] activObj;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        GenerateData();
    }

    public void Action(GameObject scanobj)
    {
        if (scanobj == null) return; // Null 체크 추가

        scanObject = scanobj;
        ObjData objData = scanobj.GetComponent<ObjData>();
        if (objData == null) return; // ObjData가 없으면 실행하지 않음

        Talk(objData.id, objData.checkBoxT, objData.activobj, objData.getItem, objData.itemData);
        talkPanal.SetActive(isAction);
    }

    public void GenerateData()
    {
        talkData.Add(1000, new string[] { "세미 \n 끄으.. 또아침인가", "세미 \n 오늘 꿈에서 할머니를 본 기분이 들어" });
        talkData.Add(10, new string[] { "세미의 옷장이다." });
        talkData.Add(11, new string[] { "테이블 위에 세미의 일기장이 보인다." });
        talkData.Add(12, new string[] { "TV \n오늘부터 한 달간 ㅇㅇ동네에서 서커스가 열린다 합니다!", "세미 \n우리 동네잖아?", "세미 \n 당장 출발하자!", "세미 \n 내 옷장에 돈을 모아놨었는데.." });
        talkData.Add(13, new string[] { "열쇠를 찾았다.", "세미 \n 이걸로 현관문을 열고 나가자" });
        talkData.Add(14, new string[] { "문이 잠겨있다." });
        talkData.Add(15, new string[] { "옷장 구석에 돈뭉치가 있다." });
        talkData.Add(16, new string[] { "버스 정류장이다, 하지만 버스는 오지 않는다.", "세미 \n 숨겨둔 자전거를 타야겠어" });
        talkData.Add(17, new string[] { "그냥 평범한 풀숲이다." });
        talkData.Add(18, new string[] { "이상하게 튀어나온 바위...", "세미 \n 여기에 자전거를 숨겨놨었지..." });
        talkData.Add(19, new string[] { "10000원입니다." });
        talkData.Add(20, new string[] { "세미 \n 와..서커스공연장.. 정말 오랜만이야!", "세미 \n 드디어 시작하려나봐 어서 자리에 앉자!" });
    }

    string GetTalk(int id, int talkindex)
    {
        if (!talkData.ContainsKey(id)) return null; // ID가 없는 경우 예외 방지
        if (talkindex >= talkData[id].Length) return null; // talkindex가 범위를 벗어나면 null 반환

        return talkData[id][talkindex];
    }


    public void Talk(int id, string checkBoxT, GameObject[] activobj, bool getItem, ItemData itemData)
    {
        string currentTalk = GetTalk(id, talkindex);

        if (currentTalk == null)
        {
            isAction = false;
            talkindex = 0;

            if (checkBoxT != "0")
                Checkbox(checkBoxT);
            if (getItem && scanObject != null) // scanObject가 null이 아닌 경우에만 실행
            {
                scanObject.layer = LayerMask.NameToLayer("Default");
                Inventory.Instance.AddItem(itemData);
            }
            
            //만약 배열의 오브젝트가 존재 한다면 
            if (activobj != null && activobj.Length > 0)
            {
                if (checkBoxT != "0")
                {
                    activObj = new GameObject[activobj.Length];
                    for (int i = 0; i < activobj.Length; i++)
                    {
                        activObj[i] = activobj[i];
                    }
                }
               
                if (checkBoxT == "0")
                {
                    for (int i = 0; i < activobj.Length; i++)
                    {
                        Debug.Log("d");
                        scanObject.layer = LayerMask.NameToLayer("Default");
                        activobj[i].SetActive(!activobj[i].activeSelf);
                    }
                }
                 
            }
            return;
        }

        UpdateTalkUI(currentTalk);
        isAction = true;
        talkindex++;


    }

    private void UpdateTalkUI(string talkData)
    {
        if (talkText == null) return; // 예외 방지

        talkText.text = talkData;
        portraitImg.color = talkText.text.Contains("세미") ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
    }

    public void MonologueId(int id)
    {
        monologueID = id;
        Monologue();
    }

    public void Monologue()
    {
        string currentTalk = GetTalk(monologueID, talkindex);

        if (currentTalk == null)
        {
            isAction = false;
            talkPanal.SetActive(false);
            talkindex = 0;
            return;
        }

        talkText.text = currentTalk;
        portraitImg.color = new Color(1, 1, 1, 1);
        talkPanal.SetActive(true);
        isAction = true;
        talkindex++;
    }

    public void Checkbox(string checkboxT)
    {
        if (checkbox == null) return;

        isAction = true;
        checkbox.SetActive(true);
        checkbox.GetComponentInChildren<Text>().text = checkboxT;
    }

    public void OnClickButtonNo()
    {
        isAction = false;
        if (checkbox != null) checkbox.SetActive(false);
        if (talkPanal != null) talkPanal.SetActive(false);
    }

    //Yes를 누르면 
    public void OnClickButtonYes()
    {
        if (activObj == null || activObj.Length == 0) return;

        foreach (var obj in activObj)
        {
            if (obj != null)
            {
                obj.SetActive(!obj.activeSelf);
            }
        }
    }
}
