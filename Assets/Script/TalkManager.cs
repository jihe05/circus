using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
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

    Dictionary<int, string[]> talkData;

    int monologueID;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    public void Action(GameObject scanobj)
    {
         scanObject = scanobj;
         ObjData objData = scanobj.GetComponent<ObjData>();
         Talk(objData.id, objData.isNap, objData.checkBoxT, objData.activobj, objData.getItem , objData.itemData);
      
        talkPanal.SetActive(isAction);
    }

    public void GenerateData()
    {
        talkData.Add(1000, new string[] { "세미 \n 끄으.. 또아침인가" , "세미 \n 오늘 꿈에서 할머니를 본 기분이 들어" });

        talkData.Add(10, new string[] { "세미의 옷장이다." });
        talkData.Add(11, new string[] { "테이블 위에 세미의 일기장이 보인다." });
        talkData.Add(12, new string[] { "TV \n오늘부터 한 달간 ㅇㅇ동네에서 서커스가 열린다 합니다! 짧은 기간이니만큼 놓치지 말고 즐겨보시길 바랍니다!", "세미 \n우리 동네잖아?", "세미 \n 당장 출발하자!" , "세미 \n 옷장에 돈을 모아놨었는데.." });
        talkData.Add(13, new string[] { "열쇠를 찾았다." , "세미 \n 이걸로 현관문을 열고 나가자" });
        talkData.Add(14, new string[] { "문이 잠겨있다."});
        talkData.Add(15, new string[] { "옷장 구석에 돈뭉치가 있다." });
        talkData.Add(16, new string[] { "버스 정류장이다, 하지만 버스는 오지 않는다." , "세미 \n 숨겨둔 자전거를 타야겠어" });
        talkData.Add(17, new string[] { "그냥 평범한 풀숲이다." });
        talkData.Add(18, new string[] { "이상하게 튀어나온 바위...", "세미 \n 여기에 자전거를 숨겨놨었지..." });
        talkData.Add(19, new string[] { "10000원입니다." });


    }

    string GetTalk(int id, int talkindex)
    {
        if (talkindex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkindex];
    }

    public void Talk(int id, bool isNpc, string checkBoxT, bool activobj, bool getItem, ItemData itemData)
    {
        string talkData = GetTalk(id, talkindex);

        if (talkData == null)
        {
            isAction = false;
            talkindex = 0;

            if (checkBoxT != "0")
                Checkbox(checkBoxT);
            if (activobj)
                ActiveGameObject(scanObject);
            if (getItem)
            {
                scanObject.layer = LayerMask.NameToLayer("Default");
                Inventory.Instance.AddItem(itemData);
            }

            return;
        }

        UpdateTalkUI(talkData, isNpc);

        isAction = true;
        talkindex++;
    }

    private void UpdateTalkUI(string talkData, bool isNpc)
    {
        talkText.text = talkData;

        if (isNpc)
        {
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            portraitImg.color = new Color(1, 1, 1, 0);
        }
    }

    public void MonologueId(int id)
    {
        monologueID = id;
        Monologue();
    }

    //독백 전용
    public void Monologue()
    {
        string talkData = GetTalk(monologueID, talkindex);

        if (talkData == null)
        {
            isAction = false;
            talkPanal.SetActive(false);
            talkindex = 0;
            return;
        }

        talkText.text = talkData;
        portraitImg.color = new Color(1, 1, 1, 1);
        talkPanal.SetActive(true);
        isAction = true;
        talkindex++;

    }

    public void Checkbox(string checkboxT)
    {
        isAction = true;
        checkbox.SetActive(true);
        checkbox.GetComponentInChildren<Text>().text = checkboxT;
    }

    public void OnClickButtonNo()
    {
        isAction = false;
        checkbox.SetActive(false);
        talkPanal.SetActive(false);
    }

    public void OnClickButtonYes(GameObject Obj)
    {
        Obj.SetActive(true);
    }

    public void ActiveGameObject(GameObject scanObject)
    {
        //아이템을 먹으면 오브젝트의 자식 오브젝트를 활성화 하거나 비활성화 함
        if (scanObject == null) return;

        Transform[] children = scanObject.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child == scanObject.transform) continue; // 부모 오브젝트는 제외
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }

        //자식이배열이 비어잇으면
        if (children.Length <= 1)
        {
            scanObject.SetActive(false);
        }



    }

}

