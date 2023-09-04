using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransactionPriceMenu : MonoBehaviour
{
    public Text price;
    public Button OKButton;
    public Button cancelButton;
    public Button upButton;
    public Button downButton;
    private int priceValue;

    private int priceChangePerClick = 0;
    
    private void Start()
    {
        OKButton.onClick.AddListener(() =>
        {
            TransactionManager.Instance.Bid(priceValue);
        });
        cancelButton.onClick.AddListener(() =>
        {
            TransactionManager.Instance.CancelTransaction();
        });
        upButton.onClick.AddListener(() =>
        {
            priceValue += priceChangePerClick;
            
            this.price.text = priceValue.ToString();
        });
        downButton.onClick.AddListener(() =>
        {
            priceValue -= priceChangePerClick;
            
            this.price.text = priceValue.ToString();
        });
    }

    public void init(int price)
    {
        priceValue = price;
        this.price.text = price.ToString();
        priceChangePerClick =(int)( price * 0.1f);
    }
}
