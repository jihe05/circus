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

    public void OnClickButtonYes(GameObject Obj)
    {
        Obj.SetActive(true);
    }

    public void ActiveGameObject(GameObject scanObject)
    {
        //�������� ������ ������Ʈ�� �ڽ� ������Ʈ�� Ȱ��ȭ �ϰų� ��Ȱ��ȭ ��
        if (scanObject == null) return;

        Transform[] children = scanObject.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child == scanObject.transform) continue; // �θ� ������Ʈ�� ����
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }

        //�ڽ��̹迭�� ���������
        if (children.Length <= 1)
        {
            scanObject.SetActive(false);
        }



    }

}

