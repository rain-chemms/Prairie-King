using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PropTimeRecorder
{
    Dictionary<PropType,float> propTimeRemainder {get;set;}//道具时间剩余字典
    bool PropTimeFlow();//道具时间流逝函数
    bool AddPropTime(PropType propType,float addTime);
    bool SetPropTime(PropType propType,float time);
}
