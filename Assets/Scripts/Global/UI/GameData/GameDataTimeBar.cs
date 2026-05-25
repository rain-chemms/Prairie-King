using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDataTimeBar : MonoBehaviour
{
    [SerializeField] public float maxTime = 1.0f;// 最大时间
    [SerializeField] public float remainTime = 1.0f;// 剩余时间
    [SerializeField] private Slider timeSlider;// 时间进度条
    [SerializeField] private TMP_Text timeText;// 时间文本

    void Update()
    {
        CheckTime();
        FreshUI();
    }

    // 检查时间
    private void CheckTime()
    {
        LevelModel nowLevel = LevelProgressControler.instance?.GetNowLevel();
        if(nowLevel != null)
        {
            maxTime = nowLevel.GetPersistTime();    
        }

        if(LevelProgressControler.instance!=null)
            remainTime = maxTime - LevelProgressControler.instance.GetTimeRecorder();
    }

    // 刷新UI
    private void FreshUI()
    {
        // 设置时间文本
        if(timeText != null)
            timeText.text = ((int)remainTime).ToString()+"/"+ ((int)maxTime).ToString();// 时间使用整数
        //设置进度条
        if(timeSlider != null)
        {
            timeSlider.value = remainTime;
            timeSlider.maxValue = maxTime;
        }
    }

}
