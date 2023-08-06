using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTable : Vendor
{

    [SerializeField] CraftWindow craftWindow;
    [SerializeField] GameObject fire;

    public override void Interact()
    {
        if(fire != null)
        {
            Instantiate(fire, transform.position, Quaternion.identity);
        }
        

        if (!craftWindow.IsOpen)
        {
            
            craftWindow.Open();
            InputManager.MyInstance.craftOpen = true;
        }
    }
    public override void StopInteract()
    {
        

        craftWindow.Close();

        
    }
    
}
