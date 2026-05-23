using UnityEngine;
using UnityEngine.UI;

public class CanvasCameraAutoGetter : MonoBehaviour
{
    //自动设置Canvas的worldCamera为主相机
    [SerializeField] public int setCateory = 0;//设置摄像机的方法
    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            switch (setCateory)
            {
                case 0:
                default:
                    canvas.worldCamera = Camera.main;
                    break;
            }
        }
    }
}
