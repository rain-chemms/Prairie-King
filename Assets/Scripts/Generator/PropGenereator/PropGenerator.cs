using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
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
                newProp = Instantiate(prefab);
                SetProductPosition(newProp);
                newProp.transform.parent = null;
            }
        }
        return newProp;
    }
}
