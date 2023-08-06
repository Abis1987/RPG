using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : NPC
{
    [SerializeField] private Item item;

    [SerializeField] private GameObject obj;

    


    public override void Interact()
    {
        InventoryScript.MyInstance.AddItem(item);

        Destroy(obj);
    }

}
