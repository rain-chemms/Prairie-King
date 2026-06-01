using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

//抽象生成列表,依据时间进度创建对象
public class AbstractGenerateList<Source,Product> : MonoBehaviour where Source : Enum where Product : MonoBehaviour
{
    //关联的生成器
    [SerializeField] public AbstractGenerator<Source,Product> generator; 
    [SerializeField] protected float recordTime;
    public void SetRecordTime(float time)
    {
        recordTime = time;
    }
    public float GetRecordTime()
    {
        return recordTime;
    }
    [SerializeField] public bool isActivate = true;//是否激活
    public void SetActivate(bool isActivate)
    {
        this.isActivate = isActivate;
    }
       
    [SerializeField] public bool isLoop = true;//是否循环不断的按照时间表创建对象
    //生成时的时间列表
    //每个时间对应一个生成源
    //使用生成器生成对象

    /// <summary>
    /// /正常来说应该把是否生成的列表与序列化字典放在检查器的同一列
    [SerializeField] private List<bool> isGenerate = new List<bool>();//记录是否已经生成
    //刷新检查表
    public void ResetIsGenerateList()
    {
        if(timeList == null) return;
        isGenerate.Clear();
        for(int i = 0; i < timeList.Count; i++)
        {
            isGenerate.Add(false);
        }
    }
    [SerializeField] public SerializeDictionary<float,Source> timeList = new SerializeDictionary<float,Source>();
    /// </summary>
    protected virtual void Start()
    {
        ResetTime();
        ResetIsGenerateList();
        timeList.PrintAll();
    }

    private float maxTime = 0.0f;
    private void CheckMaxTime()
    {
        List<float> times = timeList.Keys.ToList();
        if(times.Count > 0)
        {
            maxTime = times.Max();
            //Debug.Log("[AbstractGenerateList<" +typeof(Source).ToString()+","+ typeof(Product).ToString() + "> (" + this.GetType().ToString() +") ]:" + " Max Time:" + maxTime.ToString());
        }
        else Debug.LogWarning("[AbstractGenerateList<" +typeof(Source).ToString()+","+ typeof(Product).ToString() + "> (" + this.GetType().ToString() +") ]:" + " TimeList is Empty!");
    }

    protected virtual void Update()
    {   
        if(isActivate)
        {
            recordTime += Time.deltaTime;
        }
        CheckMaxTime();
        if(recordTime > maxTime)
        {
            //循环条件下,重新开始计时器时间
            if(isLoop) 
            {
                isGenerate.Clear();
                ResetTime();
            }
            else isActivate = false;
        }
        CheckProductGenerate();
    } 

    public virtual void ResetTime()
    {
        //重置计时器时间
        recordTime = 0;
    }

    protected virtual void CheckProductGenerate()
    {
        // 依据当前时间生成对象
        if(isGenerate.Count < timeList.Count) ResetIsGenerateList();//确保不会发生越界
        for(int i = 0; i < timeList.Count; i++)
        {
            if(recordTime > timeList.Keys.ToList()[i] && !isGenerate[i])
            {
                isGenerate[i] = true;
                generator.Generate(timeList.Values.ToList()[i]);
            }
        }
    }
}
