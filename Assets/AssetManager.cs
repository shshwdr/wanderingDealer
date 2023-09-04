using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static List<ItemSO> AllItems { private set; get; }
    public static List<CustomerSO> AllCustomers { private set; get; }
    public static List<TransactionSO> AllTransactions { private set; get; }
    // Start is called before the first frame update
    void Awake()
    {
        AllItems = new List<ItemSO>(Resources.LoadAll<ItemSO>("SO/Item"));
        AllCustomers = new List<CustomerSO>(Resources.LoadAll<CustomerSO>("SO/Customer"));
        AllTransactions = new List<TransactionSO>(Resources.LoadAll<TransactionSO>("SO/Transaction"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
