using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//该脚本用于存放玩家的游玩数据,可由外界读取
public static class GameData 
{
    public static ChapterType chapter = ChapterType.Chapter_1;//玩家所在的章节
    public static LevelType level = LevelType.Level_1;//玩家所在章节的地几关卡
    //上面两个变量用于初始化确认玩家的生成位置+摄像机的放置位置
    //玩家持有的道具
    public static PropType prop = PropType.None;
    public static uint money = 0;//玩家的金币数
    //玩家持有的升级情况
    public  static BootsType boots = BootsType.None;//靴子等级
    public static WeaponUpType weaponUp = WeaponUpType.None;//武器升级
    public static BulletType bullet = BulletType.None;//子弹等级,None代表未强化
}
