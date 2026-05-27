using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnermyModel : RoleModel
{
    //敌人种类
    [SerializeField] public EnermyType enermyType = EnermyType.None;
    //智能体导航器
    [SerializeField] public NavMeshAgent agent = null; 
    protected PlayerModel targetPlayer = null;//目标玩家的位置
    //寻找目标玩家
    //Start()函数中调用
    public virtual void SearchPlayer()
    {
        //目标玩家为空时,寻找目标玩家
        if(targetPlayer == null)
        {
            PlayerModel[] players = FindObjectsOfType<PlayerModel>();
            //挑选随机玩家作为目标
            //Debug.Log(players.Length);
            if(players!=null && players.Length > 0)
            {
                targetPlayer = players[Random.Range(0, players.Length)];
            }   
        } 
    }

    //追赶目标玩家
    //计算moveDirection
    //Update()函数中调用,返回目标的世界坐标位置
    public virtual Vector3 FollowPlayer()
    {
        Vector3 tar = transform.position;
        if(targetPlayer == null || targetPlayer.isInvisible) return tar;
        tar = ((Vector3)(transform.position - targetPlayer?.transform.position)).normalized;
        //2.5D游戏,需要x和z值
        moveDirection.x = tar.x;//获取x
        moveDirection.y = tar.z;//获取z
        if(targetPlayer.isZombieState) return tar;//逃离玩家
        return (Vector3)targetPlayer?.transform.position;
    }

    //产生随机道具掉落物
    [SerializeField] public PropGenerator propGenerator = null;//道具生成器
    [SerializeField] public bool canDropProp = true;//是否可以掉落道具
    public virtual void DropProp()
    {
        if(propGenerator == null) return;//道具生成器为空时不能产生道具
        if(!canDropProp) return;
        //产生一个0到1之间的小数
        float random = Random.Range(0.0f, 1.0f);
        //random > 0.65 时,产生道具,> 0.6时产生一个1元金币
        Prop newProp = null;
        List<PropType> canGeneratePropList = new List<PropType>();
        switch(random)
        {
            case > 0.9f:
                //Random
                canGeneratePropList.Add(PropType.FiveCoin);
                canGeneratePropList.Add(PropType.LifeCoin);
                canGeneratePropList.Add(PropType.Star);
                break;
            case > 0.8f:
                canGeneratePropList.Add(PropType.Tomb);
                canGeneratePropList.Add(PropType.Nuclear);
                canGeneratePropList.Add(PropType.SmokeBomb);
                canGeneratePropList.Add(PropType.Wheel);
                canGeneratePropList.Add(PropType.MachineGun);
                canGeneratePropList.Add(PropType.ShotGun);
                canGeneratePropList.Add(PropType.Coffee);
                break;
            case > 0.65f:
                //Random
                canGeneratePropList.Add(PropType.ShotGun);
                canGeneratePropList.Add(PropType.Coffee);
                canGeneratePropList.Add(PropType.OneCoin);
                break;
            case > 0.6f:
                canGeneratePropList.Add(PropType.OneCoin);
                break;
            default:
                break;
        }   
        //Debug.Log("random:"+ random +"|Count :" + canGeneratePropList.Count);
        if(canGeneratePropList.Count > 0)
            newProp = propGenerator.Generate(canGeneratePropList[Random.Range(0, canGeneratePropList.Count)]);
        if(newProp != null)
        {    
            newProp.transform.position = transform.position;
            Debug.Log("[EnermyModel]:Drop Prop "+ newProp.propType.ToString());
        }
    }
    
    //
    protected override void OnDeath()
    {
        DropProp();
        agent.enabled = false;//死亡后不能移动,关闭智能体导航
        base.OnDeath();
    }

    //Update()函数
    new protected void Update()
    {
        //Debug.Log("EM Update");
        base.Update();
    }

    //Start()函数
    protected void Start()
    {
        //openTouchDamage = true;
        SearchPlayer();
    }
    //限制角色AI速度

    //智能体移动
    //private Vector3 agent_target = Vector3.zero;
    public virtual void Move_Agent()
    {
        Vector3 tar = FollowPlayer();
        if(agent!=null && agent.enabled)
        {
            agent.SetDestination(tar);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        PlayerModel pl = other?.GetComponent<PlayerModel>();
        if(pl != null)
        {
            base.OnTriggerEnter(other);
        }
    }
}
