using UnityEngine;
using UnityEngine.UI;

public class GameDataPropBar : MonoBehaviour
{
    //图标字典列表
    [SerializeField] private SerializeDictionary<PropType,Texture> propTextureDict = new SerializeDictionary<PropType, Texture>();
    [SerializeField] private RawImage propImage;//道具图标显示器

    // Update is called once per frame
    void Update()
    {
        FreshUI();
    }

    protected void FreshUI()
    {
        PropType prop = GameData.prop; 
        if(propImage!=null && propTextureDict!=null)
        {
            if(propTextureDict.ContainsKey(prop))
            {
                Texture texture = propTextureDict[prop];
                if(texture!=null)
                {
                    propImage.texture = texture;
                }
            }
        }       
    }
}
