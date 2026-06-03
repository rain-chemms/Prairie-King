using UnityEngine;

public class GameOverAnimatorEvents : MonoBehaviour
{
    public void ChangeSceneToMainMenu()
    {
        SceneLoader.instance?.Load("MainMenu",()=>{
            AudioManager.instance.ChangeBgm("InTown"); 
            AudioManager.instance.PlayBgm();    
        });
    }
}
