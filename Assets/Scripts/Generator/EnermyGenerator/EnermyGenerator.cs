using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnermyGenerator : AbstractGenerator<EnermyType,EnermyModel>
{
    // 使用类型查找
    public override EnermyModel Generate(EnermyType enermyType)
    {
        base.Generate(enermyType);
        EnermyModel newEnermy = null;
        foreach(EnermyModel prefab in productPrefabs)
        {
            if(prefab.enermyType == enermyType)
            {
                newEnermy = Instantiate(prefab,transform);
                SetProductPosition(newEnermy);
                newEnermy.transform.parent = null;
            }
        }
        return newEnermy;
    }

    protected override void SetProductPosition(EnermyModel enermy)
    {
        NavMeshHit hit;
        if(NavMesh.SamplePosition(transform.position,out hit,float.PositiveInfinity,NavMesh.AllAreas))
        {
            enermy?.agent?.Warp(hit.position);
        }
        else
        {
            Debug.LogWarning("[EnermyGenerator]:Can't find a valid position for the new Enermy!");
            enermy?.agent?.Warp(transform.position);
        }
    }

}
