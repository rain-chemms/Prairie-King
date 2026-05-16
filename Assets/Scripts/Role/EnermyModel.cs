using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnermyModel : RoleModel
{
    //智能体导航器
    [SerializeField] public NavMeshAgent agent = null; 
    protected Transform targetPlayer = null;//目标玩家的位置
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
                targetPlayer = players[Random.Range(0, players.Length)].GetComponent<Transform>();
        }    
    }

    //追赶目标玩家
    //计算moveDirection
    //Update()函数中调用,返回目标的世界坐标位置
    public virtual Vector3 FollowPlayer()
    {
        if(targetPlayer == null) return transform.position;
        Vector3 tar = Vector3.zero; 
        tar = (targetPlayer.position - transform.position).normalized;
        //2.5D游戏,需要x和z值
        moveDirection.x = tar.x;//获取x
        moveDirection.y = tar.z;//获取z
        return targetPlayer.transform.position;
    }

    //产生随机道具掉落物
    public virtual void DropProp()
    {
        //产生一个0到1之间的小数
        float random = Random.Range(0, 1);
        //random > 0.65 时,产生道具,> 0.6时产生一个1元金币
        switch(random)
        {
            case > 0.9f:
                //Random
                //GenerateProp<Star>()
                //GenerateProp<LifeCoin>()
                break;
            case > 0.8f:
                //Random
                //GenerateProp<Tomb>()
                //GenerateProp<Nuclear>()
                //GenerateProp<SmokeBomb>()
                //GenerateProp<Wheel>()
                //GenerateProp<FiveCoin>()
                //GenerateProp<MachineGun>()
                break;
            case > 0.65f:
                //Random
                //GenerateProp<ShotGun>()
                //GenerateProp<Coffee>()>()
                break;
            case > 0.6f:
                //Random
                //GenerateProp<OneCoin>()
                break;
            default:
                break;
        }   
        
    }
    
    //
    protected override void OnDeath()
    {
        DropProp();
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

    //智能体移动
    public virtual void Move_Agent()
    {
        Vector3 tar = FollowPlayer();
        if(agent!=null)
        {
            agent.SetDestination(tar);
        }
    }
}
