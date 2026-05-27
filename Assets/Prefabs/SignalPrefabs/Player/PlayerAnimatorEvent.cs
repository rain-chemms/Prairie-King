using UnityEngine;

public class PlayerAnimatorEvent : MonoBehaviour
{
    [SerializeField] public PlayerModel playerGameObject;
    public void GameOver()
    {
        // 结束游戏
        //GameOver()
        // 销毁游戏对象
        //Destroy(playerGameObject.gameObject);
        if(playerGameObject is PlayerAnimatorDeathFunction)
        {
            PlayerAnimatorDeathFunction apdf = playerGameObject as PlayerAnimatorDeathFunction;
            //apdf.BeforeDelayDeathFunction();
            StartCoroutine(apdf.AfterDelayDeathFunction());
        }
    }
    //Bgm关闭
    public void CloseBgm()
    {
        //停止BGM
        AudioManager.instance?.StopBgm();
    }

    //死亡的瞬间
    public void DeathInstant()
    {
        if(playerGameObject is PlayerAnimatorDeathFunction)
        {
            PlayerAnimatorDeathFunction apdf = playerGameObject as PlayerAnimatorDeathFunction;
            apdf.DeathInstantFunction();
        }
    }
}
