using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField]
    private PlayerMove playerMove;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject prologue;

    [SerializeField]
    private GameObject homeTimeGame;
   
    private void Awake()
    {
        Instance = this;
    }

    public void StartGameSetting()
    {
        animator.SetTrigger("Scean");
        playerMove.transform.position = new Vector3(-134.3f, 9.3f, 0);
        StartCoroutine("StartGame");
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);
        prologue.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        TalkManager.Instance.MonologueId(1000);
    }

}