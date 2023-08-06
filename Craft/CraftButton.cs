using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftButton : VendorButton, IPointerClickHandler
{
    private List<Item> recipe;
    private string itemRecipe;

    private void Start()
    {
        recipe = MyVendorItem.MyItem.MyRecipe;
        AddItem(MyVendorItem);
    }

    public override void MaterialsOrGoldNeeded()
    {
        foreach(Item item in recipe)
        {
            MyPrice.text += item.MyTitle + "\n" ;
        }
    }
    private bool CanCraft()
    {
        bool canCraft = true;

        

        foreach(Item item in recipe)
        {
            int count = InventoryScript.MyInstance.GetItemCount(item.MyTitle);

            if(count > 0)
            {
                continue;
            }
            else
            {
                canCraft = false;
                break;
            }
        }
        return canCraft;
     }



    public override void OnPointerClick(PointerEventData eventData)
    {
        if (CanCraft() && InventoryScript.MyInstance.AddItem(MyVendorItem.MyItem))
        {
            foreach(Item item in recipe)
            {
                InventoryScript.MyInstance.RemoveItem(item);
            }
        }
    }
}
