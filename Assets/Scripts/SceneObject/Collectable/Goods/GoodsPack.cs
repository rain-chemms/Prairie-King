using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 商品包: 可一次性获取多个商品
public class GoodsPack : MonoBehaviour,IGoodsComponent
{
    [SerializeField] public uint cost;// 包装盒的价格
    [SerializeField] public List<IGoodsComponent> goodsList; 
    void Start()
    {
        Debug.Log("[GoodsPack] The Value of " + name + " is " + GetCost());
    }

    public void AfterCollect(PlayerModel collectPlayer)
    {
        foreach(IGoodsComponent goods in goodsList)
        {
            goods?.AfterCollect(collectPlayer);
        }
    }


    public uint GetCost()
    {
        uint allCost = this.cost;//获取包装盒的花费
        foreach(IGoodsComponent goods in goodsList)
        {
            allCost += goods.GetCost();
        }
        return allCost;
    }

    public void AddGoods(IGoodsComponent goods)
    {
        goodsList.Add(goods);
    }

    public void RemoveGoods(IGoodsComponent goods)
    {
        goodsList.Remove(goods);
    }

    // 获取商品列表:并非自身的引用
    public List<IGoodsComponent> GetNowGoodsList()
    {
        return goodsList.ToList();
    }
}   