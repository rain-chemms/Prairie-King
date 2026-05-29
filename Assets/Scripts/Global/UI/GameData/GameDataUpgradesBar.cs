using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameDataUpgradesBar : MonoBehaviour
{
    [SerializeField] public SerializeDictionary<BootsType,Texture> bootsTextureDict = new SerializeDictionary<BootsType,Texture>();
    [SerializeField] public RawImage bootsImage = null;
    [SerializeField] public SerializeDictionary<WeaponUpType,Texture> weaponUpTextureDict = new SerializeDictionary<WeaponUpType,Texture>();
    [SerializeField] public RawImage weaponUpImage = null;
    [SerializeField] public SerializeDictionary<BulletType,Texture> propTextureDict = new SerializeDictionary<BulletType,Texture>();
    [SerializeField] public RawImage propImage = null;

    // Update is called once per frame
    void Update()
    {
        FreshUI();
    }

    void FreshUI()
    {
        BulletType prop = GameData.bullet;
        if(propImage!=null && propTextureDict!=null)
        {
            if(propTextureDict.ContainsKey(prop))
                propImage.texture = propTextureDict[prop];
        }

        WeaponUpType weaponUp = GameData.weaponUp;
        if(weaponUpImage!=null && weaponUpTextureDict!=null)
        {
            if(weaponUpTextureDict.ContainsKey(weaponUp))
                weaponUpImage.texture = weaponUpTextureDict[weaponUp];
        }

        BootsType boots = GameData.boots;
        if(bootsImage!=null && bootsTextureDict!=null)
        {
            if(bootsTextureDict.ContainsKey(boots))
                bootsImage.texture = bootsTextureDict[boots];
        }
    }
}
