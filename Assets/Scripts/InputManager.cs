using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //事件监听器
    public event EventHandler OnIntercatAction;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        //启用角色移动事件
        playerInputActions.Player.Enable();

        //这是新输入系统自带的事件，按下e执行Interact_performed函数
        playerInputActions.Player.Intercat.performed += Interact_performed;
    }

    /// <summary>
    /// 这是角色互动事件的处理函数
    /// </summary>
    private void Interact_performed(InputAction.CallbackContext context)
    {
        //输入e键触发角色互动事件
        OnIntercatAction?.Invoke(this, EventArgs.Empty);
    }
    
    /// <summary>
    /// 获取角色移动方向
    /// </summary>
    /// <returns>返回角色移动方向</returns>
    public Vector2 GetMovementVectorNormalized()
    {
        //处理角色移动方向
        Vector2 moveVector2 = playerInputActions.Player.Move.ReadValue<Vector2>();

        //处理角色移动方向归一化，使得斜角移动不会速度更快
        moveVector2 = moveVector2.normalized;
        return moveVector2;
    }
}
