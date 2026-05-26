using UnityEngine;
using System.Collections;
public interface PlayerAnimatorDeathFunction
{
    float delay {get;}
    IEnumerator AfterDelayDeathFunction();//延迟后执行的死亡函数
    void DeathInstantFunction();//死亡时瞬间执行的函数
}
