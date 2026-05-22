using System.Collections.Generic;
using UnityEngine;

public class BulletVfxControler : MonoBehaviour
{
    [Header("关联的拖尾")]
    [SerializeField] public TrailRenderer trailRenderer;//子弹拖尾
    [Header("拖尾颜色检查")]
    [Header("是否开启")]
    [SerializeField] public bool checkColorByDamage = true;//是否依据子弹的伤害设置拖尾颜色种类,否则依据子弹的种类
    [Header("拖尾开启检查")]
    [Header("是否开启")]
    [SerializeField] public bool checkTrail = false;
    [Header("是否使用伤害检查(仅当前一项开启时有效)")]
    [SerializeField] public bool checkTrailByDamage = true; 
    [Header("拖尾控制数据")]
    [SerializeField] public bool openTrail = true;//是否开启拖尾
    [SerializeField] private int trailColorIndex = 0;//拖尾颜色种类
    [SerializeField] public List<Color> trailStartColors = new List<Color>();//拖尾起始颜色列表
    [SerializeField] public List<Color> trailEndColors = new List<Color>();
    //依据子弹的伤害设置拖尾颜色种类
    private Bullet bullet;//关联的子弹,子弹为空时不进行拖尾颜色设置
    void Start()
    {
        //尝试自动获取获取子弹
        if(bullet == null)
            bullet = GetComponent<Bullet>();
    }
    
    void Update()
    {
        if(checkColorByDamage) CheckTrailColorIndexByDamage();
        else CheckTrailColorInddexByBulletType();
        if(checkTrail) 
        {
            if(checkTrailByDamage) CheckOpenTrailByDamage();//检查是否开启拖尾
            else CheckOpenTrailByBulletType();//检查是否开启拖尾
        }
        else {}
        //
        ChangeTrailByData();
    }

    //依据拖尾数据修改拖尾
    private void ChangeTrailByData()
    {
        if(trailRenderer == null) return;
        if(trailEndColors.Count <= trailColorIndex || trailStartColors.Count <= trailColorIndex || trailColorIndex < 0) return;
        Color startColor = trailStartColors[trailColorIndex];
        Color endColor = trailEndColors[trailColorIndex];
        if(startColor!=null && endColor != null)
        {
            if(trailRenderer.startColor != startColor) trailRenderer.startColor = startColor;
            if(trailRenderer.endColor != endColor) trailRenderer.endColor = endColor;
        }
        if(trailRenderer.enabled != openTrail) trailRenderer.enabled = openTrail;
    }

    //颜色修改
    private void CheckTrailColorInddexByBulletType()
    {
        if(bullet == null) return;
        switch(GameData.bullet)
        {
            case BulletType.Bullet_4:
                trailColorIndex = 4;
                break;
            case BulletType.Bullet_3:
                trailColorIndex = 3;
                break;
            case BulletType.Bullet_2:
                trailColorIndex = 2;
                break;
            case BulletType.Bullet_1:
                trailColorIndex = 1;
                break;
            case BulletType.None:
            default:
                trailColorIndex = 0;
                break;
        }
    }

    private void CheckTrailColorIndexByDamage()
    {
        if(bullet == null) return;
        switch(bullet.damage)
        {
            case > 6.0f:
                trailColorIndex = 4;
                break;
            case > 4.0f:
                trailColorIndex = 3;
                break;
            case > 2.0f:
                trailColorIndex = 2;
                break;
            case > 1.0f:
                trailColorIndex = 1;
                break;
            default:
                trailColorIndex = 0;
                break;
        }
    }

    //拖尾开启判断
    private void CheckOpenTrailByBulletType()
    {
        switch(GameData.bullet)
        {
            case BulletType.Bullet_4:
            case BulletType.Bullet_3:
            case BulletType.Bullet_2:
            case BulletType.Bullet_1:
                openTrail = true;
                break;
            case BulletType.None:
            default:
                openTrail = false;
                break;
        }
    }

    private void CheckOpenTrailByDamage()
    {
        switch(bullet.damage)
        {
            case > 1.0f:
                openTrail = true;
                break;
            default:
                openTrail = false;
                break;
        }
    }
}
