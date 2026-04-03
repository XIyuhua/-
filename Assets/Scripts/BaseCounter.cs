using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform CounterTopPosition;
    //柜台是否有物体
    private KitchenObject kitchenObject;

    public virtual void Interact(Player player) { Debug.LogError("BaseCounter.Interact没有实现);"); }

    /// <summary>
    /// 转移食材到另外一个柜台或者玩家
    /// </summary>
    /// <returns></returns>
    public Transform GetKitchenObjectFollowTransform()
    {
        return CounterTopPosition;

    }

    /// <summary>
    /// 设置食材的父级
    /// </summary>
    /// <param name="kitchenObject"></param>
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    /// <summary>
    /// 获取食材
    /// </summary>
    /// <returns></returns>
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    /// <summary>
    /// 清空食材
    /// </summary>
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    /// <summary>
    /// 检测是否有食材
    /// </summary>
    /// <returns></returns>
    public bool HaskitchenObject()
    {
        return kitchenObject != null;
    }

}