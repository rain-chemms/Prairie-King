using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//关卡的摄像机锚点,用于摄像机自动寻找和匹配并移动
public class AnchorModel : AbstractModel
{
    protected virtual void Start()
    {
        name = "AnchorModel";//自动重命名,便于查找
    }
}
