using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGoods : GoodsModel,OutLookChanger<BulletType>
{
    //获取后更改的子弹类型
    [SerializeField] public BulletType bulletType;
    public override void AfterCollect(PlayerModel collectPlayer)
    {
        GameData.bullet = bulletType; 
        //在销毁前
        base.AfterCollect(collectPlayer);
    }
    void Start()
    {
        //初始化外貌
        ChangeOutLook(bulletType);
    }

    //改变外貌
    public void ChangeOutLook(BulletType type)
    {
        //根据类型更改外观显示
    }
}