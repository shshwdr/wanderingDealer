using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public Transform parent;

    public GameObject cellPrefab;

    // Start is called before the first frame update
    void Start()
    {
        CloseMenu();
    }


    public void OpenMenu()
    {
        gameObject.SetActive(true);
        var items = InventoryManager.Instance.items;
        var allCells = parent.GetComponentsInChildren<ItemCell>(true);
        if (items.Count > allCells.Length)
        {
            for (int ii = 0; ii < items.Count - allCells.Length; ii++)
            {
                var cell = Instantiate(cellPrefab, parent);
                
            }
        }
        allCells = parent.GetComponentsInChildren<ItemCell>(true);
        int i =0;
        for (; i < items.Count; i++)
        {
            allCells[i].gameObject.SetActive(true);
             allCells[i].init(items[i]);
        }
        for (; i < allCells.Length; i++)
        {
            allCells[i].gameObject.SetActive(false);
        }
    }
    
    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
