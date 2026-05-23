using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;


//单例场景加载器
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [Header("动画控制器选择")]
    [Header("不同控制器会播放不同的转场动画,默认为CrossFade")]
    [SerializeField] private Animator animator;
    [SerializeField] private float waitTime = 1.0f;
    //--基础场景加载函数开始--//
    public void Load(string levelName,Action callbacks = null)
    {
        StartCoroutine(LoadScene(levelName,callbacks));
    }
    //场景加载协程
    IEnumerator LoadScene(string levelName,Action callbacks = null)
    {
        //Play animation
        animator.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(waitTime);//暂停几秒之后继续运行
        AsyncOperation asyncOperation =  SceneManager.LoadSceneAsync(levelName,LoadSceneMode.Single);
        //等待加载完成
        while (!asyncOperation.isDone)
        {
            yield return null; // 每帧检查一次
        }
        //触发传入的事件
        callbacks?.Invoke();
        //load Scene Over:使动画器退出结束状态
        animator.SetTrigger("End");

    }
}
