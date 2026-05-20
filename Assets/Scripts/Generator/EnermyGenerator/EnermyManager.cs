using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnermyManager : ProductManager<EnermyModel>
{
    protected override void CheckAndCleanWaste()
    {
        base.CheckAndCleanWaste();
        foreach(EnermyModel enermy in GetList().ToList())
        {
            if (enermy.IsDeath())
            {
                Remove(enermy);
            }
        }
    }
}
