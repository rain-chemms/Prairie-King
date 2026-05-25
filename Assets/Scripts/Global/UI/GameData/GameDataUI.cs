using UnityEngine;

//单例模式
public class GameDataUI : MonoBehaviour
{
    public static GameDataUI instance;
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

    
    [SerializeField] public bool isDisplay = true;//是否显示
    public void SetDisplay(bool display)
    {
        isDisplay = display;
        //更改后检查画布显示
        CheckCanvasDisplay();
    }

    [SerializeField] private Canvas canvas;//自身所在的画布
    void Start()
    {
        //尝试获取自身所在的画布
        if(canvas == null)
        {
            canvas = GetComponent<Canvas>();    
        }
    }

    void Update()
    {    
        CheckCanvasDisplay();
    }

    protected void CheckCanvasDisplay()
    {
        if(canvas != null)
        {
            if(isDisplay)
            {
                canvas.enabled = true;
            }
            else
            {
                canvas.enabled = false;
            }
        }
    }
    //子级别UI控制脚本
    [SerializeField] public GameDataCoinsBar coinsBar;
    [SerializeField] public GameDataTimeBar timeBar;
    [SerializeField] public GameDataPropBar propUI;

    
        
}
