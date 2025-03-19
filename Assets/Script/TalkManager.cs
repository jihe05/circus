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
        talkData.Add(1000, new string[] { "���� \n ����.. �� ��ħ�ΰ�", "���� \n ���� �޿��� �ҸӴϸ� �� ����� ���" });
        talkData.Add(10, new string[] { "������ �����̴�." });
        talkData.Add(11, new string[] { "���̺� ���� ������ �ϱ����� ���δ�." });
        talkData.Add(12, new string[] { "TV \n���ú��� �� �ް� �������׿��� ��Ŀ���� ������ �մϴ�!", "���� \n�츮 �����ݾ�?", "���� \n ���� �������!", "���� \n �� ���忡 ���� ��Ƴ����µ�.." });
        talkData.Add(13, new string[] { "���踦 ã�Ҵ�.", "���� \n �̰ɷ� �������� ���� ������" });
        talkData.Add(14, new string[] { "���� ����ִ�." });
        talkData.Add(15, new string[] { "���� ������ ����ġ�� �ִ�." });
        talkData.Add(16, new string[] { "���� �������̴�, ������ ������ ���� �ʴ´�.", "���� \n ���ܵ� �����Ÿ� Ÿ�߰ھ�" });
        talkData.Add(17, new string[] { "�׳� ����� Ǯ���̴�." });
        talkData.Add(18, new string[] { "�̻��ϰ� Ƣ��� ����...", "���� \n ���⿡ �����Ÿ� ���ܳ�����..." });
        talkData.Add(19, new string[] { "10000���Դϴ�." });
        talkData.Add(20, new string[] { "���� \n ��.. ��Ŀ�� ������.. ���� �������̾�!", "���� \n ���� �����Ϸ�����, � �ڸ��� ����!" });
        talkData.Add(21, new string[] { "�� \n ��ſ� ���� �Ǽ̱⸦..." });
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

        // ��ȭ�� ������ ó��
        if (currentTalk == null)
        {
            HandleInteraction(checkBoxT, activobj, getItem, itemData);
            return;
        }

        // notTolk�� true�� ��� ��ȭâ ��Ȱ��ȭ
        if (notTolk)
        {
            talkPanel.SetActive(false);  // ��ȭâ ��Ȱ��ȭ
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

        // üũ�ڽ� ǥ��
        if (checkBoxT != "0")
            Checkbox(checkBoxT);

        // ������ ȹ��
        if (getItem && scanObject != null)
        {
            scanObject.layer = LayerMask.NameToLayer("Default");
            Inventory.Instance.AddItem(itemData);
        }

        // ������Ʈ Ȱ��ȭ/��Ȱ��ȭ ó��
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
        portraitImg.color = talkData.Contains("����") ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0);
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
