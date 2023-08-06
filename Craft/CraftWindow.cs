using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftWindow : VendorWindow
{
    private bool isOpen = false;

    [SerializeField] public CraftTable table;
    [SerializeField] CraftButton[] craftButtons;

    public bool IsOpen { get => isOpen; }

    public override void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        isOpen = false;
        InputManager.MyInstance.craftOpen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void Open()
    {
        isOpen = true;
        ClearButtons();
        AddItems();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        InputManager.MyInstance.craftOpen = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
   
}
