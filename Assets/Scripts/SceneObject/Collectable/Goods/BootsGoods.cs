using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsGoods : GoodsModel,OutLookChanger<BootsType>
{
    //获取后更改的子弹类型
    [SerializeField] public BootsType bootsType;
    public override void AfterCollect(PlayerModel collectPlayer)
    {
        GameData.boots = bootsType; 
        //在销毁前
        base.AfterCollect(collectPlayer);
    }

    void Start()
    {
        ChangeOutLook(bootsType);    
    }

    public void ChangeOutLook(BootsType type)
    {
        //根据类型更改外观显示
    }
}