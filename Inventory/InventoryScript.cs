using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ItemCountChanged(Item item);
public class InventoryScript : MonoBehaviour
{
    public event ItemCountChanged itemCountChangedEvent;
    

    [SerializeField] private SlotHolderScript slotHolder;
    [SerializeField] private Item[] items;
    
    private List<SlotScript> slots = new List<SlotScript>();

    private SlotScript fromSlot;

    private static InventoryScript instance;

   

    public static InventoryScript MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }
            return instance;
        }
    }

    

    public SlotScript FromSlot
    {
        get
        {
            return fromSlot;
        }

        set
        {
            fromSlot = value;

            //if (value != null)
            //{
            //    fromSlot.MyIcon.color = Color.grey;
            //}
        }

    }

    public bool AddItem(Item item)
    {
       
            if (item.MyStackSize > 0)
            {
                if (PlaceInStack(item))
                {
                    return true;
                }
            }

            return PlaceInEmpty(item);

    }

    public bool PlaceInEmpty(Item item)
    {
        if (slotHolder.AddItem(item))
        {
            OnItemCountChanged(item);
            return true;
        }
        return false;
    }

    private bool PlaceInStack(Item item)
    {
        foreach (SlotScript slots in slotHolder.MySlots)
        {
            if (slots.StackItem(item))
            {
                OnItemCountChanged(item);
                return true;
            }
        }
        return false;
    }

    public Stack<IUsable>GetUsables(IUsable type)
    {
        Stack<IUsable> usables = new Stack<IUsable>();

        foreach (SlotScript slot in slotHolder.MySlots)
        {
            if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
            {
                foreach (Item item in slot.MyItems)
                {
                    usables.Push(item as IUsable);
                }
            }
        }
        return usables;
    }

    public void OnItemCountChanged(Item item)
    {
        if(itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }

    }
    public int GetItemCount(string type)
    {
        int itemCount = 0;

        foreach (SlotScript slot in slotHolder.MySlots)
        {
            if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
            {
                itemCount += slot.MyItems.Count;
            }
        }
        return itemCount;
    }

    public void RemoveItem(Item item)
    {
        foreach(SlotScript slot in slotHolder.MySlots)
        {
            if(!slot.IsEmpty && slot.MyItem.MyTitle == item.MyTitle)
            {
                slot.RemoveItem(item);
                break;
            }
        }
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Potion potion = (Potion)Instantiate(items[0]);
            AddItem(potion);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Armor armor = (Armor)Instantiate(items[1]);
            AddItem(armor);
        }
    }

    private bool light1Active, light2Active, light3Active, light4Active, light5Active, dark1Active, dark2Active, dark3Active, dark4Active, dark5Active, arcane1Active, arcane2Active, arcane3Active,
                 arcane4Active, arcane5Active = false;

    

    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private GameObject _light1, _light2, _light3, _light4, _light5, _dark1, _dark2, _dark3, _dark4, _dark5, _arcane1, _arcane2, _arcane3, _arcane4, _arcane5;

    [SerializeField]
    private CharButton head, shoulders, chest, hands, legs, feet, main, off, twohand, skill1, skill2, skill3, skill4, skill5, light1, light2, light3, light4,
                                        light5, dark1, dark2, dark3, dark4, dark5, arcane1, arcane2, arcane3, arcane4, arcane5;

    public CharButton MySelectedButton { get; set; }
    public CharButton Skill1 { get => skill1; }
    public CharButton Skill2 { get => skill2; }
    public CharButton Skill3 { get => skill3; }
    public CharButton Skill4 { get => skill4; }
    public CharButton Skill5 { get => skill5; }

    //public void OpenClose()
    //{
    //    if (canvasGroup.alpha <= 0)
    //    {
    //        canvasGroup.blocksRaycasts = true;
    //        canvasGroup.alpha = 1;
    //    }
    //    else
    //    {
    //        canvasGroup.blocksRaycasts = false;
    //        canvasGroup.alpha = 0;
    //    }
    //}

    public void EquipArmor(Armor armor)
    {
        switch (armor.MyArmorType)
        {
            case ArmorType.Head:
                head.EquipArmor(armor);
                break;
            case ArmorType.Shoulders:
                shoulders.EquipArmor(armor);
                break;
            case ArmorType.Chest:
                chest.EquipArmor(armor);
                break;
            case ArmorType.Hands:
                hands.EquipArmor(armor);
                break;
            case ArmorType.Legs:
                legs.EquipArmor(armor);
                break;
            case ArmorType.Feet:
                feet.EquipArmor(armor);
                break;
            case ArmorType.MainHand:
                main.EquipArmor(armor);
                break;
            case ArmorType.Offhand:
                off.EquipArmor(armor);
                break;
            case ArmorType.Twohand:
                twohand.EquipArmor(armor);
                break;
            //case ArmorType.Skill:
            //    skill1.EquipArmor(armor);
            //    break;
            case ArmorType.Light1:
                if (light1Active)
                {
                    light1.EquipArmor(armor);
                }
                break;
            case ArmorType.Light2:
                if (light2Active)
                {
                    light2.EquipArmor(armor);
                }
                break;
            case ArmorType.Light3:
                if (light3Active)
                {
                    light3.EquipArmor(armor);
                }
                break;
            case ArmorType.Light4:
                if (light4Active)
                {
                    light4.EquipArmor(armor);
                }
                break;
            case ArmorType.Light5:
                if (light5Active)
                {
                    light5.EquipArmor(armor);
                }
                break;
            case ArmorType.Dark1:
                if (dark1Active)
                {
                    dark1.EquipArmor(armor);
                }
                break;
            case ArmorType.Dark2:
                if (dark2Active)
                {
                    dark2.EquipArmor(armor);
                }
                break;
            case ArmorType.Dark3:
                if (dark3Active)
                {
                    dark3.EquipArmor(armor);
                }
                break;
            case ArmorType.Dark4:
                if (dark4Active)
                {
                    dark4.EquipArmor(armor);
                }
                break;
            case ArmorType.Dark5:
                if (dark5Active)
                {
                    dark5.EquipArmor(armor);
                }
                break;
            case ArmorType.Arcane1:
                if (arcane1Active)
                {
                    arcane1.EquipArmor(armor);
                }
                break;
            case ArmorType.Arcane2:
                if (arcane2Active)
                {
                    arcane2.EquipArmor(armor);
                }
                break;
            case ArmorType.Arcane3:
                if (arcane3Active)
                {
                    arcane3.EquipArmor(armor);
                }
                break;
            case ArmorType.Arcane4:
                if (arcane4Active)
                {
                    arcane4.EquipArmor(armor);
                }
                break;
            case ArmorType.Arcane5:
                if (arcane5Active)
                {
                    arcane5.EquipArmor(armor);
                }
                break;
        }
    }

    public void ActivateLightSkillTreeSlots()
    {
        if (Player.MyInstance.MyLightPoints >= 3)
        {
            light1Active = true;
            _light1.SetActive(true);
        }
        else
        {
            if (light1.MyEquipedArmor != null)
            {
                Item tmp = light1.MyEquipedArmor;

                light1.DequipArmor();
                AddItem(tmp);

            }
            light1Active = false;
            _light1.SetActive(false);
        }
        if (Player.MyInstance.MyLightPoints >= 7)
        {
            light2Active = true;
            _light2.SetActive(true);
        }
        else
        {
            if (light2.MyEquipedArmor != null)
            {
                Item tmp = light2.MyEquipedArmor;

                light2.DequipArmor();
                AddItem(tmp);
            }
            light2Active = false;
            _light2.SetActive(false);
        }
        if (Player.MyInstance.MyLightPoints >= 12)
        {
            light3Active = true;
            _light3.SetActive(true);
        }
        else
        {
            if (light3.MyEquipedArmor != null)
            {
                Item tmp = light3.MyEquipedArmor;

                light3.DequipArmor();
                AddItem(tmp);
            }
            light3Active = false;
            _light3.SetActive(false);
        }
        if (Player.MyInstance.MyLightPoints >= 17)
        {
            light4Active = true;
            _light4.SetActive(true);
        }
        else
        {
            if (light4.MyEquipedArmor != null)
            {
                Item tmp = light4.MyEquipedArmor;

                light4.DequipArmor();
                AddItem(tmp);
            }
            light4Active = false;
            _light4.SetActive(false);
        }
        if (Player.MyInstance.MyLightPoints >= 25)
        {
            light5Active = true;

            _light5.SetActive(true);
        }
        else
        {
            if (light5.MyEquipedArmor != null)
            {
                Item tmp = light5.MyEquipedArmor;

                light5.DequipArmor();
                AddItem(tmp);
            }
            light5Active = false;
            _light5.SetActive(false);
        }

    }

    public void ActivateDarkSkillTreeSlots()
    {
        if (Player.MyInstance.MyDarkPoints >= 3)
        {
            dark1Active = true;
            _dark1.SetActive(true);
        }
        else
        {
            if (dark1.MyEquipedArmor != null)
            {
                Item tmp = dark1.MyEquipedArmor;

                dark1.DequipArmor();
                AddItem(tmp);
            }
            dark1Active = false;
            _dark1.SetActive(false);
        }
        if (Player.MyInstance.MyDarkPoints >= 7)
        {
            dark2Active = true;
            _dark2.SetActive(true);
        }
        else
        {
            if (dark2.MyEquipedArmor != null)
            {
                Item tmp = dark2.MyEquipedArmor;

                dark2.DequipArmor();
                AddItem(tmp);
            }
            dark2Active = false;
            _dark2.SetActive(false);
        }
        if (Player.MyInstance.MyDarkPoints >= 12)
        {
            dark3Active = true;
            _dark3.SetActive(true);
        }
        else
        {
            if (dark3.MyEquipedArmor != null)
            {
                Item tmp = dark3.MyEquipedArmor;

                dark3.DequipArmor();
                AddItem(tmp);
            }
            dark3Active = false;
            _dark3.SetActive(false);
        }
        if (Player.MyInstance.MyDarkPoints >= 17)
        {
            dark4Active = true;
            _dark4.SetActive(true);
        }
        else
        {
            if (dark4.MyEquipedArmor != null)
            {
                Item tmp = dark4.MyEquipedArmor;

                dark4.DequipArmor();
                AddItem(tmp);
            }
            dark4Active = false;
            _dark4.SetActive(false);
        }
        if (Player.MyInstance.MyDarkPoints >= 25)
        {
            dark5Active = true;
            _dark5.SetActive(true);
        }
        else
        {
            if (dark5.MyEquipedArmor != null)
            {
                Item tmp = dark5.MyEquipedArmor;

                dark5.DequipArmor();
                AddItem(tmp);
            }
            dark5Active = false;
            _dark5.SetActive(false);
        }
    }

    public void ActivateArcaneSkillTreeSlots()
    {
        if (Player.MyInstance.MyArcanePoints >= 3)
        {
            arcane1Active = true;
            _arcane1.SetActive(true);
        }
        else
        {
            if (arcane1.MyEquipedArmor != null)
            {
                Item tmp = arcane1.MyEquipedArmor;

                arcane1.DequipArmor();
                AddItem(tmp);
            }
            arcane1Active = false;
            _arcane1.SetActive(false);
        }
        if (Player.MyInstance.MyArcanePoints >= 7)
        {
            arcane2Active = true;
            _arcane2.SetActive(true);
        }
        else
        {
            if (arcane2.MyEquipedArmor != null)
            {
                Item tmp = arcane2.MyEquipedArmor;

                arcane2.DequipArmor();
                AddItem(tmp);
            }
            arcane2Active = false;
            _arcane2.SetActive(false);
        }
        if (Player.MyInstance.MyArcanePoints >= 12)
        {
            arcane3Active = true;
            _arcane3.SetActive(true);
        }
        else
        {
            if (arcane3.MyEquipedArmor != null)
            {
                Item tmp = arcane3.MyEquipedArmor;

                arcane3.DequipArmor();
                AddItem(tmp);
            }
            arcane3Active = false;
            _arcane3.SetActive(false);
        }
        if (Player.MyInstance.MyArcanePoints >= 17)
        {
            arcane4Active = true;
            _arcane4.SetActive(true);
        }
        else
        {
            if (arcane4.MyEquipedArmor != null)
            {
                Item tmp = arcane4.MyEquipedArmor;

                arcane4.DequipArmor();
                AddItem(tmp);
            }
            arcane4Active = false;
            _arcane4.SetActive(false);

        }
        if (Player.MyInstance.MyArcanePoints >= 25)
        {
            arcane5Active = true;
            _arcane5.SetActive(true);
        }
        else
        {
            if (arcane5.MyEquipedArmor != null)
            {
                Item tmp = arcane5.MyEquipedArmor;

                arcane5.DequipArmor();
                AddItem(tmp);
            }
            arcane5Active = false;
            _arcane5.SetActive(false);
        }
    }
}
