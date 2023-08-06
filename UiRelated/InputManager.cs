using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool inventoryOpen = false;
    public bool lootOpen = false;
    public bool craftOpen = false;
    public bool vendorOpen = false;
    public bool quitOpen = false;
    public bool chestOpen = false;

    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject lootWindow;
    [SerializeField] private GameObject craftWindow;
    [SerializeField] private GameObject vendorWindow;
    [SerializeField] private GameObject quitWindow;
    [SerializeField] private GameObject chestWindow;
    

    private static InputManager instance;
    public static InputManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<InputManager>();
                instance = GameObject.FindObjectOfType<InputManager>();
            }
            return instance;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryOpen == false)
            {
                inventory.GetComponent<CanvasGroup>().alpha = 1;
                inventory.GetComponent<CanvasGroup>().blocksRaycasts = true;
                
                inventoryOpen = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if(inventoryOpen == true)
            {
                inventory.GetComponent<CanvasGroup>().alpha = 0;
                inventory.GetComponent<CanvasGroup>().blocksRaycasts = false;
                
                inventoryOpen = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!lootOpen && !vendorOpen && !craftOpen && !inventoryOpen && !chestOpen)
            {
                if (!quitOpen)
                {
                    quitOpen = true;
                    quitWindow.GetComponent<CanvasGroup>().alpha = 1;
                    quitWindow.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    quitOpen = false;
                    quitWindow.GetComponent<CanvasGroup>().alpha = 0;
                    quitWindow.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
            if (lootOpen == true)
            {
                lootWindow.GetComponent<CanvasGroup>().alpha = 0;
                lootWindow.GetComponent<CanvasGroup>().blocksRaycasts = false;
                lootOpen = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (vendorOpen)
            {
                vendorWindow.GetComponent<VendorWindow>().Close();
                
            }
            if (craftOpen)
            {
                craftWindow.GetComponent<CraftWindow>().Close();
                
            }
            if (inventoryOpen)
            {
                inventory.GetComponent<CanvasGroup>().alpha = 0;
                inventory.GetComponent<CanvasGroup>().blocksRaycasts = false;

                inventoryOpen = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (chestOpen)
            {
                chestWindow.GetComponent<CanvasGroup>().alpha = 0;
                chestWindow.GetComponent<CanvasGroup>().blocksRaycasts = false;
                chestOpen = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
           
        }
    }
}
