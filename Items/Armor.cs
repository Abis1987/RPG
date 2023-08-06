using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ArmorType {Head, Shoulders, Chest, Hands, Legs, Feet, MainHand, Offhand, Twohand, Skill, Light1, Light2, Light3, Light4, Light5, Dark1, Dark2, Dark3, Dark4, Dark5, Arcane1, Arcane2,
                 Arcane3, Arcane4, Arcane5}

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]
public class Armor : Item
{
    [SerializeField] private ArmorType armorType;
    [SerializeField] private GameObject weapon = null;


    [SerializeField] GameObject usableSpell = null;
    [SerializeField] private float _health;
    [SerializeField] private float _mana;
    [SerializeField] private float _armor;
    [SerializeField] private float _attack;
    [SerializeField] private int _lightPoints;
    [SerializeField] private int _darkPoints;
    [SerializeField] private int _arcanePoints;

    public float MyHealth { get => _health; }
    public float MyMana { get => _mana; }
    public float MyArmour { get => _armor; }
    public float MyAttack { get => _attack; }
    internal ArmorType MyArmorType { get => armorType; }
    public int MyLightPoints { get => _lightPoints; }
    public int MyDarkPoints { get => _darkPoints; }
    public int MyArcanePoints { get => _arcanePoints; }
    public GameObject MyUsableSpell { get => usableSpell; }
    public GameObject MyWeapon { get => weapon; }

    public override string GetDescription()
    {
        string stats = string.Empty;
        if (_health > 0)
        {
            stats += string.Format("\n +{0} Health", _health);
        }
        if (_mana > 0)
        {
            stats += string.Format("\n +{0} Mana", _mana);
        }
        if (_armor > 0)
        {
            stats += string.Format("\n +{0} Armor", _armor);
        }
        if (_attack > 0)
        {
            stats += string.Format("\n +{0} Attack", _attack);
        }
        
        if (_lightPoints > 0)
        {
            stats += string.Format("\n +{0} Light", _lightPoints);
        }
        if (_darkPoints > 0)
        {
            stats += string.Format("\n +{0} Dark", _darkPoints);
        }
        if (_arcanePoints > 0)
        {
            stats += string.Format("\n +{0} Arcane", _arcanePoints);
        }

        return base.GetDescription() + stats;
    }

    public void Equip()
    {
        InventoryScript.MyInstance.EquipArmor(this);
        UpdateSkillTree();
        if (weapon != null)
        {
            Player.MyInstance.GetComponent<EquipmentSystem>().EquipWeapon(weapon);
        }
    }

    public void UpdateSkillTree()
    {
        if(_lightPoints > 0)
        {
            InventoryScript.MyInstance.ActivateLightSkillTreeSlots();
        }
        else if(_darkPoints > 0)
        {
            InventoryScript.MyInstance.ActivateDarkSkillTreeSlots();
        }
        else if(_arcanePoints > 0)
        {
            InventoryScript.MyInstance.ActivateArcaneSkillTreeSlots();
        }
    }
}
