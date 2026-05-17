using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectModel : AbstractModel
{
    [SerializeField] public Rigidbody rb;//场景物刚体
    public virtual void OpenAllCollider()
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach(Collider cld in colliders)
        {
            cld.enabled = true;
        }
    }

    public virtual void CloseAllCollider()
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach(Collider cld in colliders)
        {
            cld.enabled = false;
        }
    }
}
