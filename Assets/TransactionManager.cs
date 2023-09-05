using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Util;
using Unity.Mathematics;
using UnityEngine;


public class TransactionManager : Singleton<TransactionManager>
{
    private List<Transaction> transactionList = new List<Transaction>();
    public TransactionMenu transactionMenu;
    public int customerCount = 3;
    private void Start()
    {
        //randomly add some transactions

        var CustomerList = AssetManager.AllCustomers.ToList();
        var itemList = InventoryManager.Instance.items.ToList();
        
        for (int i = 0; i < customerCount; i++)
        {
            AddTransaction(CustomerList.PickItem(),itemList.PickItem(),AssetManager.AllTransactions.RandomItem());
        }
        ShowTransaction();
    }

    private Transaction currentTransaction => transactionList.Count == 0 ? null : transactionList[0];
    public void AddTransaction(CustomerSO customer, BaseItem item, TransactionSO tran)
    {
        var transaction = new Transaction(customer, item, tran);
        transactionList.Add(transaction);
        transactionMenu.addTransaction(transaction);
    }

    public void ShowTransaction()
    {
        transactionMenu.showWaitForTransaction(currentTransaction);
    }
    
    public void StartTransaction()
    {
        //customer start to bid, wait for player's
        
        transactionMenu.showCustomerBid(currentTransaction);
        
    }

    public void SwitchToNextCustomer()
    {
        
        var first = transactionList[0];
        transactionList.RemoveAt(0);
        transactionList.Add(first);
        ShowTransaction();
    }

    public void Bid(int price)
    {
        // 
        currentTransaction.addPlayerBid(price);
        transactionMenu.showCustomerBid(currentTransaction);
        
    }

    public void SucceedTransaction(int finalValue)
    {
        // get reward
        ResourceManager.Instance.AddGold(finalValue);
        // pay item
        InventoryManager.Instance.RemoveItem(currentTransaction.item);
        
        EndOneTransaction();
    }

    public void CancelTransaction()
    {
        EndOneTransaction();
    }
    
    public void EndOneTransaction()
    {
        
        // customer leave, next one
        var first = transactionList[0];
        transactionList.RemoveAt(0);

        if (transactionList.Count > 0)
        {
            ShowTransaction();
        }
        else
        {
            //if no one, finish day
            Debug.Log("next day");
        }
    }
    
}
