using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapenUpGoods : GoodsModel,OutLookChanger<WeaponUpType>
{
    //获取后更改的子弹类型
    [SerializeField] public WeaponUpType weaponUpType;
    public override void AfterCollect(PlayerModel collectPlayer)
    {
        GameData.weaponUp = weaponUpType; 
        //在销毁前
        base.AfterCollect(collectPlayer);
    }
    void Start()
    {
        ChangeOutLook(weaponUpType);
    }

    public void ChangeOutLook(WeaponUpType type)
    {
        //根据类型更改外观显示
    }
}