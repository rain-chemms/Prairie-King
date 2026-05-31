using UnityEngine;

public class GreenHedgehogAnimatorEvent : MonoBehaviour
{
    [SerializeField] public GreenHedgehog enermy;
    [SerializeField] public Transform stickModel;
    //效果与直接在动画器中调用enermy函数一样
    public void DropStick()
    {
        enermy?.DropStick();    
    }

    public void StickModelActive()
    {
        stickModel.gameObject.SetActive(true);
    }

    public void StickModelInactive()
    {
        stickModel.gameObject.SetActive(false);
    }
    
    public void DestroyGameObject()
    {
        Destroy(enermy?.gameObject);
    }
}
