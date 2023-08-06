using DigitalRuby.PyroParticles;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject castPoint;
    [SerializeField] private GameObject spell1;
    public Stat currentHealth;
    public Stat mana;
    public Stat stamina;


    [SerializeField] private float _attack = 50f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaRegen = 1f;
    [SerializeField] private float _armor = 3000;

    [SerializeField] private int _lightPoints;
    [SerializeField] private int _darkPoints;
    [SerializeField] private int _arcanePoints;

    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _player;

    
    public bool isDead = false;

    private bool isCasting = false;
    private bool canCast = false;
    private float damageToAply = 20f;
    private IInteractable interactable;
    private float damageReduction;
    private float castingDistance;
    
    

    private Animator animator;
    private StarterAssetsInputs _input;

    public int MyGold { get; set; }

    

    private static Player instance;


    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    public float MyAttack { get => _attack; }
    public int MyLightPoints { get => _lightPoints; }
    public int MyDarkPoints { get => _darkPoints; }
    public int MyArcanePoints { get => _arcanePoints; }

   
    private void Start()
    {
        
        MyGold = 100;
        animator = GetComponent<Animator>();
        _input = GetComponent<StarterAssetsInputs>();
        currentHealth.Initialize(maxHealth, maxHealth);
        mana.Initialize(maxMana, maxMana);
        stamina.Initialize(maxStamina, maxStamina);
        CalculateDamageReduction();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }


    private void Update()
    {
        //DrawWeapon();

        RegenStamina();

        if (_input.spell1 || _input.spell2 || _input.spell3 || _input.spell4 || _input.spell5)
        {
            StartCoroutine(Cast());

        }

    }
    //private void DrawWeapon()
    //{
    //    if (!_input.drawWeapon)
    //    {
    //        animator.SetBool("drawWeapon", false);
    //        animator.SetBool("sheathWeapon", false);
    //    }
    //    if (_input.drawWeapon && !isWeaponDrawed)
    //    {
    //        isWeaponDrawed = true;
    //        animator.SetBool("drawWeapon", true);

    //        _input.drawWeapon = false;
    //    }
    //    else if (_input.drawWeapon && isWeaponDrawed)
    //    {
    //        isWeaponDrawed = false;
    //        animator.SetBool("sheathWeapon", true);

    //        _input.drawWeapon = false;
    //    }

    //}

    public void RegenStamina()
    {
        if (stamina.MyCurrentValue < maxStamina)
        {
            stamina.MyCurrentValue += staminaRegen * Time.deltaTime;
        }
    }

    public bool CanCast()
    {

        if (_input.spell1 && InventoryScript.MyInstance.Skill1.MyEquipedArmor.MyUsableSpell.GetComponent<ProjectileDamage>().MyManaCost <= mana.MyCurrentValue)
        {

            return true;


        }
        else if (_input.spell2 && InventoryScript.MyInstance.Skill2.MyEquipedArmor.MyUsableSpell.GetComponent<ProjectileDamage>().MyManaCost <= mana.MyCurrentValue)
        {
            return true;

        }
        else if (_input.spell3 && InventoryScript.MyInstance.Skill3.MyEquipedArmor.MyUsableSpell.GetComponent<ProjectileDamage>().MyManaCost <= mana.MyCurrentValue)
        {

            return true;

        }
        else if (_input.spell4 && InventoryScript.MyInstance.Skill4.MyEquipedArmor.MyUsableSpell.GetComponent<ProjectileDamage>().MyManaCost <= mana.MyCurrentValue)
        {

            return true;

        }
        else if (_input.spell5 && InventoryScript.MyInstance.Skill5.MyEquipedArmor.MyUsableSpell.GetComponent<ProjectileDamage>().MyManaCost <= mana.MyCurrentValue)
        {

            return true;

        }

        return false;
    }

    private IEnumerator Cast()
    {

        if (CanCast())
        {
            if (!isCasting)
            {
                isCasting = true;
                animator.SetBool("cast", true);

                yield return new WaitForSeconds(1);
                if (isCasting )
                {
                    isCasting = false;
                    CastSpell();

                }

                _input.spell1 = false;
                _input.spell2 = false;
                _input.spell3 = false;
                _input.spell4 = false;
                _input.spell5 = false;
                animator.SetBool("cast", false);

            }
    }






}

    public void CastSpell()
    {
        if( _input.spell1 && InventoryScript.MyInstance.Skill1.MyEquipedArmor.MyUsableSpell.GetComponent<ProjectileDamage>().MyManaCost <= mana.MyCurrentValue)
        {

             var spell = Instantiate(InventoryScript.MyInstance.Skill1.MyEquipedArmor.MyUsableSpell, castPoint.transform.position + transform.forward * InventoryScript.MyInstance.Skill1.MyEquipedArmor.MyUsableSpell.GetComponent<ProjectileDamage>().MyInvokeDistance, castPoint.transform.rotation);
             spell.GetComponent<ProjectileDamage>().SetSpellDamage(damageToAply);
             mana.MyCurrentValue -= spell.GetComponent<ProjectileDamage>().MyManaCost;

            
        }
        else if(_input.spell2 && InventoryScript.MyInstance.Skill2.MyEquipedArmor.MyUsableSpell.GetComponent<ProjectileDamage>().MyManaCost <= mana.MyCurrentValue)
        {
            var spell = Instantiate(InventoryScript.MyInstance.Skill2.MyEquipedArmor.MyUsableSpell, castPoint.transform.position + transform.forward * InventoryScript.MyInstance.Skill2.MyEquipedArmor.MyUsableSpell.GetComponent<ProjectileDamage>().MyInvokeDistance, castPoint.transform.rotation);
            spell.GetComponent<ProjectileDamage>().SetSpellDamage(damageToAply);
            mana.MyCurrentValue -= spell.GetComponent<ProjectileDamage>().MyManaCost;
            
        }
        else if (_input.spell3 && InventoryScript.MyInstance.Skill3.MyEquipedArmor.MyUsableSpell.GetComponent<ProjectileDamage>().MyManaCost <= mana.MyCurrentValue)
        {
            
            var spell = Instantiate(InventoryScript.MyInstance.Skill3.MyEquipedArmor.MyUsableSpell, castPoint.transform.position + transform.forward * InventoryScript.MyInstance.Skill3.MyEquipedArmor.MyUsableSpell.GetComponent<ProjectileDamage>().MyInvokeDistance, castPoint.transform.rotation);
            spell.GetComponent<ProjectileDamage>().SetSpellDamage(damageToAply);
            mana.MyCurrentValue -= spell.GetComponent<ProjectileDamage>().MyManaCost;
            
        }
        else if (_input.spell4 && InventoryScript.MyInstance.Skill4.MyEquipedArmor.MyUsableSpell.GetComponent<ProjectileDamage>().MyManaCost <= mana.MyCurrentValue)
        {
            
            var spell = Instantiate(InventoryScript.MyInstance.Skill4.MyEquipedArmor.MyUsableSpell, castPoint.transform.position + transform.forward * InventoryScript.MyInstance.Skill4.MyEquipedArmor.MyUsableSpell.GetComponent<ProjectileDamage>().MyInvokeDistance, castPoint.transform.rotation);
            spell.GetComponent<ProjectileDamage>().SetSpellDamage(damageToAply);
            mana.MyCurrentValue -= spell.GetComponent<ProjectileDamage>().MyManaCost;
            
        }
        else if (_input.spell5 && InventoryScript.MyInstance.Skill5.MyEquipedArmor.MyUsableSpell.GetComponent<ProjectileDamage>().MyManaCost <= mana.MyCurrentValue)
        {
            
            var spell = Instantiate(InventoryScript.MyInstance.Skill5.MyEquipedArmor.MyUsableSpell, castPoint.transform.position + transform.forward * InventoryScript.MyInstance.Skill5.MyEquipedArmor.MyUsableSpell.GetComponent<ProjectileDamage>().MyInvokeDistance, castPoint.transform.rotation);
            spell.GetComponent<ProjectileDamage>().SetSpellDamage(damageToAply);
            mana.MyCurrentValue -= spell.GetComponent<ProjectileDamage>().MyManaCost;
            
        }

        
    }

    



   public void TakeDamage(float damage)
    {
        float damageTaken =(float)(damage * (1 - damageReduction));
        currentHealth.MyCurrentValue -= damageTaken;

        if(currentHealth.MyCurrentValue <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.SetBool("isDead", true);
        Destroy(_player, 1f);
        
    }

   private void CalculateDamageReduction()
    {
        damageReduction = 1 - (350 / (350 + _armor));
    }

    public void AddArmorStats(Armor armor)
    {
        if (armor.MyHealth > 0)
        {
            maxHealth += armor.MyHealth;
        }
        if (armor.MyMana > 0)
        {
            maxMana += armor.MyMana;
        }
        if (armor.MyArmour > 0)
        {
            _armor += armor.MyArmour;
        }
        if (armor.MyAttack > 0)
        {
            _attack += armor.MyAttack;
        }
        if(armor.MyLightPoints > 0)
        {
            _lightPoints += armor.MyLightPoints;
        }
        if (armor.MyDarkPoints > 0)
        {
            _darkPoints += armor.MyDarkPoints;
        }
        if (armor.MyArcanePoints > 0)
        {
            _arcanePoints += armor.MyArcanePoints;
        }

    }

    public void RemoveArmorStats(Armor armor)
    {
        if(armor.MyHealth > 0)
        {
            maxHealth -= armor.MyHealth;
        }
        if(armor.MyMana > 0)
        {
            maxMana -= armor.MyMana;
        }
        if (armor.MyArmour > 0 )
        {
            _armor -= armor.MyArmour;
        }
        if (armor.MyAttack > 0)
        {
            _attack -= armor.MyAttack;
        }
        if (armor.MyLightPoints > 0)
        {
            _lightPoints -= armor.MyLightPoints;
        }
        if (armor.MyDarkPoints > 0)
        {
            _darkPoints -= armor.MyDarkPoints;
        }
        if (armor.MyArcanePoints > 0)
        {
            _arcanePoints -= armor.MyArcanePoints;
        }

    }

}
