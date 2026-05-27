using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 商品包: 可一次性获取多个商品
public class GoodsPack : GoodsModel
{
    [SerializeField] public List<GoodsModel> goodsList; 
    void Start()
    {
        Debug.Log("[GoodsPack] The Value of " + name + " is " + GetCost());
    }

    public override void AfterCollect(PlayerModel collectPlayer)
    {
        foreach(GoodsModel goods in goodsList)
        {
            goods?.AfterCollect(collectPlayer);
        }
    }


    public override uint GetCost()
    {
        uint allCost = base.GetCost();//获取包装盒的花费
        foreach(GoodsModel goods in goodsList)
        {
            allCost += goods.GetCost();
        }
        return allCost;
    }

    public void AddGoods(GoodsModel goods)
    {
        goodsList.Add(goods);
    }

    public void RemoveGoods(GoodsModel goods)
    {
        goodsList.Remove(goods);
    }

    // 获取商品列表:并非自身的引用
    public List<GoodsModel> GetNowGoodsList()
    {
        return goodsList.ToList();
    }
}   