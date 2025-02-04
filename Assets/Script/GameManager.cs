using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   
    public static GameManager Instance;

    [SerializeField]
    private PlayerMove playerMove;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject prologue;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {

    }

    public void PlayerMoveStop()
    {
       playerMove.enabled = false;
    }

    public void SeanChangePoint()
    {
        animator.SetTrigger("Scean");
    }


}
