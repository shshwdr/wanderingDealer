using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CustomerSO", menuName = "Customer")]
public class CustomerSO : ScriptableObject
{
    
    public string name;
    public string desc;
    public string alliegance;
    public List<CustomerSO> friends;
    //attributes
}
