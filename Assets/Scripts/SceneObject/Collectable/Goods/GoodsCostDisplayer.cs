using System;
using UnityEngine;
using TMPro;

public class GoodsCostDisplayer<EnumType> : MonoBehaviour where EnumType : Enum
{
    [SerializeField] public GoodsModel<EnumType> goods;
    [SerializeField] private TMP_Text text;//显示文本
    [Header("是否实时更新商品价格")]
    [SerializeField] protected bool alwaysCheckCostByType = false;
    protected void Update()
    {
        if(alwaysCheckCostByType)
        {
            GetGoodsCostAndDisplay();
        }
    }
    //获取商品价格并显示
    private void Start()
    {
        GetGoodsCostAndDisplay();   
    }

    private void GetGoodsCostAndDisplay()
    {
        if(goods!=null)
        {
            uint cost = goods.GetCost();
            text.text = cost.ToString();
        }
    }
}
