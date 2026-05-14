using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//章节类型:目前设置两章
public enum ChapterType
{
    Chapter_1,
    Chapter_2
}

//关卡类型:目前设置四关+Boss关,关卡中若没有对应LevelType,则默认为Level_1
public enum LevelType
{
    Level_1,
    Level_2,
    Level_3,
    Level_4,
    Level_Boss
}

/*
    三种强化的枚举
        靴子: 增加移速
        子弹: 增加攻击力+更改子弹外观
        枪械升级: 增加攻击速度
*/
//靴子类型: 提升移动速度
public enum BootsType
{
    None,//没有靴子装备
    Boots_1,//靴子等级1
    Boots_2,//靴子等级2
    Boots_3,//靴子等级3
    Boots_4//靴子等级4
}

//子弹类型: 增加攻击力
public enum BulletType
{
    None,//没有子弹装备
    Bullet_1,//子弹等级1
    Bullet_2,//子弹等级2
    Bullet_3,//子弹等级3
    Bullet_4//子弹等级4
}

//枪械升级类型: 增加射速
public enum WeaponUpType
{
    None,//没有枪械装备
    WeaponUp_1,//枪械等级1
    WeaponUp_2,//枪械等级2
    WeaponUp_3,//枪械等级3
    WeaponUp_4//枪械等级4
}

//道具类型: 属于掉落物品,玩家主动激活后触发相应的效果
/*
    玩家捡起时会存入背包中
    若背包内已有道具,则会直接使用
*/
public enum PropType
{
    None,//没有道具
    Wheel,//车轮道具: 一段时间内改为向四周射击
    MachineGun,//机关枪道具: 一段时间内大幅提升攻击速度
    ShotGun,//霰弹枪道具: 一段时间内改为三发射击
    Star,//星星道具: 一段时间内获得 "ShotGun" + "MachineGun" + "Coffee" 效果
    Coffee,//咖啡道具: 一段时间内提升移速
    Tomb,//墓碑道具: 一段时间内变为僵尸,提升移动速度无敌,并可以接触杀死敌人
    Nuclear,//核弹道具: 秒杀除了Boss以外的敌人,对Boss造成大量伤害
    SmokeBomb,//烟雾弹道具: 传送到地形的随机位置,隐身(无敌但是不会接触杀死敌人)
    LifeCoin,//生命币道具: 玩家的剩余命条数+1
    OneCoin,//金币道具: 玩家金币数+1,拾取时触发
    FiveCoin//5元金币道具: 玩家金币数+5,拾取时触发    
}
