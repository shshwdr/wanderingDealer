using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public string name;
    public string desc;
    public int actualCost;
    public Sprite image;
}
