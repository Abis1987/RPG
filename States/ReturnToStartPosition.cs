using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

internal class ReturnToStartPosition : IState
{
    private readonly AIEnemyController _npc;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");


    public ReturnToStartPosition(AIEnemyController npc, NavMeshAgent navMeshAgent, Animator animator)
    {
        _npc = npc;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
    }

    public void Tick()
    {

    }

    public void OnEnter()
    {
        


        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_npc._startPosition);
        
    }

    public void OnExit()
    {

    }
}
