using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataGetter : MonoBehaviour
{
    [SerializeField] public ChapterType chapter = ChapterType.Chapter_1;//玩家所在的章节
    [SerializeField] public LevelType level = LevelType.Level_1;//玩家所在章节的地几关卡
    //上面两个变量用于初始化确认玩家的生成位置+摄像机的放置位置
    //玩家持有的道具
    [SerializeField] public PropType prop = PropType.None;
    [SerializeField] public uint life = 3;//玩家剩余生命条数,死亡时本关重打,该数值减1;该小于等于0时角色死亡游戏结束
    [SerializeField] public uint money = 0;//玩家的金币数
    //玩家持有的升级情况
    [SerializeField] public BootsType boots = BootsType.None;//靴子等级
    [SerializeField] public WeaponUpType weaponUp = WeaponUpType.None;//武器升级
    [SerializeField] public BulletType bullet = BulletType.None;//子弹等级,None代表未强化

    void Update()
    {
        FreshGameData();
    }

    public void FreshGameData()
    {
        chapter = GameData.chapter;
        level = GameData.level;
        prop = GameData.prop;
        life = GameData.life;
        money = GameData.money;
        boots = GameData.boots;
        weaponUp = GameData.weaponUp;
        bullet = GameData.bullet;    
    }
}
