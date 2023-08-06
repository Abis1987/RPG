
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    
    [SerializeField] private float intervalDuration = 1f;
    


    private bool canDealDamage = false;

    private float elapsedTime = 0f;

    private ProjectileDamage projectileDamage;

    private Collider target;

   

    private void Start()
    {
        projectileDamage = GetComponent<ProjectileDamage>();
        
        
       
        
        
           
    }


    public void HitTarget(Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponentInParent<Enemy>().TakeDamage(projectileDamage.MyTotalSpellDamage);



        }
    }

    private void OnTriggerStay(Collider other)
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= intervalDuration)
        {
            Debug.Log("damage");
            HitTarget(other);
            elapsedTime = 0;
        }
        
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("damage");
    //    HitTarget(other);
    //}
}







