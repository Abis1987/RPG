using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VendorButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image icon;
    
    [SerializeField] private TMPro.TextMeshProUGUI title;
    
    [SerializeField] private TMPro.TextMeshProUGUI price;

    [SerializeField] private VendorItem vendorItem;

    public VendorItem MyVendorItem { get => vendorItem; }
    public TextMeshProUGUI MyPrice { get => price; }
    public Image MyIcon { get => icon; set => icon = value; }

    public void AddItem(VendorItem vendorItem)
    {
        this.vendorItem = vendorItem;
        icon.sprite = vendorItem.MyItem.MyIcon;
        title.text = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[vendorItem.MyItem.MyQuality], vendorItem.MyItem.MyTitle);
        MaterialsOrGoldNeeded();
        gameObject.SetActive(true);
        
    }

    public virtual void MaterialsOrGoldNeeded()
    {
        price.text = vendorItem.MyItem.MyPrice.ToString();
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if ((Player.MyInstance.MyGold >= vendorItem.MyItem.MyPrice) && InventoryScript.MyInstance.AddItem(Instantiate(vendorItem.MyItem)))
        {
            SellItem();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.MyInstance.ShowTooltip(vendorItem.MyItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }

    public void SellItem()
    {
        Player.MyInstance.MyGold -= vendorItem.MyItem.MyPrice;
                
    }
}
