using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObjectModel : SceneObjectModel
{
    [SerializeField] public bool isCollectable = true;//当前是否可被拾取
    //玩家拾取触发函数
    void OnTriggerEnter(Collider other)
    {
        if(!isCollectable) return;
        PlayerModel pl = other.GetComponent<PlayerModel>();
        if(pl != null)
        {
            AfterCollect(pl);
        }
    }

    //玩家拾取后会产生的效果
    public virtual void AfterCollect(PlayerModel collectPlayer)
    {
        //拾取后禁用自身碰撞体
        CloseAllCollider();
    }


}
