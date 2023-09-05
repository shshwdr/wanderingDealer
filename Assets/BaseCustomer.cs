using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCustomer : MonoBehaviour
{

    public CustomerSO customerSO;

    public BaseCustomer(CustomerSO so)
    {
        customerSO = so;
    }
}
