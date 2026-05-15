using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleModel : AbstractModel
{
    [SerializeField]/*[NonSerialized]*/ public Vector2 moveDirection = Vector2.zero;//移动方向
    [SerializeField] public float moveForce = 1f;//移动作用力
    [SerializeField] public float hp = 1f;//血量
    [SerializeField] public float damage = 1f;//伤害
    [SerializeField] public Rigidbody rb;
    [SerializeField] public bool openTouchDamage = false;//是否开启接触伤害
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
        rb.transform.forward = new Vector3(moveDirection.x ,transform.forward.y,moveDirection.y).normalized;
        //添加作用力
        rb.AddForce(rb.transform.forward * moveForce * Time.deltaTime);
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
    protected void Update()
    {
        if(IsDeath())
        {
            OnDeath();
        }
    } 
}