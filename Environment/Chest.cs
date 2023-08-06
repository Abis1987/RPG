using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : NPC
{
    private bool isOpen;
    public override void Interact()
    {
        if (isOpen)
        {
            StopInteract();
        }
        else
        {
            isOpen = true;

        }
    }

    public override void StopInteract()
    {
        
    }
}
