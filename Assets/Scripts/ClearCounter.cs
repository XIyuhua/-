using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Media;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    //这是柜台上面生成物体的方法
     public override void Interact(Player player)
    {
        //先确定柜台上面是否有食材，如果没有则执行下面代码
        if(!HaskitchenObject())
        {
            //检测玩家手里有没有食材，如果玩家手里有食材，则执行
            if(player.HaskitchenObject())
            {
                //将玩家手里的食材放上当前柜台上面
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            //如果柜台上面有食材并且玩家手里没有食材，则执行下面代码
            if (!player.HaskitchenObject())
            {
                //将柜台上面食材放到玩家手里
                this.GetKitchenObject().SetKitchenObjectParent(player);
            } else {}
        }

    }


}
