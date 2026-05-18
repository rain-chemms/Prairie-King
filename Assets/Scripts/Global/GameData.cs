using System;
using System.IO;
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
    public static uint life = 3;//玩家剩余生命条数,死亡时本关重打,该数值减1;该小于等于0时角色死亡游戏结束
    public static uint money = 0;//玩家的金币数
    //玩家持有的升级情况
    public  static BootsType boots = BootsType.None;//靴子等级
    public static WeaponUpType weaponUp = WeaponUpType.None;//武器升级
    public static BulletType bullet = BulletType.None;//子弹等级,None代表未强化

    public static class SaveSystem
    {
        internal class SaveData
        {
            // 对应 GameData 中的静态变量
            public ChapterType chapter;
            public LevelType level;
            public PropType prop;
            public uint life;
            public uint money;
            public BootsType boots;
            public WeaponUpType weaponUp;
            public BulletType bullet;
            public SaveData()
            {
                // 将当前游戏数据复制到存档对象中
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

        //存档系统
        public const string savePath = "Save/";
        public const string saveName = "Save.json"; 
        public static bool SaveGame()
        {
            SaveData saveData = new SaveData();
            if(saveData == null) return false;
            string json = JsonUtility.ToJson(saveData, true);
            //将路径归于文件下
            //路径:
            //游戏文件夹:
            //     SaveFile
            //          SaveData1.json
            String realPath = Path.Combine(Application.persistentDataPath,savePath,saveName);
            try
            {
                //写入文件,自动创建对应路径文件夹
                if (!Directory.Exists(realPath))
                {
                    Directory.CreateDirectory(realPath);
                    Debug.LogWarning("[GameData SaveSystem]:Not Find Save Directory, Directory Created! Path: " + realPath);
                }
                File.WriteAllText(realPath, json);
                Debug.Log("[GameData SaveSystem]:Save Successful! The Save Path:" + realPath);
                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError("[GameData SaveSystem]:Save Faild! Error Messaage:" + e.Message);
                return false;
            }
        }

        public static bool LoadGame()
        {
            String loadPath = Path.Combine(Application.persistentDataPath,savePath,saveName);
            if (File.Exists(loadPath))
            {
                try
                {
                    // 1. 读取文件内容
                    string json = File.ReadAllText(loadPath);
                    // 2. 反序列化 JSON 为 SaveData 对象
                    SaveData data = JsonUtility.FromJson<SaveData>(json);
                    // 3. 将数据回写到静态变量中
                    chapter = data.chapter;
                    level = data.level;
                    prop = data.prop;
                    life = data.life;
                    money = data.money;
                    boots = data.boots;
                    weaponUp = data.weaponUp;
                    bullet = data.bullet;
                    Debug.Log("[GameData SaveSystem]:Read Successful!");
                    return true;
                }
                catch (System.Exception e)
                {
                    Debug.LogError("[GameData SaveSystem]:Read Faild! Error Messaage:" + e.Message);
                    return false;
                }
            }
            else
            {
                Debug.LogWarning("[GameData SaveSystem]:Not Find File:{" + loadPath + "}");
                return false;
            }
        }
    }
}
