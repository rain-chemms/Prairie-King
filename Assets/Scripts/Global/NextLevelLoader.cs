using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NextLevelLoader : MonoBehaviour
{
    //下一关的索引
    [SerializeField] public uint nextLevel = 1;
    [SerializeField] private PlayerCameraMover cameraMover;
    private List<Collider> colliders = new List<Collider>();
    void Start()
    {
        //尝试获取相机移动器
        if (cameraMover == null)
        {
            cameraMover = FindObjectOfType<PlayerCameraMover>();
        }
        RefreshColliders();
        //获取LevelTraps
        traps = traps = FindObjectsOfType<MonoBehaviour>().Where(c => c is LevelTrap).Select(c => c as LevelTrap).ToList();
    }

    public void RefreshColliders()
    {
        //自动获取所有子碰撞区域
        colliders.Clear();
        foreach (Collider child in GetComponentsInChildren<Collider>())
        {
            colliders.Add(child);
        }
    }

    [SerializeField] public bool isOpen = false;
    //设置是否开启
    public void SetOpen(bool open)
    {
        isOpen = open;
    }

    void Update()
    {
        //开启时激活所有Collider组件
        foreach (Collider collider in colliders)
        {
            if(collider.enabled != isOpen) collider.enabled = isOpen;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //保险检测
        if(!isOpen) return;
        //检测是否进入
        PlayerModel player = other.GetComponent<PlayerModel>();
        if(player != null && cameraMover != null)
        {
            isOpen = false;//关闭加载器,防止重复加载
            CloseTrapPart();
            //保存加载数据
            GameData.SaveSystem.SaveGame();
            LevelProgressControler.LoadLevel(player,cameraMover,nextLevel);//加载下一关
        }
    }

    [SerializeField] public List<LevelTrap> traps = new List<LevelTrap>();
    //打开挡路的区域和机关装置
    public void OpenTrapPart()
    {
        foreach (LevelTrap trap in traps)
        {
            trap?.Open();
        }
    }

    //关闭挡路的区域和机关装置
    public void CloseTrapPart()
    {
        foreach (LevelTrap trap in traps)
        {
            trap?.Close();
        }
    }
}
