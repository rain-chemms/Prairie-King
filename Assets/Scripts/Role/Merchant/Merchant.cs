using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Merchant : RoleModel
{
    [SerializeField] public bool isSale = true;
    [SerializeField] public Vector3 salePlace;//售货处,isSale为true时移动到该处并售卖物品
    [SerializeField] public Vector3 idlePlace;//闲逛处,isSale为false时收起商品并移动到该处
    [SerializeField] public Animator animator;//动画器
    [SerializeField] public Vector3 GoodsStartPosition;//商品起始位置
    [SerializeField] public float goodsInterval = 0.5f;//商品间隔
    [SerializeField] private bool isArrived = false;
    void Start()
    {
        isSale = true;
        CloseGoodsList();
        GenerateGoodsByGameData();
    }

    new void Update()
    {
        base.Update();
        if (isArrived)
        {
            if (isSale)
            {
                ShowGoodsList();
            }
            else
            {
                CloseGoodsList();
            }
        }
        else
        {
            CloseGoodsList();
        }
    }

    void FixedUpdate()
    {
        float arrivalThreshold = 1f;
        Vector3 targetPos = isSale ? salePlace : idlePlace;

        Vector2 pos2D = new Vector2(rb.transform.position.x, rb.transform.position.z);
        Vector2 tar2D = new Vector2(targetPos.x, targetPos.z);
        if (Vector2.Distance(pos2D,tar2D) < arrivalThreshold)
        {
            isArrived = true;
        }
        else
        {
            isArrived = false;
            Vector3 dir = (targetPos - rb.transform.position).normalized;
            moveDirection.x = dir.x;
            moveDirection.y = dir.z;
        }
        
        //未抵达目标点时
        if (!isArrived)
        {
            Move();
            if (animator != null) animator.SetBool("IsMove", true);
        }
        else
        {
            rb.linearVelocity = Vector3.zero;//让商人停下来
            moveDirection = Vector2.zero;
            if (animator != null) animator.SetBool("IsMove", false);
        }

    }

    //展示商品列表
    [SerializeField] public Transform goodsContainer;//商品容器
    [SerializeField] public List<Transform> prefabsList;
    public void ShowGoodsList()
    {
        goodsContainer?.gameObject?.SetActive(true);
    }

    //关闭商品列表
    public void CloseGoodsList()
    {
        goodsContainer?.gameObject?.SetActive(false);
    }

    //依据GameData生成商品
    public virtual void GenerateGoodsByGameData()
    {
        //子弹升级
        foreach (var item in prefabsList)
        {
            if (item == null) continue;
            BulletGoods goods = item.GetComponent<BulletGoods>();
            if (goods != null)
            {
                BulletType type = goods.GetGoodsType();
                switch (GameData.bullet)
                {
                    case BulletType.Bullet_1:
                        type = BulletType.Bullet_2;
                        break;
                    case BulletType.Bullet_2:
                        type = BulletType.Bullet_3;
                        break;
                    case BulletType.Bullet_3:
                        type = BulletType.Bullet_4;
                        break;
                    case BulletType.Bullet_4:
                        type = BulletType.None;
                        break;
                    case BulletType.None:
                        type = BulletType.Bullet_1;
                        break;
                }
                if (type == BulletType.None) break;
                BulletGoods newBG = Instantiate(goods, goodsContainer);
                newBG.SetGoodsType(type);//设置类型
                newBG.SetCost(newBG.GetGoodsType());
                newBG.ChangeOutLook(newBG.GetGoodsType());//改变外观
                newBG.transform.position = goodsContainer.transform.position + GoodsStartPosition + Vector3.right * goodsInterval * 0;
                break;
            }
        }
        //武器升级
        foreach (var item in prefabsList)
        {
            if (item == null) continue;
            WeaponUpGoods goods = item.GetComponent<WeaponUpGoods>();
            if (goods != null)
            {
                WeaponUpType type = goods.GetGoodsType();
                switch (GameData.weaponUp)
                {
                    case WeaponUpType.WeaponUp_1:
                        type = WeaponUpType.WeaponUp_2;
                        break;
                    case WeaponUpType.WeaponUp_2:
                        type = WeaponUpType.WeaponUp_3;
                        break;
                    case WeaponUpType.WeaponUp_3:
                        type = WeaponUpType.WeaponUp_4;
                        break;
                    case WeaponUpType.WeaponUp_4:
                        type = WeaponUpType.None;
                        break;
                    case WeaponUpType.None:
                    default:
                        type = WeaponUpType.WeaponUp_1;
                        break;
                }
                if (type == WeaponUpType.None) break;
                WeaponUpGoods newWG = Instantiate(goods, goodsContainer);
                newWG.SetGoodsType(type);
                newWG.SetCost(newWG.GetGoodsType());
                newWG.ChangeOutLook(newWG.GetGoodsType());
                newWG.transform.position = goodsContainer.transform.position + GoodsStartPosition + Vector3.right * goodsInterval * 1;
                break;
            }
        }
        //靴子升级
        foreach (var item in prefabsList)
        {
            if (item == null) continue;
            BootsGoods goods = item.GetComponent<BootsGoods>();
            if (goods != null)
            {
                BootsType type = goods.GetGoodsType();
                switch (GameData.boots)
                {
                    case BootsType.Boots_1:
                        type = BootsType.Boots_2;
                        break;
                    case BootsType.Boots_2:
                        type = BootsType.None;
                        break;
                    case BootsType.None:
                    default:
                        type = BootsType.Boots_1;
                        break;
                }
                if (type == BootsType.None) break;
                BootsGoods newBG = Instantiate(goods, goodsContainer);
                newBG.SetGoodsType(type);
                newBG.SetCost(newBG.GetGoodsType());//设置价格
                newBG.ChangeOutLook(newBG.GetGoodsType());
                newBG.transform.position = goodsContainer.transform.position + GoodsStartPosition + Vector3.right * goodsInterval * 2;
                break;
            }
        }
    }
}
