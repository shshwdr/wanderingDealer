using System.Collections;
using System.Collections.Generic;
using DG.Util;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public int gold;
    public ResourceMenu menu;
    public void AddGold(int amount)
    {
        gold += amount;
        menu.UpdateGold();
    }
}
