using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.InputSystem;
using System;

public class GoldenFinger : MonoBehaviour
{
    //输入系统
    [SerializeField] public InputActionAsset inputAction;
    void Start()
    {
        InitActions();
        FunctionLink();
    }
    private InputActionMap goldenFingerMap;
    private InputAction killAllEnermy;
    private InputAction getProp;
    private InputAction skipTime;
    private InputAction resetTime;
    public void InitActions()
    {
        goldenFingerMap = inputAction?.FindActionMap("GoldenFinger");    
        killAllEnermy = goldenFingerMap?.FindAction("KillAllEnermy");
        getProp = goldenFingerMap?.FindAction("GetProp");
        skipTime = goldenFingerMap?.FindAction("SkipTime");
        resetTime = goldenFingerMap?.FindAction("ResetTime");
        goldenFingerMap.Enable();
    }

    public void FunctionLink()
    {   
        killAllEnermy.performed += (context) =>
        {
            EnermyModel[] enermyModels = FindObjectsOfType<EnermyModel>();
            foreach(EnermyModel enermyModel in enermyModels)
            {
                enermyModel.BeHurt(9999999999);
            }
        };

        getProp.performed += (context) =>
        {
            int value = 0; 
            if(int.TryParse(context.control.name,out value))
            {
                GameData.prop = (PropType)value;
            }
            Debug.Log("[GoldenFinger]:"+"Get Prop:" + GameData.prop + " | Read Value:" + value);
        };

        skipTime.performed += (context) =>
        {
            LevelProgressControler.instance.SetTimeRecorder((float)LevelProgressControler.instance?.GetNowLevel()?.GetPersistTime() - 1.0f);
        };

        resetTime.performed += (context) =>
        {
            LevelProgressControler.instance.ResetTimeRecorder();
        };
    }

    void OnEnable()
    {
        goldenFingerMap?.Enable();
    }

    void OnDisable()
    {
        goldenFingerMap?.Disable();    
    }

}
