using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropGenerator : AbstractGenerator<PropType,Prop>
{
    [SerializeField] List<Prop> propPrefabs;
    //使用类型查找
    public override Prop Generate(PropType propType)
    {
        base.Generate(propType);
        Prop newProp = null;
        foreach(Prop prefab in propPrefabs)
        {
            if(prefab.propType == propType)
            {
                newProp = Instantiate(prefab,productManager?.transform);
                SetProductPosition(newProp);
                productManager?.Add(newProp);
            }
        }
        return newProp;
    }
}
