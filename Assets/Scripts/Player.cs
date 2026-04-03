using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float moveSpeed = 5f;
    private bool isMoving = false;

    private void Update()
    {
        //获取角色移动归一化向量
        Vector2 inputVector = inputManager.GetMovementVectorNormalized();
        //将归一化向量转换为3D向量，人话就是移动的方向向量，模长为1
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        //角色的高度
        float playerHeight = 2f;
        //角色的半径
        float playerRadius = 0.7f;
        //角色移动距离
        float moveDistance = moveSpeed * Time.deltaTime;

        //碰撞检测
        //角色是否可以移动
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //角色碰撞了障碍物不能移动,尝试朝x轴或z轴移动
        if(!canMove)
        {
            //角色不能移动,向x轴试一下，记得归一化
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            //如果x轴可以移动,则朝x轴移动
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                //如果x轴也不能移动,则朝z轴检测试试，记得归一化
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                //如果z轴可以移动,则朝z轴移动
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }
        //没有碰撞则移动
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        

        //处理角色跟随移动朝向
        //forword = 朝向
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * 10f);


        //角色是否移动
        isMoving = inputVector != Vector2.zero;
    }
    
    //角色是否移动，供给其他脚本使用
    public bool IsMoving()
    {      
        return isMoving;
    }
}
    