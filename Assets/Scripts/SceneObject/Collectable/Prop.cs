using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : CollectableObjectModel
{
    [SerializeField] private float haveInsistTime = 5.0f;
    [SerializeField] private float blinkInterval = 0.5f;//模型闪烁间隔
    [SerializeField] private bool isBlink = false;//模型是否闪烁
    [SerializeField] public float persisitTime = 15.0f;//道具存在时间,超过后消失
    [SerializeField] public float propEffectTime = 10.0f;//道具拾取后持续时间
    [SerializeField] public PropType propType = PropType.OneCoin;//道具类型
    [SerializeField] public Animator animator = null;///道具动画器
    [SerializeField] public Transform model = null;//道具模型,用于在最后剩余5s钟内使模型闪烁
    
    void Start()
    {
        if(model == null) model = transform.GetChild(0);//获取模型    
        haveInsistTime = 0.0f;
    }

    void Update()
    {
        haveInsistTime += Time.deltaTime;
        if(haveInsistTime > persisitTime)
        {
            PlayerOverPersistTimeAnimation();
        }
        CheckModelBlink();
        JustBlink();
    }

    //道具闪烁函数
    private float blinkTimeRecorder = 0.0f; 
    private void JustBlink()
    {
        if(model == null) return;
        if(isBlink)
        {
            blinkTimeRecorder += Time.deltaTime;
            if(blinkTimeRecorder >= blinkInterval)
            {
                model.gameObject.SetActive(!model.gameObject.activeSelf);
                blinkTimeRecorder = 0.0f;
            }
        }
        else {
            blinkTimeRecorder = 0.0f;
            model.gameObject.SetActive(true);
        }
    }
    //模型闪烁检测函数
    public void CheckModelBlink()
    {
        if(haveInsistTime >= persisitTime - 5.0f) isBlink = true;
        else isBlink = false;
    }

    public override void AfterCollect(PlayerModel collectPlayer)
    {
        base.AfterCollect(collectPlayer);
        //直接生效->存储到背包->背包满触发
        Debug.Log("[Prop]:"+"Collect Prop:"+propType);
        if(propType == PropType.OneCoin || propType==PropType.FiveCoin || propType==PropType.LifeCoin) 
        {
            EffectOnPlayer(collectPlayer);
            Debug.Log("[Prop]:"+"Prop is Reward:"+propType);
        }
        else if(GameData.prop == PropType.None) GameData.prop = propType;
        else 
        {
            EffectOnPlayer(collectPlayer);
            Debug.Log("[Prop]:"+"BackPack Full,Use Prop:"+propType);
        }
        PlayerCollectedAnimation();
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
                    enermy.canDropProp = false;//被核弹杀死不可掉落道具
                    enermy.BeHurt(500);
                }
                //TriggerAnimation();
                break;
        }
        return true;
    }

    protected virtual void PlayerOverPersistTimeAnimation()
    {
        animator.SetTrigger("OverPersistTime");
    }

    protected virtual void PlayerCollectedAnimation()
    {
        animator.SetTrigger("Collected");
    }
}
