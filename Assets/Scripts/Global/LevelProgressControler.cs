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
        if(instance == null)
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
        if(!nowLevel.isBossLevel || timeLock)
            timeRecorder += Time.deltaTime;
        if(timeRecorder > nowLevel.GetPersistTime())
        {
            //超时后关闭当前关卡的敌人生成器
            nowLevel.SetGeneratorsActivate<EnermyType,EnermyModel>(false);
            //发出相应的响应
            if(nowLevel?.GetEnermyManager()?.GetList().ToList().Count() <= 0)
            {
                //关卡计时器重置并锁定时间防止,持续刷新
                SetTimeLock(true);
                ResetTimeRecorder();
                //关卡尾声逻辑
                if(nowLevel!=null)
                {
                    if(nowLevel.haveMerchant) GenerateMerchant();          
                }
            }
        }
    }

    private void GenerateMerchant()
    {
        
    }
}
