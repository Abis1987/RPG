using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : SlotScript
{
    [SerializeField] private ArmorType armorType;

    private Armor equipedArmor = null;

    //[SerializeField] private Image icon;

    public Armor MyEquipedArmor { get => equipedArmor; }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable is Armor)
            {
                Armor tmp = (Armor)HandScript.MyInstance.MyMoveable;

                if (tmp.MyArmorType == armorType)
                {
                    EquipArmor(tmp);
                }

                UIManager.MyInstance.RefreshTooltip(tmp);
            }
            else if (HandScript.MyInstance.MyMoveable == null && equipedArmor != null)
            {
                HandScript.MyInstance.TakeMoveable(equipedArmor);
                
                DequipArmor();
                
                InventoryScript.MyInstance.MySelectedButton = this;
                //MyIcon.color = Color.grey;
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if(equipedArmor != null)
            {
                InventoryScript.MyInstance.AddItem(equipedArmor);
                DequipArmor();
                
            }
        }
    }

    public void EquipArmor(Armor armor)
    {
        Player.MyInstance.AddArmorStats(armor);
        
        armor.Remove();

        

        if(equipedArmor != null)
        {
            //if(equipedArmor != armor)
            //{  
            Player.MyInstance.RemoveArmorStats(equipedArmor);
            armor.MySlot.AddItem(equipedArmor);
               
            //}
            UIManager.MyInstance.RefreshTooltip(equipedArmor);
        }
        else
        {
            UIManager.MyInstance.HideTooltip();
        }
        MyIcon.enabled = true;
        MyIcon.sprite = armor.MyIcon;
        MyIcon.color = Color.white;
        armor.UpdateSkillTree();
        equipedArmor = armor;
        equipedArmor.MyCharButton = this;
        
        

        if(HandScript.MyInstance.MyMoveable == (armor as IMoveable))
        {
            HandScript.MyInstance.Drop();
        }


            
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (equipedArmor != null)
        {
            UIManager.MyInstance.ShowTooltip(equipedArmor);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }

    public void DequipArmor()
    {
        Player.MyInstance.RemoveArmorStats(equipedArmor);
        equipedArmor.UpdateSkillTree();
        MyIcon.color = Color.white;
        MyIcon.enabled = false;
        equipedArmor.MyCharButton = null;
        Armor tmp = equipedArmor;
        equipedArmor = null;
        
        if (tmp.MyWeapon != null)
        {
            
            
            Player.MyInstance.GetComponent<EquipmentSystem>().DestroyWeapon();

            
        }

       

       
        
        
        

    }
}
