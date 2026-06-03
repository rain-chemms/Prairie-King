using UnityEngine;

public class PhoneControlUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //单例模式
    public static PhoneControlUI instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        //如果为移动端平台,则激活该物体
        #if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
            EffectPhoneControlUI(true);
        #else 
            EffectPhoneControlUI(false);
        #endif
    }

    public void EffectPhoneControlUI(bool display)
    {
        gameObject.SetActive(display);
    }
}
