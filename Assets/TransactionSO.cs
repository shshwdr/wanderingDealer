using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TransactionType{Buy, Sell}
[CreateAssetMenu(fileName = "TransactionSO", menuName = "Transaction")]
public class TransactionSO : ScriptableObject
{
    public TransactionType transactionType;

}
