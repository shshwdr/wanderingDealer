using System;
using System.Collections;
using System.Collections.Generic;
using DG.Util;
using Unity.Mathematics;
using UnityEngine;

public class Transaction
{
    public CustomerSO customer;
    public ItemSO item;
    public TransactionSO transaction;

    public List<int> playerBids=new List<int>();
    public List<int> customerBids = new List<int>();
    public int playerClosestBid()
    {
        var res = int.MaxValue;
        foreach (var bid in playerBids)
        {
            res = math.min(res, bid);
        }

        return res;

    }

    public void addPlayerBid(int price)
    {
        playerBids.Add(price);
    }

    public void addCustoemrBid(int price)
    {
        customerBids.Add(price);
    }
    
    
    public Transaction(CustomerSO c, ItemSO i, TransactionSO t)
    {
        customer = c;
        item = i;
        transaction = t;
    }
}

public class TransactionManager : Singleton<TransactionManager>
{
    private List<Transaction> transactionList = new List<Transaction>();
    public TransactionMenu transactionMenu;
    public int customerCount = 3;
    private void Start()
    {
        //randomly add some transactions
        for (int i = 0; i < customerCount; i++)
        {
            AddTransaction(AssetManager.AllCustomers.RandomItem(),AssetManager.AllItems.RandomItem(),AssetManager.AllTransactions.RandomItem());
        }
        ShowTransaction();
    }

    private Transaction currentTransaction => transactionList.Count == 0 ? null : transactionList[0];
    public void AddTransaction(CustomerSO customer, ItemSO item, TransactionSO tran)
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

    public void SucceedTransaction()
    {
        // get reward
        // pay item
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
