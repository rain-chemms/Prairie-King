using UnityEngine;
using TMPro;

public class GameDataCoinsBar : MonoBehaviour
{
    [SerializeField] public TMP_Text moneyText;
    [SerializeField] public TMP_Text lifeText;
    public void Update()
    {
        FreshUI();
    }

    protected void FreshUI()
    {
        if(moneyText != null)
            moneyText.text = "X"+GameData.money.ToString();
        if(lifeText != null)
            lifeText.text = "X"+GameData.life.ToString();
    }

}
