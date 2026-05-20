using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMover : MonoBehaviour
{
    [SerializeField] public Camera camera = null;
    [SerializeField] public Transform moveTarget = null;//要移动和旋转到哪个物体
    [SerializeField] private float lerpSpeed = 10.0f;
    [SerializeField] private float rotateSpeed = 10.0f;
    void Update() {
        if(moveTarget != null && camera != null)
        {
            //移动摄像机
            camera.transform.position = Vector3.Lerp(camera.transform.position,moveTarget.position,Time.deltaTime * lerpSpeed);
            //旋转摄像机
            camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation,Quaternion.LookRotation(moveTarget.position - camera.transform.position),Time.deltaTime * rotateSpeed);
        }
    }

    public void SetTarget(Transform target) {
        moveTarget = target;
    }
}
