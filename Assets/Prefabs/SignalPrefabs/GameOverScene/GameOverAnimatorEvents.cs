using UnityEngine;

public class GameOverAnimatorEvents : MonoBehaviour
{
    public void ChangeSceneToMainMenu()
    {
        SceneLoader.instance?.Load("MainMenu",()=>{
            #if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
            PhoneControlUI.instance.EffectPhoneControlUI(false);
            #endif
            AudioManager.instance.ChangeBgm("InTown"); 
            AudioManager.instance.PlayBgm();    
        });
    }
}
