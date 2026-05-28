using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpGoods : GoodsModel<WeaponUpType>
{
    //获取后更改的武器升级类型
    public override void AfterCollect(PlayerModel collectPlayer)
    {
        if(GameData.money < cost) return;
        GameData.weaponUp = type; 
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
    public override void SetCost(WeaponUpType type)
    {
        switch(type)
        {
            
            case WeaponUpType.WeaponUp_2:
                cost = 20;
                break;
            case WeaponUpType.WeaponUp_3:
                cost = 30;
                break;
            case WeaponUpType.WeaponUp_4:
                cost = 40;
                break;
            case WeaponUpType.WeaponUp_1:
                cost = 10;
                break;
            case WeaponUpType.None://哪个傻子钱多的买这个,可以做成就或彩蛋
            default:
                cost = 999;
                break;
        }
    }   
}