using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : CollectableObjectModel
{
    [SerializeField] public float persisitTime = 15.0f;//道具存在时间,超过后消失
    [SerializeField] public float propEffectTime = 10.0f;//道具拾取后持续时间
    [SerializeField] public PropType propType = PropType.OneCoin;//道具类型
    [SerializeField] public Animator animator = null;///道具动画器
    public override void AfterCollect(PlayerModel collectPlayer)
    {
        base.AfterCollect(collectPlayer);
        if(GameData.prop == PropType.None) GameData.prop = propType;
        else EffectOnPlayer(collectPlayer);
        //TriggerDestoryAnimation();
        //Destroy(gameObject);
    }

    public bool EffectOnPlayer(PlayerModel effectPlayer)
    {
        if(effectPlayer == null) return false;
        switch(propType)
        {
            //数值恢复型
            case PropType.OneCoin:
                GameData.money += 1;
                break;
            case PropType.FiveCoin:
                GameData.money += 5;
                break;
            case PropType.LifeCoin:
                GameData.life += 1;
                break;
            //技能效果型:设置自己
            case PropType.Wheel:
            case PropType.MachineGun:
            case PropType.ShotGun:
            case PropType.Coffee:
            case PropType.SmokeBomb:
            case PropType.Tomb:
                effectPlayer.SetPropTime(propType,propEffectTime);           
                break;
            //星星比较特殊,是设置三个已有的技能事件
            case PropType.Star:
                    effectPlayer.SetPropTime(PropType.MachineGun,propEffectTime);
                    effectPlayer.SetPropTime(PropType.ShotGun,propEffectTime);
                    effectPlayer.SetPropTime(PropType.Coffee,propEffectTime);
                break;
            case PropType.Nuclear://核弹:对所有敌人造成500伤害
                EnermyModel[] enermies = FindObjectsOfType<EnermyModel>();
                foreach(EnermyModel enermy in enermies)
                {
                    enermy.BeHurt(500);
                }
                //TriggerAnimation();
                break;
        }
        return true;
    }
}
