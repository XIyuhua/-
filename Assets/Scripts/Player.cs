using System;
using System.Threading;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance{ get; private set; }

    //这是选中柜台计数器改变事件
    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChanged;


    /// <summary>
    /// 这是选中柜台计数器改变事件参数
    /// </summary>
    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        //选中的柜台计数器
        public BaseCounter selectedCounter;
    }


    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private LayerMask countesLayer;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    //这是柜台的计数器
    private BaseCounter selectedCounter;
    //是否可以移动
    private bool isMoving = false;
    //最后一次射线击中的方向
    private Vector3 lastInteractDir;
    private KitchenObject kitchenObject;



    void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start()
    {
        //将交互对象加入事件监听器，这个事件来自于输入管理器
        inputManager.OnIntercatAction += GameInput_OnIntercatAction;
    }

    /// <summary>
    /// 这是游戏输入事件
    /// </summary>
    /// <param name="sender">这是对象事件者</param>
    /// <param name="e">输入的按键</param>
    private void GameInput_OnIntercatAction(object sender, EventArgs e)
    {
        if(selectedCounter != null)
        {
            //如果有选中的柜台，则进行交互
            selectedCounter?.Interact(this);
        }

        
    }
    
    //角色是否移动，供给其他脚本使用
    private void Update()
    {

        HandleMovement();
        HandleInteractions();
        
    }

    /// <summary>
    /// 处理角色交互
    /// </summary>
    private void HandleInteractions()
    {
        //获取角色移动归一化向量
        Vector2 inputVector = inputManager.GetMovementVectorNormalized();
        //将归一化向量转换为3D向量，人话就是移动的方向向量，模长为1
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        //射线距离
        float interactDistance = 2f;

        //记录玩家最后一次输入的移动方向，并给射线用
        if(moveDir!= Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        //射线检测
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hitInfo, interactDistance, countesLayer))
        {
            //检测碰撞的物体是否有ClearCounter组件
            if (hitInfo.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //进来说明是柜台有柜台脚本，可以进行交互，然后就要更新选中物体了
                //这里的clearCounter是上次选中的柜台，现在碰撞的是这个柜台，说明现在的柜台已经选中了，需要更新选中物体
                if (baseCounter != selectedCounter)
                {
                    //更新选中物体
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                //碰撞的物体没有ClearCounter组件，说明是空地，需要清空选中物体
                SetSelectedCounter(null);
            }
        }
        else
        {
            //没有碰撞，说明是空地，需要清空选中物体
            SetSelectedCounter(null);
        }
    }

    /// <summary>
    /// 处理角色移动
    /// </summary>
    private void HandleMovement()
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

        //碰撞检测(胶囊，底，头，半径，方向，距离)
        //角色是否可以移动
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //角色碰撞了障碍物不能移动,尝试朝x轴或z轴移动
        if (!canMove)
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

    /// <summary>
    /// 更新选中的柜台
    /// </summary>
    /// <param name="selectedCounter">新的柜台是什么</param>
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        //触发选中柜台计数器改变事件
        //这个事件执行之后，selectedCounterVisual脚本内控制柜台特效的方法会随之更新
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangeEventArgs() { selectedCounter = selectedCounter });
    }

    //角色是否移动，供给其他脚本使用
    public bool IsMoving()
    {
        return isMoving;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;

    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    /// <summary>
    /// 检测是否有物品
    /// </summary>
    /// <returns></returns>
    public bool HaskitchenObject()
    {
        return kitchenObject != null;
    }
}
