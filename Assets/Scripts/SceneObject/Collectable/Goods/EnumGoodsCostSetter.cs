using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface EnumGoodsCostSetter<EnumType> where EnumType : Enum
{
    public void SetCost(EnumType type);
}