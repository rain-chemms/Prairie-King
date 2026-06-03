using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//关卡进度控制器
public class LevelProgressControler : MonoBehaviour//,MerchantGenerator
{
    public static LevelProgressControler instance;
    [SerializeField] private LevelModel nowLevel;
    public LevelModel GetNowLevel()
    {
        return nowLevel;
    }
    public void SetNowLevel(LevelModel level)
    {
        nowLevel = level;
    }
    private void Awake()
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

    //关卡计时器
    //UI组件可以读取关卡进度控制器的数据进行UI更新
    [SerializeField] private float timeRecorder = 0;
    public float GetTimeRecorder()
    {
        return timeRecorder;
    }

    public void SetTimeRecorder(float time)
    {
        timeRecorder = time;
    } 
    [SerializeField] private bool timeLock = false;//关卡计时器是否被锁定
    public void SetTimeLock(bool isLock)
    {
        timeLock = isLock;
    }
    //重置计时器
    public void ResetTimeRecorder()
    {
        timeRecorder = 0;
    }

    void Start()
    {
        ResetTimeRecorder();
    }

    private bool nowLevelHaveSaved = false;
    private void SaveDataToGameData()
    {
        if(nowLevel != null && !nowLevelHaveSaved)
        {
            GameData.level = nowLevel.level;
            nowLevelHaveSaved = true;
        }
    }

    void Update()
    {
        //刷新计时器
        if (nowLevel != null)
        {
            if (!nowLevel.isBossLevel && !timeLock)
                timeRecorder += Time.deltaTime;
            if (timeRecorder > nowLevel.GetPersistTime())//只进行时间检测
            {
                //超时后关闭当前关卡的敌人生成器
                nowLevel.SetGeneratorsActivate<EnermyType, EnermyModel>(false);
            }
            
            if (timeRecorder > nowLevel.GetPersistTime() || (bool)nowLevel?.isBossLevel)//进行时间和Boss关检测检测,Boss关只需要打败Boss即可
            {        
                //发出相应的响应
                if (nowLevel?.GetEnermyManager()?.GetList().ToList().Count() <= 0)
                {
                    //关卡计时器重置并锁定时间防止,持续刷新
                    SetTimeLock(true);
                    ResetTimeRecorder();
                    //开启下一关加载器
                    nowLevel?.GetNextLevelLoader()?.SetOpen(true);
                    nowLevel?.GetNextLevelLoader()?.OpenTrapPart();//打开所有关闭的机关
                    //关卡尾声逻辑
                    if (nowLevel != null)
                    {
                        if (nowLevel.haveMerchant) GenerateMerchant();
                    }
                    AudioManager.instance.StopBgm();
                }
                else
                {
                    AudioManager.instance.PlayBgm();    
                }
            }
        }
    }
    [SerializeField] public Merchant merchantPrefab;
    private void GenerateMerchant()
    {
        Merchant newMerchant = Instantiate(merchantPrefab);
        newMerchant.SetIdlePlace((Vector3)nowLevel?.GetMerchantIdlePoint().position);
        newMerchant.SetSalePlace((Vector3)nowLevel?.GetMerchantSalePoint().position);
        newMerchant.transform.position = newMerchant.GetIdlePlace();//生成位置初始化
        newMerchant.gameObject.SetActive(true);//激活物体
        newMerchant.SetSaleState(true);//商店状态打开
    }
    
    public static bool LoadLevel(PlayerModel player,PlayerCameraMover playerCamera,uint levelIndex = 1)
    {
        if(player == null || playerCamera == null) return false;
        List<LevelModel> levels = FindObjectsOfType<LevelModel>().ToList();
        if(levels == null || levels.Count <= 0) return false;
        bool haveTargetLevel = false;
        uint minLevel = uint.MaxValue;
        LevelModel targetLevelModel = null;
        foreach(LevelModel levelModel in levels)
        {
            if(levelModel.level < minLevel) 
            {
                minLevel = levelModel.level;
                targetLevelModel = levelModel;
            }
            if(levelModel.level == levelIndex)
            {
                haveTargetLevel = true;
                targetLevelModel = levelModel;
                break;    
            }
        }
        
        if(!haveTargetLevel) Debug.LogWarning("[LevelProgressControler]:Not Find Level:{" + levelIndex + "}" + ", Have already jump To First Level");
        if(targetLevelModel == null){Debug.LogWarning("[LevelProgressControler]:Not have Level In this Scene!");return false;}
        //关闭所有Trap
        instance.nowLevel?.GetNextLevelLoader()?.CloseTrapPart();//关闭所有打开的机关
        //设置玩家数据
        LevelProgressControler.instance.SetNowLevel(targetLevelModel);
        //关闭当前关卡的下一关加载器
        LevelProgressControler.instance?.GetNowLevel()?.GetNextLevelLoader()?.SetOpen(false);
        targetLevelModel.SetPlayerData(player,playerCamera);
        //刷新敌人生成器的状态
        LevelProgressControler.instance?.GetNowLevel()?.FreshAllEnermyGenerateListListState();
        
        //保存当前关卡数据
        LevelProgressControler.instance.SaveDataToGameData();
        LevelProgressControler.instance.nowLevelHaveSaved = false;//关卡数据保存状态重置
        instance?.StartCoroutine(instance?.StartLevel(2.0f));//2.0s后开始关卡,单例启动携程
        return haveTargetLevel;
    }

    //等待一定时间后重新加载关卡
    IEnumerator StartLevel(float time)
    {            
        yield return new WaitForSeconds(time);            
        //激活所有当前关卡的敌人生成器
        ResetTimeRecorder();
        SetTimeLock(false);//解锁时间
        LevelProgressControler.instance.GetNowLevel().SetGeneratorsActivate<EnermyType, EnermyModel>(true);
        //设置BGM
        bool isBoss = LevelProgressControler.instance.GetNowLevel().isBossLevel;
        if(isBoss)
        {
            switch(GameData.chapter)
            {
                case ChapterType.Chapter_3:
                    AudioManager.instance.ChangeBgm("LastBoss");
                    break;
                case ChapterType.Chapter_1:
                case ChapterType.Chapter_2:
                default:
                    AudioManager.instance.ChangeBgm("OutLawBoss");
                    break;
            }
        }
        else
        {
            AudioManager.instance.ChangeBgm("Normal");
        }
        AudioManager.instance.PlayBgm();//播放BGM
    }
}
