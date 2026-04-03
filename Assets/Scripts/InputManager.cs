using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        //启用角色移动事件
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        //处理角色移动方向
        Vector2 moveVector2 = playerInputActions.Player.Move.ReadValue<Vector2>();
        
        //处理角色移动方向归一化，使得斜角移动不会速度更快
        moveVector2 = moveVector2.normalized;
        Debug.Log(moveVector2);
        return moveVector2;
    }
}
