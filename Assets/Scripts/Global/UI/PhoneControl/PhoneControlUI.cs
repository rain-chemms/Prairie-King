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
        //初始场景不激活
        EffectPhoneControlUI(false);
    }

    public void EffectPhoneControlUI(bool display)
    {
        gameObject.SetActive(display);
    }
}
