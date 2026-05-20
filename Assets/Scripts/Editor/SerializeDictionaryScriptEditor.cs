using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SerializeDictionary<float,EnermyType>))]
public class SerializeDictionaryScriptEditor : Editor
{
    private SerializedProperty keys;
    private SerializedProperty values;

    private void OnEnable()
    {
        keys = serializedObject.FindProperty("keys");// 获取键
        values = serializedObject.FindProperty("values");// 获取值
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        // 保持两个列表长度一致
        int newSize = EditorGUILayout.IntField("SerializeDictionary Size", keys.arraySize);
        newSize = Mathf.Max(0, newSize);
        while (keys.arraySize < newSize) 
            keys.InsertArrayElementAtIndex(keys.arraySize);
        while (values.arraySize < newSize) 
            values.InsertArrayElementAtIndex(values.arraySize);
        while (keys.arraySize > newSize) 
        { 
            keys.DeleteArrayElementAtIndex(keys.arraySize - 1); 
            values.DeleteArrayElementAtIndex(values.arraySize - 1); 
        }
        // 绘制键值对
        for (int i = 0; i < keys.arraySize; i++)
        {
            var key = keys.GetArrayElementAtIndex(i);
            var value = values.GetArrayElementAtIndex(i);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(key, GUIContent.none);
            EditorGUILayout.PropertyField(value, GUIContent.none);
            EditorGUILayout.EndHorizontal();
        }        
        serializedObject.ApplyModifiedProperties();
    }
}
