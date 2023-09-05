using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem.ChatMapper;
using UnityEngine;
public enum ItemState{}
public class BaseItem
{
    public string name => itemSO.name;
    public ItemSO itemSO;

    public BaseItem(ItemSO so)
    {
        itemSO = so;
    }
}
