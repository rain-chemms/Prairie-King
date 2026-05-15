using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy1 : EnermyModel
{
    [SerializeField] public Animator animator;
    new void Update()
    {
        base.Update();
        FollowPlayer();
    }

    void FixedUpdate()
    {
        Move();
    }

    protected override void OnDeath()
    {
        openTouchDamage = false;//死亡后不能造成接触伤害
        animator.SetTrigger("Death");
        base.OnDeath();
    }
}
