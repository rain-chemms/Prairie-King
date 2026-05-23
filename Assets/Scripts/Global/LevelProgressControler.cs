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

    void Update()
    {
        //刷新计时器
        if (nowLevel != null)
        {
            if (!nowLevel.isBossLevel || timeLock)
                timeRecorder += Time.deltaTime;
            if (timeRecorder > nowLevel.GetPersistTime())
            {
                //超时后关闭当前关卡的敌人生成器
                nowLevel.SetGeneratorsActivate<EnermyType, EnermyModel>(false);
                //发出相应的响应
                if (nowLevel?.GetEnermyManager()?.GetList().ToList().Count() <= 0)
                {
                    //关卡计时器重置并锁定时间防止,持续刷新
                    SetTimeLock(true);
                    ResetTimeRecorder();
                    //关卡尾声逻辑
                    if (nowLevel != null)
                    {
                        if (nowLevel.haveMerchant) GenerateMerchant();
                    }
                }
            }

        }
    }

    private void GenerateMerchant()
    {

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
        //设置玩家数据
        LevelProgressControler.instance.SetNowLevel(targetLevelModel);
        targetLevelModel.SetPlayerData(player,playerCamera);
        instance?.StartCoroutine(instance?.StartLevel(0.5f));//0.5s后开始关卡,单例启动携程
        return haveTargetLevel;
    }

    //等待一定时间后重新加载关卡
    IEnumerator StartLevel(float time)
    {
        yield return new WaitForSeconds(time);            
        //激活所有当前关卡的敌人生成器
        LevelProgressControler.instance.GetNowLevel().SetGeneratorsActivate<EnermyType, EnermyModel>(true);
    }
}
