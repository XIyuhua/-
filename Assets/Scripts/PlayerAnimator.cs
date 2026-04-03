using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    private Animator animator;
    //定义动画状态的字符串常量，防止拼写错误
    private const string IS_WALKING = "IsWalking";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //更新玩家的动画状态
        if (player != null && animator != null)
        {
            animator.SetBool(IS_WALKING, player.IsMoving());
        }
    }

}
