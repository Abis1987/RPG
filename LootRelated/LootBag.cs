using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : NPC
{
    [SerializeField] protected GameObject bag;

    [SerializeField] protected LootTable lootTable;

    
   
    public override void Interact()
    {
        InputManager.MyInstance.lootOpen = true;
        lootTable.ShowLoot();
        
    }

    public override void StopInteract()
    {
        Destroy(bag);
        LootWindow.MyInstance.Close();
        InteractWithInteractable.MyInstance.lootBagEmpty = false;
    }
}
