using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropGenerator : AbstractGenerator
{
    [SerializeField] List<Prop> propPrefabs;
    public Prop GenerateProp(PropType propType)
    {
        Prop newProp = null;
        foreach(Prop prefab in propPrefabs)
        {
            if(prefab.propType == propType)
            {
                newProp = Instantiate(prefab);
                newProp.transform.parent = null;
            }
        }
        return newProp;
    }
}
