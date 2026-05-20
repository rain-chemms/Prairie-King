using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

//生成物管理器
public class ProductManager<Product> : AbstractModel where Product : MonoBehaviour
{
    [SerializeField] public List<Product> productList = new List<Product>();
    public List<Product> GetList()
    {
        return productList;
    }
    //生成物管理器
    public void Add(Product product)
    {
        if(!productList.Contains(product))
            productList.Add(product);
    }

    public void Remove(Product product)
    {
        if(productList.Contains(product))
            productList.Remove(product);
    }

    protected virtual void Update()
    {
        CheckAndCleanWaste();
    }
    
    protected virtual void CheckAndCleanWaste()
    {
        if(productList!=null) productList.RemoveAll( item => item == null);
    }
}

