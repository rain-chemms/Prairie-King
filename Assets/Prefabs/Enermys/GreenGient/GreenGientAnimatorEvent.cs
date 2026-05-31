using UnityEngine;

public class GreenGientAnimatorEvent : MonoBehaviour
{
    [SerializeField] public GreenGient enermy1Object;
    public void DestroyGameObject()
    {
        Destroy(enermy1Object.gameObject);
    }
}
