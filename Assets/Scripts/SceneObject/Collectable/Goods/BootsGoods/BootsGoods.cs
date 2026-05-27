using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsGoods : GoodsModel<BootsType>
{    
    //获取后更改靴子类型
    public override void AfterCollect(PlayerModel collectPlayer)
    {
        if(GameData.money < cost) return;
        GameData.boots = type; 
        //在销毁前
        base.AfterCollect(collectPlayer);
    }
    new protected void Awake()
    {
        base.Awake();
    }

    new protected void Start()
    {
        base.Start();        
    }
    // 设置价格生效器
    public override void SetCost(BootsType type)
    {
        switch(type)
        {
            case BootsType.Boots_2:
                cost = 15;
                break;
            case BootsType.Boots_1:
                cost = 7;
                break;
            case BootsType.None://哪个傻子钱多的买这个,可以做成就或彩蛋
            default:
                cost = 999;
                break;
        }
    } 
}