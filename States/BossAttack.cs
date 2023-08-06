using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAttack : IState
{
    

    private readonly AIEnemyController _npc;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private float _lastAttack;
    private float _attackCooldown;


    public BossAttack(AIEnemyController npc, NavMeshAgent navMeshAgent, Animator animator)
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
        int randomIndex = Random.Range(0, _npc.MyAttackAnimations.Length);
        string randomTrigger = _npc.MyAttackAnimations[randomIndex];
        _animator.SetTrigger(randomTrigger);
    }

    public void OnEnter()
    {
        _attackCooldown = _animator.GetCurrentAnimatorStateInfo(0).length;
    }

    public void OnExit()
    {
        
    }
}
