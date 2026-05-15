using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEvent : MonoBehaviour
{
    [SerializeField] public PlayerModel playerGameObject;
    public void GameOver()
    {
        // 结束游戏
        //GameOver()
        // 销毁游戏对象
        Destroy(playerGameObject.gameObject);
    }
}
