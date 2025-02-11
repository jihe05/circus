using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Diary : MonoBehaviour
{
     List<string> diaryList = new List<string>(); // 일기 리스트(인스펙터 창에서 직접 작성)
    [SerializeField] private Text leftT; // 왼쪽 페이지 텍스트
    [SerializeField] private Text rightT; // 오른쪽 페이지 텍스트
    [SerializeField] private InputField leftinputField; 
    [SerializeField] private InputField rightinputField;
    [SerializeField] private Text leftpageIndex;
    [SerializeField] private Text rightpageIndex;

    private int currentPage = 0; // 현재 페이지 번호

    private void Start()
    {
        InitializeDiary();
        UpdateDiary();
    }

    // 일기 기본 데이터 설정 (세미의 일지)
    private void InitializeDiary()
    {
        // 세미의 일기 (총 6페이지)
        diaryList.Add("9월 5일\n\n오늘도 하루 종일 다락방에 있었다. 밖은 춥고, 이 방도 춥다.\r\n고모는 아침부터 기분이 나빴는지 나를 쳐다보지도 않았다.\r\n괜찮아. 어차피 말 걸어도 좋은 소리는 안 들릴 테니까.\r\n할머니가 계셨다면, 따뜻한 차를 끓여주셨을까?\r\n할머니 냄새가 나는 담요를 꼭 끌어안고 잤다.");
        diaryList.Add("9월 6일\n\n고모는 나한테 자꾸 집안일을 시킨다.\r\n오늘은 설거지와 청소를 했는데, 그 때마다 무시당하는 기분이 든다.\r\n혼자 있는 시간이 많아져서, 생각이 많아졌다.\r\n할머니가 떠나고 나서 나도 변한 것 같고, 고모도 더 차가워졌다.\r\n그래도… 그냥 참아야 할까? 언제까지 이렇게 살아야 할까?");
        diaryList.Add("9월 7일\n\n이젠 다락방에 있는 것조차 싫어졌다.\r\n창문을 통해 보이는 작은 정원만이 유일한 위로다.\r\n그냥… 밖으로 나가고 싶다. 혼자서라도 나가면 더 좋을 텐데.\r\n어떤 날은 이렇게 답답하게 하루하루가 지나가는 게 너무 무섭다.\r\n할머니가 나를 어떻게 생각했을까? 늘 나를 따뜻하게 감싸 주셨던 할머니.\r\n이젠 그리운 마음만 더 커진다.");
        diaryList.Add("9월 8일\n\n고모가 나를 더욱 무시하는 것 같아.\r\n오늘은 고모의 얼굴을 보고 아무 말도 하지 않았다. 그냥 고개만 끄덕였을 뿐.\r\n나는 여전히 다락방에 갇혀 있는 느낌이다.\r\n할머니가 계셨다면, 이런 상황이 다르게 느껴졌을까?\r\n하지만 이제는 그저… 멀리 떠나고 싶은 마음뿐이다.\r\n할머니, 나를 데려가 주세요. 여기에서 벗어나고 싶어요.");
        diaryList.Add("9월 9일\n\n창문 밖을 보니까 아이들이 놀고 있었다.\r\n나도 같이 뛰어놀고 싶었는데… 언제까지 여기 있어야 할까?\r\n할머니가 떠난 뒤로 모든 게 엉망이야.\r\n하지만, 언젠가는…\r\n어디든 좋으니까, 멀리 가고 싶어. 고모에게서 벗어나, 자유롭게 살고 싶다.");
        diaryList.Add("9월 10일\n\n언젠간 이집을 떠나고 말꺼야...");

        // 나머지 4페이지는 빈 값으로 설정 (사용자가 입력할 예정)
        for (int i = 6; i < 10; i++)
        {
            diaryList.Add("");
        }
    }

    // 왼쪽 버튼 클릭 시 호출
    public void OnClickLeft()
    {
        if (currentPage > 0)
        {
            SaveDiaryInput(); // 페이지 이동 전에 입력값 저장
            currentPage -= 2;
            UpdateDiary();
        }
      
    }

    // 오른쪽 버튼 클릭 시 호출
    public void OnClickRight()
    {
        if (currentPage < diaryList.Count - 2)
        {
            SaveDiaryInput(); // 페이지 이동 전에 입력값 저장
            currentPage += 2;
            UpdateDiary();
        }
       
    }

    // 페이지 내용 업데이트
    private void UpdateDiary()
    {
        // 왼쪽 페이지 텍스트 설정
        leftT.text = (currentPage < diaryList.Count) ? diaryList[currentPage] : "";
        rightT.text = (currentPage + 1 < diaryList.Count) ? diaryList[currentPage + 1] : "";

        // 인덱스 업데이트 (페이지 번호 표시)
        leftpageIndex.text = (currentPage + 1).ToString();
        rightpageIndex.text = (currentPage + 2).ToString();

        // 사용자가 직접 입력할 수 있는 페이지 (7~10페이지) 처리
        if (currentPage >= 6) // 7페이지부터는 인풋 필드 활성화
        {
            leftinputField.gameObject.SetActive(true);
            rightinputField.gameObject.SetActive(true);
            leftT.gameObject.SetActive(false);
            rightT.gameObject.SetActive(false);

            // 기존에 저장된 내용을 불러오기
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

    // 입력된 내용을 저장하는 함수
    public void SaveDiaryInput()
    {
        if (currentPage >= 6) // 7페이지 이상일 때만 저장
        {
            diaryList[currentPage] = leftinputField.text;
            if (currentPage + 1 < diaryList.Count)
            {
                diaryList[currentPage + 1] = rightinputField.text;
            }
        }
    }
}
