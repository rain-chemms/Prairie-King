using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGoods : GoodsModel<BulletType>
{
    //获取后更改的子弹类型
    public override void AfterCollect(PlayerModel collectPlayer)
    {
        if(GameData.money < cost) return;
        GameData.bullet = type; 
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
    public override void SetCost(BulletType type)
    {
        switch(type)
        {

            case BulletType.Bullet_2:
                cost = 30;
                break;
            case BulletType.Bullet_3:
                cost = 45;
                break;
            case BulletType.Bullet_4:
                cost = 60;
                break;
            case BulletType.Bullet_1:
                cost = 15;
                break;
            case BulletType.None://哪个傻子钱多的买这个,可以做成就或彩蛋
            default:
                cost = 999;
                break;
        }
    }   

}