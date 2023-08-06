using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    private Transform target;
    private NavMeshAgent agent;
    private AIEnemyController controller;
    private Animator animator;
    private float damageTreshhold = 0f;

    private bool isDead = false;

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float speed;
    [SerializeField] GameObject lootBag;
    [SerializeField] private Loot[] loot;
    [SerializeField] private float damage;
    [SerializeField] private GameObject[] specialSkills;

    [SerializeField] private string type;

    private float _animBlend;
    [SerializeField] private float _animSpeedChangeRate = 10.0f;

    public string MyType { get => type; }
    public Transform MyTarget { get => target; set => target = value; }
    public bool IsDead { get => isDead; }
    public float MyDamageTreshhold { get => damageTreshhold; set => damageTreshhold = value; }

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        controller = GetComponent<AIEnemyController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animBlend = Mathf.Lerp(_animBlend, agent.velocity.magnitude, Time.deltaTime * _animSpeedChangeRate);
        if (_animBlend < 0.01f) _animBlend = 0f;

        animator.SetFloat("Speed", _animBlend);
    }

    public void DealDamage()
    {
        Player.MyInstance.TakeDamage(damage);
        
    }

    public virtual void TakeDamage(float damage)
    {
        
        
        controller._wasAttacked = true;
        currentHealth -= damage;
        damageTreshhold += damage;

        if (currentHealth < 0) currentHealth = 0;

        if (!isDead)
        {
            agent.speed = 4;
            agent.SetDestination(target.position);
        }
        
        

        if (currentHealth <= 0)
        {
            
            Die();
            isDead = true;
        }
        if (!isDead)
        {
            animator.SetTrigger("Hit");
        }

    }

    private void Die()
    {
        if (!isDead)
        {
            var dropedLootBag = Instantiate(lootBag, transform.position, Quaternion.identity).GetComponent<LootTable>();
            dropedLootBag.MyLoot = loot;

            PlayerManager.instance.OnKillConfirmed(this);

            animator.SetTrigger("Dead");
            agent.SetDestination(transform.position);
            controller.target = null;
            Destroy(GetComponent<BoxCollider>());
            Destroy(gameObject, 5f);
        }
        

    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void PerformSpecialAttack()
    {
        
        if(specialSkills.Length == 1)
        {
            var specialSkill = Instantiate(specialSkills[0], transform.position, Quaternion.identity);
            specialSkill.GetComponent<ProjectileDamage>().SetSpellDamage(0);
        }
        
    }
    
}
