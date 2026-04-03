using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    //这是柜台上面生成物体的方法
    public override void Interact(Player player)
    {
        //判断是否已经有食材在玩家手里 
        if (!player.HaskitchenObject())
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.localPosition = Vector3.zero;
            //将生成的食材放入玩家手里
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            //播放动画
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }

    }

}
