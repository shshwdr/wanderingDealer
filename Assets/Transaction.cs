using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Transaction
{
    public CustomerSO customer;
    public BaseItem item;
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
    
    
    public Transaction(CustomerSO c, BaseItem i, TransactionSO t)
    {
        customer = c;
        item = i;
        transaction = t;
    }
}

