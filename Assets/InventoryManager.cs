using System.Collections;
using System.Collections.Generic;
using DG.Util;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    
    public List<BaseItem> items = new List<BaseItem>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var itemSO in AssetManager.AllItems)
        {
            addItem(itemSO);
        }
    }

    public void addItem(ItemSO so)
    {
        items.Add(new BaseItem(so));
    }

    public void RemoveItem(BaseItem item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
        else
        {
            
            Debug.LogError("no item in inventory "+item.itemSO.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
