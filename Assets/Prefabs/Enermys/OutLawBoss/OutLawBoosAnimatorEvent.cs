using UnityEngine;

public class OutLawBoosAnimatorEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public OutLawBoss enermy1Object;
    public void DestroyGameObject()
    {
        Destroy(enermy1Object.gameObject);
    }
}
