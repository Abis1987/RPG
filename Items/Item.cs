using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class Item : ScriptableObject, IMoveable, IDescribable
{
    [SerializeField] private Quality quality;

    [SerializeField] private Sprite icon;

    [SerializeField] private int stackSize;

    [SerializeField] private string title;

    [SerializeField] private int price;

    [SerializeField] List<Item> recipe;

    private SlotScript slot;

    public Sprite Icon => icon;

    public int MyStackSize  => stackSize;

    public SlotScript MySlot { get => slot; set => slot = value; }

    private CharButton charButton;

    
    public Sprite MyIcon => icon;

    public Quality MyQuality { get => quality; }
    public string MyTitle { get => title; }
    public CharButton MyCharButton
    {
        get
        {
           return charButton; 
        }
        set
        {
            MySlot = null;
            charButton = value;
        }
    }

    public int MyPrice { get => price; }
    public List<Item> MyRecipe { get => recipe; }
    

    public virtual string GetDescription()
    {
        
        return string.Format("<color={0}>{1}</color>", QualityColor.MyColors[MyQuality], MyTitle);
    }

        public void Remove()
    {
        if(MySlot != null)
        {
            MySlot.RemoveItem(this);
                        
        }
    }
}
