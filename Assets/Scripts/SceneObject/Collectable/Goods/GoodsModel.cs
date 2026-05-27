using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 组合模式接口,用于制作商品包
public interface IGoodsComponent
{
    // 获取商品价格（组合模式核心：统一接口）
    uint GetCost();
    // 拾取后的逻辑（组合模式核心：统一操作）
    void AfterCollect(PlayerModel collectPlayer);
}

public class GoodsModel<EnumType> : CollectableObjectModel,IGoodsComponent,EnumGoodsCostSetter<EnumType> where EnumType : Enum
{
    [SerializeField] protected EnumType type;
    [SerializeField] protected uint cost = 0;// 商品价格
    public virtual uint GetCost()
    {
        return cost;
    }
    public void SetCost(uint cost)
    {
        this.cost = cost;
    }
    public void SetSelfCost(uint cost)
    {
        this.cost = cost;
    }
    //商品外观切换控制器列表
    [SerializeField] public List<EnumMaterialChanger<EnumType>> materialChangerList = new List<EnumMaterialChanger<EnumType>>();
    //触发商品外观切换
    public void ChangeOutLook(EnumType type)
    {
        foreach(EnumMaterialChanger<EnumType> changer in materialChangerList)
        {
            changer?.ChangeOutLook(type);
        }
    }
    
    public override void AfterCollect(PlayerModel collectPlayer)
    {
        // 商品在拾取后销毁自身前的函数
        if(GameData.money < cost) return;
        GameData.money -= cost;
        base.AfterCollect(collectPlayer);
        //商品在拾取后销毁自身
        Destroy(gameObject);
    }
    //依据EnumType设置商品价格
    //只有重写了这个方法的类才能在alwaysCheckCostByType时有效
    public virtual void SetCost(EnumType type)
    {
        Debug.Log("[GoodsModel<"+ typeof(EnumType).ToString() +">] Set cost by Type:" + type.ToString());
    }

    // 商品销毁后执行的函数
    protected virtual void OnDestroy()
    {} 

    protected void Awake()
    {
        //依据类型设置商品价格
        SetCost(type);
    }
    protected void Start()
    {
        //改变商品外貌
        ChangeOutLook(type);
    }
}
