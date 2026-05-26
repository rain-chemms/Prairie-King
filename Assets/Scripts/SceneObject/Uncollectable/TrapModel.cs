using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrapModel : UncollectableObjectModel,LevelTrap
{
    [Header("关机设置")]
    //是否在机关开启时隐藏机关而在关闭时开启机关
    [SerializeField] public bool hideOnOpen = true;
    //基础机关类,为机关的基类
    public virtual void Open()
    {
        if(hideOnOpen) CloseAllCollider();
        else OpenAllCollider();
    }

    public virtual void Close()
    {
        if(hideOnOpen) OpenAllCollider();
        else CloseAllCollider();
    }
}
