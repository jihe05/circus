using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    GameObject[] activObj;

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
        Talk(objData.id, objData.checkBoxT, objData.activobj, objData.getItem, objData.itemData);

        talkPanal.SetActive(isAction);
    }

    public void GenerateData()
    {
        talkData.Add(1000, new string[] { "���� \n ����.. �Ǿ�ħ�ΰ�" , "���� \n ���� �޿��� �ҸӴϸ� �� ����� ���" });

        talkData.Add(10, new string[] { "������ �����̴�." });
        talkData.Add(11, new string[] { "���̺� ���� ������ �ϱ����� ���δ�." });
        talkData.Add(12, new string[] { "TV \n���ú��� �� �ް� �������׿��� ��Ŀ���� ������ �մϴ�! ª�� �Ⱓ�̴ϸ�ŭ ��ġ�� ���� ��ܺ��ñ� �ٶ��ϴ�!", "���� \n�츮 �����ݾ�?", "���� \n ���� �������!" , "���� \n ���忡 ���� ��Ƴ����µ�.." });
        talkData.Add(13, new string[] { "���踦 ã�Ҵ�." , "���� \n �̰ɷ� �������� ���� ������" });
        talkData.Add(14, new string[] { "���� ����ִ�."});
        talkData.Add(15, new string[] { "���� ������ ����ġ�� �ִ�." });
        talkData.Add(16, new string[] { "���� �������̴�, ������ ������ ���� �ʴ´�." , "���� \n ���ܵ� �����Ÿ� Ÿ�߰ھ�" });
        talkData.Add(17, new string[] { "�׳� ����� Ǯ���̴�." });
        talkData.Add(18, new string[] { "�̻��ϰ� Ƣ��� ����...", "���� \n ���⿡ �����Ÿ� ���ܳ�����..." });
        talkData.Add(19, new string[] { "10000���Դϴ�." });
        talkData.Add(20, new string[] { "���� \n ��..��Ŀ��������.. ���� �������̾�!", "���� \n �� ���� �����Ϸ����� � �ڸ��� ����!" });


    }

    string GetTalk(int id, int talkindex)
    {
        if (talkindex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkindex];
    }

    public void Talk(int id, string checkBoxT, GameObject[] activobj, bool getItem, ItemData itemData)
    {
        string talkData = GetTalk(id, talkindex);

        if (talkData == null)
        {
            isAction = false;
            talkindex = 0;

            if (checkBoxT != "0")
                Checkbox(checkBoxT);
            if (getItem)
            {
                scanObject.layer = LayerMask.NameToLayer("Default");
                Inventory.Instance.AddItem(itemData);
            }

            return;
        }

        UpdateTalkUI(talkData);

        isAction = true;
        talkindex++;
        if (activobj != null)
        {
            for (int i = 0; i < activobj.Length; i++)
            {
                activObj[i] = activobj[i];
            }
        }
        
    }

    private void UpdateTalkUI(string talkData)
    {
        talkText.text = talkData;

        if (talkText.text.Contains("����"))
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

    //���� ����
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

    public void OnClickButtonYes()
    {
        if (activObj == null)
            return;

        for (int i = 0; i < activObj.Length; i++)
        {
            if (activObj[i].activeSelf == false)
                activObj[i].SetActive(true);
           else if (activObj[i].activeSelf == true)
                activObj[i].SetActive(false);
        }
    }



}

