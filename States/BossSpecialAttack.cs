using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSpecialAttack : IState
{
    private float currentCastTime = 0f;
    private float timeUntillCast = 3f;

    private readonly BossAIController _npc;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;

    

    public BossSpecialAttack(BossAIController npc, NavMeshAgent navMeshAgent, Animator animator)
    {
        _npc = npc;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
    }
    public void OnEnter()
    {

       
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        
        currentCastTime += Time.deltaTime;
        _animator.SetBool("casting", true);
        if(currentCastTime >= timeUntillCast)
        {
            _animator.SetBool("casting", false);
            _npc._canPerformSpecialAttack = false;
            _npc.GetComponent<Enemy>().PerformSpecialAttack();
            
            currentCastTime = 0;
        }
        

    }
}
