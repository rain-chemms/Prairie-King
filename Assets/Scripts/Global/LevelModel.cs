using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;


public class LevelModel : AbstractModel
{
    [SerializeField] public uint level = 0;//当前关卡的索引
    [SerializeField] public bool haveMerchant = false;//关卡结束后是否生成商人
    [SerializeField] public float levelPersistTime = 0.0f;//关卡持续时间
    public void SetPersistTime(float time)
    {
        levelPersistTime = time;
    }
    public float GetPersistTime()
    {
        return levelPersistTime;
    }

    [SerializeField] public bool isBossLevel = false;//是否是Boss关
    [SerializeField] protected CameraAnchor cameraAnchor;
    [SerializeField] protected PlayerAnchor playerAnchor;
    [SerializeField] protected NextLevelLoader nextLevelLoader;//下一关加载器
    public void SetNextLevelLoader(NextLevelLoader nextLevelLoader)//设置下一关加载器
    {
        this.nextLevelLoader = nextLevelLoader;
    }
    public NextLevelLoader GetNextLevelLoader()//获取下一关加载器
    {
        return nextLevelLoader;
    }
    [SerializeField] protected EnermyManager enermyManager;//敌人管理器
    public void SetEnermyManager(EnermyManager enermyManager)
    {
        this.enermyManager = enermyManager;
    }
    public EnermyManager GetEnermyManager()
    {
        return enermyManager;
    }

    void Start()
    {
        //自动获取锚点
        if(!SearchCameraAnchor()) Debug.LogWarning("[LevelModel]:Not Find Level's CameraAnchor!Please Set CameraAnchor Object");
        if(!SearchPlayerAnchor()) Debug.LogWarning("[LevelModel]:Not Find Level's PlayerAnchor!Please Set PlayerAnchor Object");
        if(!SearchEnermyManager()) Debug.LogWarning("[LevelModel]:Not Find Level's EnermyManager!Please Set EnermyManager Object");
        else SetEnermyManagerToEnermyGenerator();
    }
    //加载关卡静态函数
    public void SetPlayerData(PlayerModel player,PlayerCameraMover cameraMover)
    {
        if(player!=null && cameraMover!=null)
        {
            if(player.rb != null){
                player.rb.useGravity = true;//重启重力
                player.OpenAllCollider();//激活所有碰撞器
            }
            if(playerAnchor != null)
                player.transform.position = playerAnchor.transform.position;//设置玩家初始位置未角色锚点
            cameraMover.SetTarget(cameraAnchor.transform);//设置相机锚点跟随
        }        
    }

    protected bool SearchCameraAnchor()
    {
        List<CameraAnchor> cameraAnchors = new List<CameraAnchor>();
        cameraAnchors = GetComponentsInChildren<CameraAnchor>().ToList();
        //使用第一个匹配的CameraAnchor
        if(cameraAnchors == null || cameraAnchors.Count <= 0) return false;
        cameraAnchor = cameraAnchors[0];
        return true;
    }

    protected bool SearchPlayerAnchor()
    {
        List<PlayerAnchor> playerAnchors = new List<PlayerAnchor>();
        playerAnchors = GetComponentsInChildren<PlayerAnchor>().ToList();
        //使用第一个匹配的PlayerAnchor
        if(playerAnchors == null || playerAnchors.Count <= 0) return false;
        playerAnchor = playerAnchors[0];
        return true;        
    }

    protected bool SearchEnermyManager()
    {
        List<EnermyManager> enermyManagers = new List<EnermyManager>();
        enermyManagers = GetComponentsInChildren<EnermyManager>().ToList();
        //使用第一个匹配的EnermyManager
        if(enermyManagers == null || enermyManagers.Count <= 0) return false;
        enermyManager = enermyManagers[0];
        return true;
    }

    protected void SetEnermyManagerToEnermyGenerator()
    {
        List<EnermyGenerator> enermyGenerators = GetComponentsInChildren<EnermyGenerator>().ToList();
        if(enermyGenerators != null && enermyManager!=null)
        {
            foreach(EnermyGenerator generator in enermyGenerators)
            {
                generator.SetProductManager(enermyManager);
            }
        }
    }

    //设置特定类型的生成器的激活状态:自动进行检测
    public void SetGeneratorsActivate<Source,Product>(bool isActivate) //传入参数代表要关闭的生成器的类别
        where Source : Enum 
        where Product : MonoBehaviour
    {
        List<AbstractGenerateList<Source,Product>> abstractGenerateLists = GetComponentsInChildren<AbstractGenerateList<Source,Product>>().ToList();
        if(abstractGenerateLists!=null)
        {
            foreach(AbstractGenerateList<Source,Product> generateList in abstractGenerateLists)
            {
                generateList.SetActivate(isActivate);
            }
        }
        Debug.Log("[LevelModel]:Set Generators Activate:"+isActivate+" | List Size:"+abstractGenerateLists.Count);
    }
    [SerializeField] public Transform merchantSalePoint;
    public void SetMerchantSalePoint(Transform merchantSalePoint)
    {
        this.merchantSalePoint = merchantSalePoint;
    }
    public Transform GetMerchantSalePoint()
    {
        return merchantSalePoint;
    }
    [SerializeField] public Transform merchantIdlePoint;
    public void SetMerchantIdlePoint(Transform merchantIdlePoint)
    {
        this.merchantIdlePoint = merchantIdlePoint;
    }
    public Transform GetMerchantIdlePoint()
    {
        return merchantIdlePoint;
    }
    [SerializeField] public List<EnermyGenerateList> enermyGenerateListList;
    public List<EnermyGenerateList> GetEnermyGenerateList()
    {
        return enermyGenerateListList;
    }
    public void FreshAllEnermyGenerateListListState()
    {
        if(enermyGenerateListList == null || enermyGenerateListList.Count <= 0) return;
        foreach(EnermyGenerateList generator in enermyGenerateListList)
        {
            //重置生成器时间并将其开启
            generator?.ResetTime();
            //generator?.SetActivate(true);
            generator?.ResetIsGenerateList();
        }
    }
}
