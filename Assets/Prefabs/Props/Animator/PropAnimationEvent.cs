using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropAnimationEvent : MonoBehaviour
{
    [SerializeField] public Prop prop;
    public void DestroyAfterCollect()
    {
        if(prop!=null)
        {
            Destroy(prop.gameObject);
        }
    }

    public void CloseAllCollider()
    {
        if(prop!=null)
        {
            Collider[] colliders = prop.GetComponents<Collider>();
            foreach(Collider collider in colliders)
            {
                collider.enabled = false;
            }
        }
    }
}
