using UnityEngine;

public class GameMachine : MonoBehaviour
{
    //继续游戏
    public static void ContinueGame()
    {
        GameData.SaveSystem.LoadGame();//加载游戏数据
        ChapterControler.instance.LoadChapter(GameData.chapter);//加载章节
    }

    //开始游戏
    public static void StartNewGame()
    {
        GameData.ResetData();//重置数据
        ChapterControler.instance.LoadChapter(GameData.chapter);//加载章节
    }

    //退出游戏机器
    public static void ExitGameMachine()
    {
        //返回到上一个场景
    }
}
