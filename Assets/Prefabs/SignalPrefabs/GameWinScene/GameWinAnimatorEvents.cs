using UnityEngine;
using TMPro;

public class GameWinAnimatorEvents : MonoBehaviour
{
    [SerializeField] public TMP_Text showText;
    public void ChangeTextContent(string content)
    {
        showText.text = content;
    }
    public void ChangeSceneToMainMenu()
    {
        SceneLoader.instance?.Load("MainMenu",()=>{
            #if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
            PhoneControlUI.instance.EffectPhoneControlUI(false);
            #endif
            GameDataUI.instance.SetDisplay(false);
            AudioManager.instance.ChangeBgm("InTown"); 
            AudioManager.instance.PlayBgm();    
        });
    }
}
