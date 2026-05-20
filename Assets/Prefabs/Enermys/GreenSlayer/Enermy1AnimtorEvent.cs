using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermy1AnimtorEvent : MonoBehaviour
{
    [SerializeField] public Enermy1 enermy1Object;
    public void DestroyGameObject()
    {
        Destroy(enermy1Object.gameObject);
    }
}
