using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatLoot : LootBag
{
    public override void StopInteract()
    {
        
        LootWindow.MyInstance.Close();
        
    }
}
