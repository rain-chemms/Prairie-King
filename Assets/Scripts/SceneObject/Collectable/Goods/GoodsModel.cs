using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsModel : CollectableObjectModel
{
    [SerializeField] protected uint cost = 0;// 商品价格
    public virtual uint GetCost()
    {
        return cost;
    }
    public void SetSelfCost(uint cost)
    {
        this.cost = cost;
    }

    public override void AfterCollect(PlayerModel collectPlayer)
    {
        // 商品在拾取后销毁自身前的函数
        base.AfterCollect(collectPlayer);
        if(GameData.money < cost) return;
        GameData.money -= cost;
        //商品在拾取后销毁自身
        Destroy(gameObject);
    }

    // 商品销毁后执行的函数
    protected virtual void OnDestroy()
    {} 
}
