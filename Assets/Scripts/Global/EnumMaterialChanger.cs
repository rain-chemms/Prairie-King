using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于使用Enum枚举更改材质外观
public class EnumMaterialChanger<EnumType> : MonoBehaviour where EnumType : Enum
{
    [Header("下面这些变量与材质更改有关,可以不设置")]
    [SerializeField] public SerializeDictionary<EnumType,Material> materialDict = new SerializeDictionary<EnumType,Material>();
    [SerializeField] public Transform changePart = null;
    public void ChangeOutLook(EnumType type)
    {
        if(changePart == null) return;
        if(materialDict.Count <= 0 || !materialDict.ContainsKey(type)) return;
        foreach(KeyValuePair<EnumType,Material> item in materialDict)
        {
            if(item.Key.Equals(type))
            {
                Renderer renderer = changePart.GetComponent<Renderer>();
                if(item.Value != null && renderer!=null) 
                {
                    renderer.material =  item.Value;
                    break;
                }
            }
        }
    }
}
