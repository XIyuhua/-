using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 橱柜与食物的父类接口
/// </summary>
public interface IKitchenObjectParent
{

    /// <summary>
    /// 设置食材跟随的位置（一般是移动）
    /// </summary>
    /// <returns>返回需要移动的位置</returns>
    public Transform GetKitchenObjectFollowTransform();

    /// <summary>
    /// 设置柜台上的食材
    /// </summary>
    /// <param name="kitchenObject">食材</param>
    public void SetKitchenObject(KitchenObject kitchenObject);

    /// <summary>
    /// 获取当前柜台上食材
    /// </summary>
    /// <returns></returns>
    public KitchenObject GetKitchenObject();

    /// <summary>
    /// 清空柜台
    /// </summary>
    public void ClearKitchenObject();

    /// <summary>
    /// 检测柜台是否有物品
    /// </summary>
    /// <returns></returns>
    public bool HaskitchenObject();
}
