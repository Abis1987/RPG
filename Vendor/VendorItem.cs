using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VendorItem 
{
    [SerializeField] private Item item;

    public Item MyItem { get => item; }
}
