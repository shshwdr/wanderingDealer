using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class TransactionMenu : MonoBehaviour
{
    public Transform currentCustomerParent;
    public Transform customerQueueParent;
    public TransactionDialogueMenu dialogueMenu;
    public TransactionItemMenu itemMenu;
    public TransactionPriceMenu priceMenu;

    public Dictionary<Transaction, GameObject> transactionToCustomerGO = new Dictionary<Transaction, GameObject>();
    public void addTransaction(Transaction currentTransaction)
    {
        var customerPrefab = Resources.Load<GameObject>("Prefab/TransactionCustomer");
        var customer = Instantiate(customerPrefab);
        customer.transform.parent = customerQueueParent;
        customer.transform.localScale =Vector3.one* 0.5f;
        transactionToCustomerGO[currentTransaction] = customer;
        
    }
    public void showWaitForTransaction(Transaction currentTransaction)
    {
        var customer = transactionToCustomerGO[currentTransaction];
        if (customer.transform.parent != currentCustomerParent)
        {
            customer.transform.parent = currentCustomerParent;
            customer.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }

        dialogueMenu.init("买家", "我是"+currentTransaction.customer.name + "，我想买"+currentTransaction.item.name,new List<ButtonOption>()
        {
            new ButtonOption("来交易吧",new UnityAction(() =>
            {
                TransactionManager.Instance.StartTransaction();
            })),
            new ButtonOption("让我想想",new UnityAction(() =>
            {
                TransactionManager.Instance.SwitchToNextCustomer();
            })),
        });

        priceMenu.gameObject.SetActive(false);
        itemMenu.gameObject.SetActive(false);
    }

    public void showCustomerBid(Transaction currentTransaction)
    {
        
        priceMenu.gameObject.SetActive(true);
        itemMenu.gameObject.SetActive(true);

        if (currentTransaction.playerBids.Count == 0)
        {
            var actualCost = currentTransaction.item.itemSO.actualCost;
            int customerCost = (int)(actualCost * Random.Range(0.9f, 1.3f));
            currentTransaction.customerBids.Add(customerCost);
            itemMenu.init(currentTransaction.item.name,actualCost,customerCost);
            priceMenu.init(customerCost);
            dialogueMenu.init("买家", customerCost + "是我的价格",new List<ButtonOption>()
            {
            
            });
        }
        else
        {
            var actualCost = currentTransaction.item.itemSO.actualCost;
            int customerCost = (int)(actualCost * Random.Range(0.9f, 1.3f));
            var lastPlayerBid = currentTransaction.playerBids.LastItem();
            var lastCustomerBid = currentTransaction.customerBids.LastItem();
            if (lastPlayerBid <= lastCustomerBid)
            {
                // player offer something less or equal, that's nice
                
                dialogueMenu.init("买家", "完美的交易！",new List<ButtonOption>()
                {
                    new ButtonOption("和气生财",new UnityAction(() =>
                    {
                        TransactionManager.Instance.SucceedTransaction(currentTransaction.playerBids.LastItem());
                    })),
                });
            }
            else
            {
                var customerGetAngryPrice = math.min( actualCost * 1.4f,currentTransaction.playerClosestBid());
                int customerCanAcceptPrice =(int)( Random.Range(lastCustomerBid, actualCost * Random.Range(1.2f, 1.4f)));
                if (lastPlayerBid <= customerGetAngryPrice)
                {
                    // player offer something higher, but customer is ok with that
                    dialogueMenu.init("买家", "我考虑一下。。",new List<ButtonOption>()
                    {
                        new ButtonOption("这个超值的",new UnityAction(() =>
                        {
                            if (lastPlayerBid <= customerCanAcceptPrice)
                            {
                                
                                dialogueMenu.init("买家", "勉为其难的接受吧！",new List<ButtonOption>()
                                {
                                    new ButtonOption("下次再来",new UnityAction(() =>
                                    {
                                        TransactionManager.Instance.SucceedTransaction(currentTransaction.playerBids.LastItem());
                                    })),
                                });
                            }
                            else
                            {
                                dialogueMenu.init("买家", "不行，"+customerCanAcceptPrice+ "怎么样？",new List<ButtonOption>()
                                {
                                });
                                
                                priceMenu.init(customerCanAcceptPrice);
                                currentTransaction.customerBids.Add(customerCanAcceptPrice);
                                itemMenu.init(currentTransaction.item.name,actualCost,customerCanAcceptPrice);
                                priceMenu.gameObject.SetActive(true);
                                itemMenu.gameObject.SetActive(true);
                            }
                        })),
                    });
                }
                else
                {
                    dialogueMenu.init("买家", "不可能！我只能接受"+customerCanAcceptPrice,new List<ButtonOption>()
                    {
                        new ButtonOption("好吧好吧",new UnityAction(() =>
                        {
                            
                            TransactionManager.Instance.SucceedTransaction(customerCanAcceptPrice);
                        })),
                        new ButtonOption("不行我不能接受",new UnityAction(() =>
                        {
                            TransactionManager.Instance.CancelTransaction();
                        })),
                    });
                    
                    itemMenu.init(currentTransaction.item.name,actualCost,customerCanAcceptPrice);
                    priceMenu.gameObject.SetActive(false);
                }
            }
        }
        
    }
}
