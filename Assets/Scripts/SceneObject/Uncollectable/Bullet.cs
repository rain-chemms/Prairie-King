using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : UncollectableObjectModel
{
    [SerializeField] public bool isPlayerSide = false;//是否是玩家阵营子弹
    [SerializeField] public float flyTime = 5.0f;//最长飞行时间
    private float haveFlyTime = 0.0f;//已经飞行时间
    [SerializeField] public float damage = 1.0f;//子弹的伤害
    [SerializeField] public float force = 1.0f;//子弹的作用力,决定子弹飞行的速度
    [SerializeField] public Vector3 direction = Vector3.zero;//子弹的飞行方向
    [SerializeField] public Animator animator = null;//子弹的动画器
    
    public void SetBulletData(Vector3 direction,bool isPlayerSide = false, float flyTime = 5.0f, float damage = 1.0f, bool resetForce = false,float force = 500.0f)
    {
        this.direction = direction;
        this.isPlayerSide = isPlayerSide;
        this.flyTime = flyTime;
        this.damage = damage;
        if(resetForce) this.force = force;
    }

    void Start()
    {
        haveFlyTime = 0.0f;//重置已经飞行时间
    }

    void Update()
    {
        haveFlyTime += Time.deltaTime;
        //飞行时间超过最大飞行时间或者伤害小于0时，子弹失效
        if(CheckBulletTimeOver() || !CheckBulletDamageEffective())
        {
            AfterBulletOver();
        }  
    }

    void FixedUpdate()
    {
        BulletFly();
    }

    private void BulletFly()
    {
        if(rb == null) return;
        rb.AddForce(direction.normalized * force * Time.deltaTime);
    }
    
    private bool CheckBulletTimeOver()
    {
        if(haveFlyTime >= flyTime) return true;
        return false;
    }

    private bool CheckBulletDamageEffective()
    {
        if(damage <= 0) return false;
        return true;
    }

    //子弹失效时调用的函数
    protected virtual void AfterBulletOver()
    {
        CloseAllCollider();
        TriggerOverAnimation();
    }


    //子弹消失时的动画
    protected virtual void TriggerOverAnimation()
    {
        if(animator == null) return;
        animator.SetTrigger("IsOver");
    }

    //子弹碰撞
    void OnTriggerEnter(Collider other)
    {
        //检测是否是障碍物
        switch(other.tag)
        {
            //可在此处添加障碍物类别
            case "Obstacle":
                AfterBulletOver();//触发子弹失效函数
                break;      
            default:
                break;
        }

        if(isPlayerSide)
        {
            EnermyModel enermy = other.GetComponent<EnermyModel>();
            if(enermy != null)
            {
                DamageRole(enermy);
            }
        }
        else
        {
            PlayerModel player = other.GetComponent<PlayerModel>();
            if(player != null)
            {
                DamageRole(player);
            }
        }
    }

    protected virtual bool DamageRole(RoleModel role)
    {
        if(role == null) return false;
        float tempHp = role.GetHp();
        //血量小于等于0的role无法收到伤害
        if(tempHp <= 0) return false;
        //造成伤害
        role.BeHurt(damage);
        damage -= tempHp;
        return true;
    }
}
