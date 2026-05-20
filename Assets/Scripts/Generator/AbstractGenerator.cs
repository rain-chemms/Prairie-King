using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AbstractGenerator<Source,Product> : AbstractModel where Source : Enum where Product : MonoBehaviour
{
    [SerializeField] public ProductManager<Product> productManager;//产品管理器,所有的产品都会存放在管理器中,管理器为空时,产品会直接生成在根节点上
    public ProductManager<Product> GetProductManager()
    {
        return productManager;
    }
    public void SetProductManager(ProductManager<Product> productManager)
    {
        this.productManager = productManager;
    }
    [SerializeField] public List<Product> productPrefabs = new List<Product>(); 
    //使用列别查找
    public virtual Product Generate(Source source)
    {
        Product product = default(Product);
        SetProductPosition(product);
        return product;
    }

    // 使用名字查找
    public Product Generate(String productName)
    {
        Product newProduct = null;
        foreach(Product prefab in productPrefabs)
        {
            if(prefab.name.Equals(productName))
            {
                newProduct = Instantiate(prefab,productManager?.transform);
                SetProductPosition(newProduct);
                productManager?.Add(newProduct);
            }
        }
        return newProduct;
    }

    protected virtual void SetProductPosition(Product product)
    {
        if(product != null)
        {
            product.transform.position = transform.position;
        }
    } 
}
