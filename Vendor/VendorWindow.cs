using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorWindow : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;

    [SerializeField] private VendorButton[] vendorButtons;

    private List<List<VendorItem>> pages = new List<List<VendorItem>>();

    [SerializeField] private TMPro.TextMeshProUGUI pageNumber;

    [SerializeField] protected Vendor vendor;

    private int pageIndex;

    public Vendor MyVendor { get => vendor; }
    public List<List<VendorItem>> Pages { get => pages; }

    public void CreatePages(VendorItem[] items)
    {

        pages.Clear();
        List<VendorItem> page = new List<VendorItem>();
        for (int i = 0; i < items.Length; i++)
        {
            page.Add(items[i]);

            if(page.Count == 21 || i == items.Length -1)
            {
                pages.Add(page);
                page = new List<VendorItem>();
            }
        }

        AddItems();
    }

    public void AddItems()
    {
        //for (int i = 0; i < items.Length; i++)
        //{
        //    vendorButtons[i].AddItem(items[i]);
        //}
        pageNumber.text = pageIndex + 1 + "/" + pages.Count;

        if(pages.Count > 0)
        {
            for (int i = 0; i < pages[pageIndex].Count; i++)
            {
                if(pages[pageIndex][i] != null)
                {
                    vendorButtons[i].AddItem(pages[pageIndex][i]);
                }
            }
        }
    }
    public virtual void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        vendor.IsOpen = false;
        InputManager.MyInstance.vendorOpen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public virtual void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        InputManager.MyInstance.vendorOpen = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void NextPage()
    {
        if(pageIndex < pages.Count - 1)
        {
            ClearButtons();
            pageIndex++;
            AddItems();

        }
    }

    public void PreviousPage()
    {
        if(pageIndex > 0)
        {
            ClearButtons();
            pageIndex--;
            AddItems();
        }
    }

    public void ClearButtons()
    {
        foreach (VendorButton btn in vendorButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    
}
