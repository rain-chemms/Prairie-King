using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleModel : AbstractModel
{
    [SerializeField]/*[NonSerialized]*/ public Vector2 moveDirection = Vector2.zero;//移动方向
    [SerializeField] public float moveForce = 1f;//移动作用力
    [SerializeField] public float rotateSpeed = 5f;//旋转速度
    [SerializeField] public float hp = 1f;//血量
    
    [SerializeField] public float damage = 1f;//伤害
    [SerializeField] public Rigidbody rb;
    public virtual void OpenAllCollider()
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach(Collider cld in colliders)
        {
            cld.enabled = true;
        }
    }

    public virtual void CloseAllCollider()
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach(Collider cld in colliders)
        {
            cld.enabled = false;
        }
    }
    [SerializeField] public bool openTouchDamage = false;//是否开启接触伤害
    [SerializeField] public ForceMode forceMode = ForceMode.Force;//移动时作用力模式
    //[SerializeField] public float maxVelocity = 100f;//最大速度
    public void OpenTouchDamage()
    {
        openTouchDamage = true;
    }
    public void CloseTouchDamage()
    {
        openTouchDamage = false;
    }
    [SerializeField] public bool isInvulnerable = false;//是否无敌
    //获取当前血量
    public float GetHp()
    {
        return hp;
    }
    
    //受伤函数
    public virtual void BeHurt(float damage)
    {
        if(isInvulnerable) return;
        hp -= damage;
        if (hp <= 0.0f)
        {
            hp = 0.0f;
        }
        //触发随机的HitEffect
        AudioManager.instance?.TriggerHitEffect();
    }

    //设置移动方向
    public virtual void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    } 

    //移动函数
    //FixedUpdate()函数中调用
    public virtual void Move()
    {        
        //将Role自身的方向设置为移动方向
        Vector3 dir = new Vector3(moveDirection.x ,rb.transform.forward.y,moveDirection.y).normalized;
        //在移动的情况下,添加作用力并修改角色方向
        if(moveDirection != Vector2.zero)
        {
            //速度模式下以自身的作用力大小限制速度大小
            if(forceMode == ForceMode.VelocityChange)
            {
                /*
                if(rb.linearVelocity.magnitude > moveForce)
                {
                    rb.linearVelocity = rb.linearVelocity.normalized * moveForce;
                }
                */
                rb.linearVelocity = dir.normalized * moveForce;
            }
            else rb.AddForce(dir * moveForce * Time.deltaTime,forceMode);
            rb.transform.forward = Vector3.Lerp(rb.transform.forward,dir,Time.deltaTime * rotateSpeed);
        }
        else rb.linearVelocity = Vector3.zero;//无移动方向时停止移动
    }

    //接触伤害
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(!openTouchDamage) return;
        RoleModel pl = other.GetComponent<RoleModel>();
        if(pl != null)
        {
            pl.BeHurt(this.damage);
        }    
    }

    //检查死亡
    public bool IsDeath()
    {
        if(hp <= 0.01) return true;
        else return false;
    }

    //角色死亡触发函数
    protected virtual void OnDeath()
    {
        if(rb!=null)
        {
            openTouchDamage = false;
            rb.useGravity = false;
        }
        CloseAllCollider();//关闭所有碰撞器
    }

    //更新函数
    protected void Update()
    {
        CheckDeath();
    } 
    
    protected bool haveTriggerDeath = false;
    protected virtual void CheckDeath()
    {
        if(IsDeath())
        {
            if(!haveTriggerDeath)
            {
                haveTriggerDeath = true;
                OnDeath();
            }
        }
        
    }
}