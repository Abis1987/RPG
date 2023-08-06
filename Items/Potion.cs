using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Items/Potion", order = 1)]
public class Potion : Item, IUsable
{
    [SerializeField] protected GameObject potionPrefab;
    [SerializeField] private string description;

    [SerializeField] float health = 0f;
    [SerializeField] float mana = 0f;
    

    public void Use()
    {
        if(health > 0)
        {
            Player.MyInstance.currentHealth.MyCurrentValue += health;
        }
        if(mana > 0)
        {
            Player.MyInstance.mana.MyCurrentValue += mana;
        }
        
        Remove();
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n{0}", description );


    }

}
