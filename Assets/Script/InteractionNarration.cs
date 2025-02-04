using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractionNarration : MonoBehaviour
{
    public static InteractionNarration instance;

    [SerializeField] private GameObject narration;
    [SerializeField] private Text narrationText;

    [SerializeField] private GameObject checkBox;
    [SerializeField] private Text checkBoxText;

    private Coroutine currentCoroutine;
    private bool isNarrationActive = false;

    string nowCheckBoxText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isNarrationActive && Input.GetMouseButtonDown(0))
        {
            SkipNarration();
        }
    }

    public void Narration(string _narration, string _checkBox)
    {
        nowCheckBoxText = _checkBox;
        narrationText.text = _narration;
        isNarrationActive = true;
        ActivateForSeconds(narration, nowCheckBoxText);
    }

    public void Checkbox(string nowCheckBoxText)
    {
        Time.timeScale = 0;
        checkBox.SetActive(true);
        checkBoxText.text = nowCheckBoxText;
    }

    public void ActivateForSeconds(GameObject obj, string _checkBox)
    {
        if (obj == null) return;
        obj.SetActive(true);

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(DeactivateAfterTime(obj, _checkBox));
    }

    private IEnumerator DeactivateAfterTime(GameObject obj, string _checkBox)
    {
        yield return new WaitForSeconds(2);
        SkipNarration(); 
    }

    public void SkipNarration()
    {
        if (!isNarrationActive) return;

        isNarrationActive = false;
        narration.SetActive(false); 
        Checkbox(nowCheckBoxText);
    }

    public void OnButtonClickNo()
    {
        Time.timeScale = 1;
    }
}
