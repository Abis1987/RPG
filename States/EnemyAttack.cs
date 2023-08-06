using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

internal class EnemyAttack : IState
{
    private readonly AIEnemyController _npc;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private float _lastAttack;
    private float _attackCooldown;


    public EnemyAttack(AIEnemyController npc, NavMeshAgent navMeshAgent, Animator animator)
    {
        _npc = npc;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
    }

    public void Tick()
    {
        
        if (Time.time - _lastAttack < _attackCooldown) 
        {
           
            return; 
        }
        
        _lastAttack = Time.time;
        PerformRandomAttack();
        _npc._wasAttacked = false;
        _npc.GetComponent<Enemy>().DealDamage();

        

    }
    private void PerformRandomAttack()
    {
        if(_npc.MyAttackAnimations.Length > 1)
        {
            int randomIndex = Random.Range(0, _npc.MyAttackAnimations.Length);
            string randomTrigger = _npc.MyAttackAnimations[randomIndex];
            _animator.SetTrigger(randomTrigger);
        }
        else
        {
            _animator.SetTrigger(_npc.MyAttackAnimations[0]);
        }
        
    }
    public void OnEnter()
    {
        
        _attackCooldown = _animator.GetCurrentAnimatorStateInfo(0).length;
        
        
    }

    public void OnExit()
    {

    }
    
}
