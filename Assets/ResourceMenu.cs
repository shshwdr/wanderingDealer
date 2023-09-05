using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceMenu : MonoBehaviour
{
    public Text goldText;

    public void UpdateGold()
    {
        goldText.text = "金币：" + ResourceManager.Instance.gold;
    }

}
