using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;
using UnityEngine.InputSystem;

[Serializable]
public class SerializeDictionary<KeyType,ValueType> : Dictionary<KeyType,ValueType>,ISerializationCallbackReceiver
{
    //用于在检查器中显示的两个列表
    [SerializeField] private List<KeyType> keys = new List<KeyType>();
    [SerializeField] private List<ValueType> values = new List<ValueType>();

    // 序列化前:
    public void OnBeforeSerialize()
    {
    }
    
    // 反序列化后:将列表中的数据重新组装回字典
    public void OnAfterDeserialize()
    {
        this.Clear();
        //确保 i 不会超过 values 的长度，防止越界
        //int count = Mathf.Min(keys.Count, values.Count);
        for (int i = 0; i < keys.Count; i++)
        {
            //只有符合条件的键值对才会被添加到字典中
            if(i < values.Count)//确保 i 值不会超过 values 的长度
            {
                if(keys[i] != null && values[i] != null)//确保键值对不为 null
                {
                    if(ContainsKey(keys[i])) this[keys[i]] = values[i];//修改字典中的键值
                    else Add(keys[i], values[i]);  
                } 
                
            }    
        }
        //PrintAll();
    }
    //打印字典内全部数据
    public void PrintAll()
    {
        Debug.Log("[SerializeDictionary<" + typeof(KeyType).ToString() + "," + typeof(ValueType).ToString() + ">]: Data List => {");
        Debug.Log("    Key       Value");
        foreach (KeyValuePair<KeyType,ValueType> item in this)
        {
            Debug.Log("    " + item.Key.ToString() + "       " + item.Value.ToString());    
        }
        Debug.Log("}");
    }
}
