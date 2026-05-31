using UnityEngine;

public class Stick : EnermyModel
{
    new void Update()
    {
        base.Update();
    }
    
    //自身不可移动
    void FixedUpdate()
    {}

    protected override void OnDeath()
    {
        openTouchDamage = false;//死亡后不能造成接触伤害
        base.OnDeath();
        Destroy(gameObject);//销毁自身
    }
}
