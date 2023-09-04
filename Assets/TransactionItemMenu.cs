using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransactionItemMenu : MonoBehaviour
{
    public Text itemName;

    public Text itemMyGuessPrice;

    public Text itemCurrentPrice;

    public void init(string n, int guessPrice, int currentPrice )
    {
        itemName.text = n;
        itemMyGuessPrice.text = "估计价格："+ guessPrice.ToString();
        itemCurrentPrice.text = "当前价格："+ currentPrice.ToString();
    }
}
