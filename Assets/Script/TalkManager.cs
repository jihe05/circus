using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public static TalkManager Instance { get; private set; }

    [SerializeField] private GameObject talkPanel;
    [SerializeField] private Image portraitImg;
    [SerializeField] private Text talkText;
    [SerializeField] private GameObject scanObject;
    [SerializeField] private GameObject checkbox;
    public bool isAction;
    public int talkindex;

    private Dictionary<int, string[]> talkData = new Dictionary<int, string[]>();

    private int monologueID;
    private GameObject[] activObj;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        GenerateData();
    }

    public void Action(GameObject scanobj)
    {
        if (scanobj == null) return;

        scanObject = scanobj;
        ObjData objData = scanobj.GetComponent<ObjData>();
        if (objData == null) return;

        Talk(objData.id, objData.checkBoxT, objData.activobj, objData.getItem, objData.itemData, objData.notTolk);

            talkPanel.SetActive(isAction);
    }

    private void GenerateData()
    {
        talkData.Add(1000, new string[] { "세미 \n 끄으.. 또 아침인가", "세미 \n 오늘 꿈에서 할머니를 본 기분이 들어" });
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
        talkData.Add(20, new string[] { "세미 \n 와.. 서커스 공연장.. 정말 오랜만이야!", "세미 \n 드디어 시작하려나봐, 어서 자리에 앉자!" });
        talkData.Add(21, new string[] { "잭 \n 즐거운 관람 되셨기를..." });
    }

    private string GetTalk(int id, int talkindex)
    {
        if (!talkData.ContainsKey(id)) return null;
        if (talkindex < 0 || talkindex >= talkData[id].Length) return null;

        return talkData[id][talkindex];
    }

    public void Talk(int id, string checkBoxT, GameObject[] activobj, bool getItem, ItemData itemData, bool notTolk)
    {
        string currentTalk = GetTalk(id, talkindex);

        // 대화가 없으면 처리
        if (currentTalk == null)
        {
            HandleInteraction(checkBoxT, activobj, getItem, itemData);
            return;
        }

        // notTolk가 true인 경우 대화창 비활성화
        if (notTolk)
        {
            talkPanel.SetActive(false);  // 대화창 비활성화
            HandleInteraction(checkBoxT, activobj, getItem, itemData);
        }
        else
        {
            UpdateTalkUI(currentTalk);
            isAction = true;
            talkindex++;
        }
    }

    private void HandleInteraction(string checkBoxT, GameObject[] activobj, bool getItem, ItemData itemData)
    {
        isAction = false;
        talkindex = 0;

        // 체크박스 표시
        if (checkBoxT != "0")
            Checkbox(checkBoxT);

        // 아이템 획득
        if (getItem && scanObject != null)
        {
            scanObject.layer = LayerMask.NameToLayer("Default");
            Inventory.Instance.AddItem(itemData);
        }

        // 오브젝트 활성화/비활성화 처리
        if (activobj != null && activobj.Length > 0)
        {
            if (checkBoxT != "0")
            {
                activObj = new GameObject[activobj.Length];
                activobj.CopyTo(activObj, 0);
            }

            if (checkBoxT == "0")
            {
                foreach (GameObject obj in activobj)
                {
                    if (obj != null) obj.SetActive(!obj.activeSelf);
                }
            }
        }
    }

    private void UpdateTalkUI(string talkData)
    {
        if (talkText == null) return;

        talkText.text = talkData;
        portraitImg.color = talkData.Contains("세미") ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
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
            talkPanel.SetActive(false);
            talkindex = 0;
            return;
        }

        talkText.text = currentTalk;
        portraitImg.color = new Color(1, 1, 1, 1);
        talkPanel.SetActive(true);
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
        if (talkPanel != null) talkPanel.SetActive(false);
    }

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
