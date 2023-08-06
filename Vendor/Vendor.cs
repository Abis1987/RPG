using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : NPC, IInteractable
{
    [SerializeField] private VendorItem[] Items;

    [SerializeField] private VendorWindow vendorWindow;

    

    public bool IsOpen { get; set; }

    public override void Interact()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            vendorWindow.CreatePages(Items);
            vendorWindow.Open();
        }
        
    }

    public override void StopInteract()
    {
        IsOpen = false;
        
        vendorWindow.Close();
    }

  
}
