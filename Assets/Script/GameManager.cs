using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject prologue;
    [SerializeField] private GameObject homeTimeGame;
    [SerializeField] private List<GameObject> timelineObject;
    [SerializeField] private Button skipButton;

    private bool previousActiveState = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        bool hasActiveObject = false;

        foreach (var obj in timelineObject)
        {
            if (obj.activeSelf)
            {
                hasActiveObject = true;
                break;
            }
        }

        if (hasActiveObject != previousActiveState)
        {
            skipButton.gameObject.SetActive(hasActiveObject);
            previousActiveState = hasActiveObject;
        }
    }

    public void OnSkipButtonClicked()
    {
        foreach (var obj in timelineObject)
        {
            if (obj.activeSelf)
            {
                obj.SetActive(false);
                TalkManager.Instance.Activepenal();
                playerMove.enabled = true;
                break;
            }
        }
    }

    public void StartGameSetting()
    {
        animator.SetTrigger("Scean");
        playerMove.transform.position = new Vector3(-134.3f, 9.3f, 0);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);
        prologue.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        TalkManager.Instance.MonologueId(1000);
    }
}
