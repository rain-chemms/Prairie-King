using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class BulletAnimatorEvent : MonoBehaviour
{
    [SerializeField] public Bullet bullet;
    public void BulletDestory()
    {
        if(bullet == null)  return;
        Destroy(bullet.gameObject);
    }
}
