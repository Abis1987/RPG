using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    private ObservableStack<Item> items = new ObservableStack<Item>();
    

    [SerializeField] private Image icon;
    [SerializeField] private TMPro.TextMeshProUGUI stackSize;
    private SlotHolderScript slotholder;

    public bool IsEmpty
    {
        get
        {
            return items.Count == 0;
        }
    }

    public bool IsFull
    {
        get
        {
            if (IsEmpty || MyCount < MyItem.MyStackSize )
            {
                return false;
            }

            return true;
            
        }
    }

    public Item MyItem
    {
        get
        {
            if(!IsEmpty)
            {
                return items.Peek();
            }
            return null;
        }
    }

    public ObservableStack<Item> MyItems
    {
        get
        {
            return items;
        }
    }

    public Image MyIcon { get => icon; set => icon = value; }

    public int MyCount
    {
        get { return items.Count; }
    }

    public TMPro.TextMeshProUGUI MyStackText
    {
        get
        {
            return stackSize;
        }
    }

    public SlotHolderScript MySlotholder { get => slotholder; set => slotholder = value; }

    private void Awake()
    {
        items.OnPop += new UpdateStackEvent(UpdateSlot);
        items.OnPush += new UpdateStackEvent(UpdateSlot);
        items.OnClear += new UpdateStackEvent(UpdateSlot);
    }

    public bool AddItem(Item item)
    {
        
        items.Push(item);

        icon.sprite = item.Icon;
        icon.color = Color.white;
        item.MySlot = this;
        if (item is Armor)
        {
            (item as Armor).UpdateSkillTree();
        }

        return true;
    }

    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {

            InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
            
        }
    }

    public void Clear()
    {
        int initCount = MyItems.Count;

        if (initCount > 0)
        {

            for (int i = 0; i < initCount; i++)
            {
                InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
            }
            
            
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (InventoryScript.MyInstance.FromSlot == null && !IsEmpty)
            {
                if (HandScript.MyInstance.MyMoveable != null)
                {
                    if (HandScript.MyInstance.MyMoveable is Armor)
                    {
                        if (MyItem is Armor && (MyItem as Armor).MyArmorType == (HandScript.MyInstance.MyMoveable as Armor).MyArmorType)
                        {
                            (MyItem as Armor).Equip();
                            InventoryScript.MyInstance.AddItem((Item)HandScript.MyInstance.MyMoveable);
                            HandScript.MyInstance.Drop();
                        }
                    }
                }

                else if(HandScript.MyInstance.MyMoveable == null)
                {
                    HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
                    InventoryScript.MyInstance.FromSlot = this;
                }
                



            }
            else if(InventoryScript.MyInstance.FromSlot == null && IsEmpty && HandScript.MyInstance.MyMoveable != null)
            {
                InventoryScript.MyInstance.AddItem((Item)HandScript.MyInstance.MyMoveable);
                HandScript.MyInstance.Drop();
            }
            
            else if (InventoryScript.MyInstance.FromSlot == null && IsEmpty)
            {
                if(HandScript.MyInstance.MyMoveable is Armor)
                {
                    Armor armor = (Armor)HandScript.MyInstance.MyMoveable;
                    InventoryScript.MyInstance.MySelectedButton.DequipArmor();
                    AddItem(armor);
                    HandScript.MyInstance.Drop();
                }
            }
            else if (InventoryScript.MyInstance.FromSlot != null )
            {
                if (PutItemBack() || MergeItems(InventoryScript.MyInstance.FromSlot) || SwapItems(InventoryScript.MyInstance.FromSlot) || AddItems(InventoryScript.MyInstance.FromSlot.items))
                {
                    HandScript.MyInstance.Drop();
                    InventoryScript.MyInstance.FromSlot = null;
                }
            }
        }


        if (eventData.button == PointerEventData.InputButton.Right && HandScript.MyInstance.MyMoveable == null)
        {
            MyItem.MySlot = this;
            UseItem();
        }
    }

    public void UseItem()
    {
        if (MyItem is IUsable)
        {

            (MyItem as IUsable).Use();
        }

        else if(MyItem is Armor)
        {
            (MyItem as Armor).Equip();
            //RemoveItem(MyItem);
        }

    }

    public bool StackItem(Item item)
    {
        if (!IsEmpty && item.name == MyItem.name && items.Count < MyItem.MyStackSize )
        {
            items.Push(item);
            item.MySlot = this;
            return true;
        }
        return false;
        
    }

    private bool PutItemBack()
    {
        if (InventoryScript.MyInstance.FromSlot == this)
        {
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            return true;
        }
        return false;
    }


    public bool AddItems(ObservableStack<Item> newItems)
    {
      
            if (IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
            {
                int count = newItems.Count;

                for (int i = 0; i < count; i++)
                {
                    if (IsFull)
                    {
                        return false;
                    }

                    AddItem(newItems.Pop());
                }

                return true;
            }

            
      
        return false;
    }

    private bool SwapItems(SlotScript from)
    {
        if (MyItem is Armor)
        {
            (MyItem as Armor).UpdateSkillTree();
        }
        if (from.items.Peek() is Armor)
        {
            (from.items.Peek() as Armor).UpdateSkillTree();
        }
        if (IsEmpty)
        {
            return false;
        }
        if(from.MyItem.GetType() != MyItem.GetType() || from.MyCount+MyCount > MyItem.MyStackSize)
        {
           
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.items);
            from.items.Clear();
            if(items.Peek().MyStackSize > 0)
            {
                from.AddItems(items);
            }
            else
            {
               from.AddItem(MyItem);
            }
            

            items.Clear();
            if(tmpFrom.Peek().MyStackSize > 0)
            {
                AddItems(tmpFrom);
            }
            else
            {
                AddItem(tmpFrom.Peek());
            }
          

                return true;
        }
        
        
        return false;
    }

    private bool MergeItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() == MyItem.GetType() && !IsFull)
        {
            int free = MyItem.MyStackSize - MyCount;

            for (int i = 0; i < free; i++)
            {
                AddItem(from.items.Pop());
            }

            return true;
        }

        return false;
    }

    private void UpdateSlot()
    {
        UIManager.MyInstance.UpdateStackSize(this);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsEmpty)
        {
            UIManager.MyInstance.ShowTooltip(MyItem);
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
