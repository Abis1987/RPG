using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Craft Material", menuName = "Items/Material", order = 1)]
public class CraftMaterial : Item
{
    
    [SerializeField] private string description;



   

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n{0}", description);


    }
}
