using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ProloguUI : MonoBehaviour
{
    [SerializeField]
    public Scrollbar scrollbar;

    public float duration = 2f; 

    void Start()
    {
        StartCoroutine(ScrollToZero()); 
    }

    IEnumerator ScrollToZero()
    {
        float startValue = scrollbar.value; 
        float elapsedTime = 0f; 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; 
            scrollbar.value = Mathf.Lerp(startValue, 0f, elapsedTime / duration);
            yield return null;
        }

        scrollbar.value = 0f;
        gameObject.SetActive(false);

    }
}


