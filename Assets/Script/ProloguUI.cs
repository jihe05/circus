using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProloguUI : MonoBehaviour
{
    [SerializeField]
    public Scrollbar scrollbar;

    public float duration = 2f;
    private Coroutine scrollCoroutine; // 코루틴을 추적하기 위한 변수
    private bool isSkipping = false; // 스킵 여부 확인

    private void Start()
    {
        scrollCoroutine = StartCoroutine(ScrollToZero());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 감지
        {
            SkipPrologue();
        }
    }

    IEnumerator ScrollToZero()
    {
        float startValue = scrollbar.value;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (isSkipping) yield break; // 스킵되면 즉시 종료
            elapsedTime += Time.deltaTime;
            scrollbar.value = Mathf.Lerp(startValue, 0f, elapsedTime / duration);
            yield return null;
        }

        scrollbar.value = 0f;
        EndPrologue();
    }

    private void SkipPrologue()
    {
        if (isSkipping) return; // 이미 스킵 중이면 실행 안 함

        isSkipping = true;
        if (scrollCoroutine != null)
        {
            StopCoroutine(scrollCoroutine); // 진행 중인 코루틴 중단
        }

        scrollbar.value = 0f;
        EndPrologue();
    }

    private void EndPrologue()
    {
        GameManager.Instance.StartGameSetting();
    }
}
