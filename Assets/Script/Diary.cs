using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Diary : MonoBehaviour
{
     List<string> diaryList = new List<string>(); // �ϱ� ����Ʈ(�ν����� â���� ���� �ۼ�)
    [SerializeField] private Text leftT; // ���� ������ �ؽ�Ʈ
    [SerializeField] private Text rightT; // ������ ������ �ؽ�Ʈ
    [SerializeField] private InputField leftinputField; 
    [SerializeField] private InputField rightinputField;
    [SerializeField] private Text leftpageIndex;
    [SerializeField] private Text rightpageIndex;

    private int currentPage = 0; // ���� ������ ��ȣ

    private void Start()
    {
        InitializeDiary();
        UpdateDiary();
    }

    // �ϱ� �⺻ ������ ���� (������ ����)
    private void InitializeDiary()
    {
        // ������ �ϱ� (�� 6������)
        diaryList.Add("9�� 5��\n\n���õ� �Ϸ� ���� �ٶ��濡 �־���. ���� ���, �� �浵 ���.\r\n���� ��ħ���� ����� �������� ���� �Ĵٺ����� �ʾҴ�.\r\n������. ������ �� �ɾ ���� �Ҹ��� �� �鸱 �״ϱ�.\r\n�ҸӴϰ� ��̴ٸ�, ������ ���� �����ּ�����?\r\n�ҸӴ� ������ ���� ��並 �� ����Ȱ� ���.");
        diaryList.Add("9�� 6��\n\n���� ������ �ڲ� �������� ��Ų��.\r\n������ �������� û�Ҹ� �ߴµ�, �� ������ ���ô��ϴ� ����� ���.\r\nȥ�� �ִ� �ð��� ��������, ������ ��������.\r\n�ҸӴϰ� ������ ���� ���� ���� �� ����, ��� �� ����������.\r\n�׷����� �׳� ���ƾ� �ұ�? �������� �̷��� ��ƾ� �ұ�?");
        diaryList.Add("9�� 7��\n\n���� �ٶ��濡 �ִ� ������ �Ⱦ�����.\r\nâ���� ���� ���̴� ���� �������� ������ ���δ�.\r\n�׳ɡ� ������ ������ �ʹ�. ȥ�ڼ��� ������ �� ���� �ٵ�.\r\n� ���� �̷��� ����ϰ� �Ϸ��Ϸ簡 �������� �� �ʹ� ������.\r\n�ҸӴϰ� ���� ��� ����������? �� ���� �����ϰ� ���� �̴ּ� �ҸӴ�.\r\n���� �׸��� ������ �� Ŀ����.");
        diaryList.Add("9�� 8��\n\n��� ���� ���� �����ϴ� �� ����.\r\n������ ����� ���� ���� �ƹ� ���� ���� �ʾҴ�. �׳� ���� �������� ��.\r\n���� ������ �ٶ��濡 ���� �ִ� �����̴�.\r\n�ҸӴϰ� ��̴ٸ�, �̷� ��Ȳ�� �ٸ��� ����������?\r\n������ ������ ������ �ָ� ������ ���� �������̴�.\r\n�ҸӴ�, ���� ������ �ּ���. ���⿡�� ����� �;��.");
        diaryList.Add("9�� 9��\n\nâ�� ���� ���ϱ� ���̵��� ��� �־���.\r\n���� ���� �پ��� �;��µ��� �������� ���� �־�� �ұ�?\r\n�ҸӴϰ� ���� �ڷ� ��� �� �����̾�.\r\n������, �������¡�\r\n���� �����ϱ�, �ָ� ���� �;�. ��𿡰Լ� ���, �����Ӱ� ��� �ʹ�.");
        diaryList.Add("9�� 10��\n\n������ ������ ������ ������...");

        // ������ 4�������� �� ������ ���� (����ڰ� �Է��� ����)
        for (int i = 6; i < 10; i++)
        {
            diaryList.Add("");
        }
    }

    // ���� ��ư Ŭ�� �� ȣ��
    public void OnClickLeft()
    {
        if (currentPage > 0)
        {
            SaveDiaryInput(); // ������ �̵� ���� �Է°� ����
            currentPage -= 2;
            UpdateDiary();
        }
      
    }

    // ������ ��ư Ŭ�� �� ȣ��
    public void OnClickRight()
    {
        if (currentPage < diaryList.Count - 2)
        {
            SaveDiaryInput(); // ������ �̵� ���� �Է°� ����
            currentPage += 2;
            UpdateDiary();
        }
       
    }

    // ������ ���� ������Ʈ
    private void UpdateDiary()
    {
        // ���� ������ �ؽ�Ʈ ����
        leftT.text = (currentPage < diaryList.Count) ? diaryList[currentPage] : "";
        rightT.text = (currentPage + 1 < diaryList.Count) ? diaryList[currentPage + 1] : "";

        // �ε��� ������Ʈ (������ ��ȣ ǥ��)
        leftpageIndex.text = (currentPage + 1).ToString();
        rightpageIndex.text = (currentPage + 2).ToString();

        // ����ڰ� ���� �Է��� �� �ִ� ������ (7~10������) ó��
        if (currentPage >= 6) // 7���������ʹ� ��ǲ �ʵ� Ȱ��ȭ
        {
            leftinputField.gameObject.SetActive(true);
            rightinputField.gameObject.SetActive(true);
            leftT.gameObject.SetActive(false);
            rightT.gameObject.SetActive(false);

            // ������ ����� ������ �ҷ�����
            leftinputField.text = diaryList[currentPage];
            rightinputField.text = (currentPage + 1 < diaryList.Count) ? diaryList[currentPage + 1] : "";
        }
        else
        {
            leftinputField.gameObject.SetActive(false);
            rightinputField.gameObject.SetActive(false);
            leftT.gameObject.SetActive(true);
            rightT.gameObject.SetActive(true);
        }
    }

    // �Էµ� ������ �����ϴ� �Լ�
    public void SaveDiaryInput()
    {
        if (currentPage >= 6) // 7������ �̻��� ���� ����
        {
            diaryList[currentPage] = leftinputField.text;
            if (currentPage + 1 < diaryList.Count)
            {
                diaryList[currentPage + 1] = rightinputField.text;
            }
        }
    }
}
