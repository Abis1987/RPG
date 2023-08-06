using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{

    [SerializeField] private float invokeDistance;
    private float totalSpellDamage = 0f;
    [SerializeField] private float spellDamage = 0f;
    [SerializeField] private float manaCost = 0f;

    public float MyInvokeDistance { get => invokeDistance; }
    public float MyTotalSpellDamage { get => totalSpellDamage; }
    public float MyManaCost { get => manaCost; }

    public void HitTarget(Collision other)
    {
        
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponentInParent<Enemy>().TakeDamage(totalSpellDamage);
            


        }
    }

    public void HitPlayer(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().TakeDamage(spellDamage);
        }
    }

   
    

    public void SetSpellDamage(float damage)
    {
        totalSpellDamage = spellDamage + damage;
    }
}
