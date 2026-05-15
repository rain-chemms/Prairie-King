using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerValueCaculator
{
    float baseDamage { get; }
    float baseShootInterval { get; }
    float baseMoveForce { get; }
    float NowDamage();//计算当前的伤害
    float NowShootInterval();//计算当前的射速
    float NowMoveForce();//计算当前的移动力
}
