using StarterAssets;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private float attackSpeed = 1f; 
    private float attackTimer = 0f;

    private Animator animator;
    private StarterAssetsInputs _input;

    private void Start()
    {
        animator = GetComponent<Animator>();
        _input = GetComponent<StarterAssetsInputs>();
        animator.SetFloat("CombatSpeed", (attackSpeed * 2));
    }

    public void Update()
    {

        attackTimer += Time.deltaTime;

        if (_input.attack )
        {
            if (attackTimer >= 1f / attackSpeed)
            {

                Attack();

                attackTimer = 0f;
            }
        }
       
    }

    private void Attack()
    {
       
      
            animator.SetTrigger("attack");
            Debug.Log("attack");
           
  
            
            if (InteractWithInteractable.MyInstance.MyCurrentEnemy != null)
            {
                InteractWithInteractable.MyInstance.MyCurrentEnemy.TakeDamage(Player.MyInstance.MyAttack);
                Debug.Log("damage");
            }

        
    }
}