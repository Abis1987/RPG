using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : NPC
{
    [SerializeField] private GameObject chestUI;
    public override void Interact()
    {
        InputManager.MyInstance.chestOpen = true;
        chestUI.GetComponent<CanvasGroup>().alpha = 1;
        chestUI.GetComponent<CanvasGroup>().blocksRaycasts = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
