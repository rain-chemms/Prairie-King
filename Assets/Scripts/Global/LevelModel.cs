using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEditor.SearchService;
using UnityEngine;


public class LevelModel : AbstractModel
{
    [SerializeField] public uint level = 0;//当前关卡的索引
    [SerializeField] public bool haveMerchant = false;//关卡结束后是否生成商人
    [SerializeField] protected CameraAnchor cameraAnchor;
    [SerializeField] protected PlayerAnchor playerAnchor;

    void Start()
    {
        //自动获取锚点
        if(!SearchCameraAnchor()) Debug.LogWarning("[LevelModel]:Not Find Level's CameraAnchor!Please Set CameraAnchor Object");
        if(!SearchPlayerAnchor()) Debug.LogWarning("[LevelModel]:Not Find Level's PlayerAnchor!Please Set PlayerAnchor Object");
    }
    //加载关卡静态函数
    public static bool LoadLevel(PlayerModel player,PlayerCameraMover playerCamera,uint levelIndex = 1)
    {
        if(player == null || playerCamera == null) return false;
        List<LevelModel> levels = FindObjectsOfType<LevelModel>().ToList();
        if(levels == null || levels.Count <= 0) return false;
        bool haveTargetLevel = false;
        uint minLevel = uint.MaxValue;
        LevelModel minLevelModel = null;
        foreach(LevelModel levelModel in levels)
        {
            if(levelModel.level < minLevel) 
            {
                minLevel = levelModel.level;
                minLevelModel = levelModel;
            }
            if(levelModel.level == levelIndex)
            {
                //设置玩家数据
                levelModel.SetPlayerData(player,playerCamera);
                haveTargetLevel = true;
                break;    
            }
        }
        if(!haveTargetLevel)
        {
            minLevelModel.SetPlayerData(player,playerCamera);
            Debug.LogWarning("[LevelModel]:Not Find Level:{" + levelIndex + "}" + ", Have already jump To First Level");
        }
        return haveTargetLevel;
    }

    protected void SetPlayerData(PlayerModel player,PlayerCameraMover cameraMover)
    {
        if(player!=null && cameraMover!=null)
        {
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

    //设置特定类型的生成器的可见性
    public void SetGeneratorsActivate<Generator,Source,Product>(bool isActivate) 
        where Source : Enum 
        where Product : MonoBehaviour
        where Generator : AbstractGenerateList<Source,Product>
    {
        List<AbstractGenerateList<Source,Product>> abstractGenerateLists = GetComponentsInChildren<AbstractGenerateList<Source,Product>>().ToList();
        if(abstractGenerateLists!=null)
        {
            foreach(AbstractGenerateList<Source,Product> generateList in abstractGenerateLists)
            {
                generateList.SetActivate(isActivate);
            }
        }
    }
}
