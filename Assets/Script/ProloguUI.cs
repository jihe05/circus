using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProloguUI : MonoBehaviour
{
    [SerializeField]
    public Scrollbar scrollbar;

    public float duration = 2f;
    private Coroutine scrollCoroutine; // �ڷ�ƾ�� �����ϱ� ���� ����
    private bool isSkipping = false; // ��ŵ ���� Ȯ��

    private void Start()
    {
        scrollCoroutine = StartCoroutine(ScrollToZero());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ�� ����
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
            if (isSkipping) yield break; // ��ŵ�Ǹ� ��� ����
            elapsedTime += Time.deltaTime;
            scrollbar.value = Mathf.Lerp(startValue, 0f, elapsedTime / duration);
            yield return null;
        }

        scrollbar.value = 0f;
        EndPrologue();
    }

    private void SkipPrologue()
    {
        if (isSkipping) return; // �̹� ��ŵ ���̸� ���� �� ��

        isSkipping = true;
        if (scrollCoroutine != null)
        {
            StopCoroutine(scrollCoroutine); // ���� ���� �ڷ�ƾ �ߴ�
        }

        scrollbar.value = 0f;
        EndPrologue();
    }

    private void EndPrologue()
    {
        GameManager.Instance.StartGameSetting();
    }
}
