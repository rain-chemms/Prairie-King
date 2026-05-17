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
    [SerializeField] public bool openTouchDamage = false;//是否开启接触伤害
    
    //获取当前血量
    public float GetHp()
    {
        return hp;
    }
    
    //受伤函数
    public virtual void BeHurt(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0.0f;
        }
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
            rb.AddForce(dir * moveForce * Time.deltaTime);
            rb.transform.forward = Vector3.Lerp(rb.transform.forward,dir,Time.deltaTime * rotateSpeed);
        }
    }

    //接触伤害
    void OnTriggerEnter(Collider other)
    {
        if(!openTouchDamage) return;
        PlayerModel pl = other.GetComponent<PlayerModel>();
        if(pl != null)
        {
            pl.BeHurt(this.damage);
        }    
    }

    //检查死亡
    public bool IsDeath()
    {
        if(hp <= 0) return true;
        else return false;
    }

    //角色死亡触发函数
    protected virtual void OnDeath()
    {}

    //更新函数
    private bool haveTriggerDeath = false;
    protected void Update()
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