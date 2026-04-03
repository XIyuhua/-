using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent KitchenObjectParent;


    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    /// <summary>
    /// 设置食材的柜台
    /// </summary>
    /// <param name="KitchenObjectParent"></param>
    public void SetKitchenObjectParent(IKitchenObjectParent KitchenObjectParent)
    {
        //如果当前食材已经有柜台，则将当前食材从原来的柜台中清除
        if (this.KitchenObjectParent != null)
        {
            this.KitchenObjectParent.ClearKitchenObject();
        }

        //将传递过来的柜台设置为食材现在的柜台（数据上）
        this.KitchenObjectParent = KitchenObjectParent;
        if (KitchenObjectParent.HaskitchenObject())
        {
            Debug.LogError("当前玩家已经有食材了！");
        }

        //将食材添加到新的柜台中（传递过来的柜台）
        KitchenObjectParent.SetKitchenObject(this);

        //将食材移动到新的柜台的位置（视觉上）
        transform.parent = KitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;

    }
    
    /// <summary>
    /// 获取食材的柜台
    /// </summary>
    /// <returns></returns>
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return KitchenObjectParent;
    }
}
