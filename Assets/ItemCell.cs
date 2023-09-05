using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour
{
    public Image image;
    public Text name;

    public void init(BaseItem item)
    {
        image.sprite = item.itemSO.image;
        name.text = item.itemSO.name;
    }
}
