using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//单例加载器
public class ChapterControler : MonoBehaviour
{
    public static ChapterControler instance;
    [SerializeField] public PlayerModel playerPrefab;
    [SerializeField] public PlayerCameraMover cameraMoverPrefab;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //加载相应名称的场景
    public virtual void LoadChapter(ChapterType chapter)
    {
        switch(chapter)
        {
            case ChapterType.Chapter_3:
                SceneLoader.instance.Load("Chapter3Scene",AfterChapterSceneLoad);//加载场景
                break;
            case ChapterType.Chapter_2:
                SceneLoader.instance.Load("Chapter2Scene",AfterChapterSceneLoad);//加载场景
                break;
            case ChapterType.Chapter_1:
            default:
                SceneLoader.instance.Load("Chapter1Scene",AfterChapterSceneLoad);//加载场景
                break;
        }
    }
    
    //加载场景后加载关卡
    private void AfterChapterSceneLoad()
    {
        //获取玩家
        PlayerModel player = FindObjectOfType<PlayerModel>()?.GetComponent<PlayerModel>();
        if(player == null)
        {
            player = Instantiate(playerPrefab,null);
        }
        //获取摄像机移动器
        PlayerCameraMover cameraMover = FindObjectOfType<PlayerCameraMover>()?.GetComponent<PlayerCameraMover>();
        if(cameraMover == null)
        {
            cameraMover = Instantiate(cameraMoverPrefab,null);
        }
        Debug.Log("[ChapterControler] Set Player: " + player?.name + "| CameraMover: " + cameraMover?.name);
        //加载相应关卡
        LevelProgressControler.LoadLevel(player,cameraMover,GameData.level);
        //显示战斗UI
        GameDataUI.instance.SetDisplay(true);
    }

    //加载游戏数据中的场景
    public void LoadGameDataChapter()
    {
        LoadChapter(GameData.chapter);
    }
}
