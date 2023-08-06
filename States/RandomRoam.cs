using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

internal class RandomRoam : IState
{
    private readonly AIEnemyController _npc;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private float _walkRadius = 3f;
    private float _lastRoam;
    private float _roamCooldown = 7f;

   

    

    public RandomRoam(AIEnemyController npc, NavMeshAgent navMeshAgent, Animator animator)
    {
        _npc = npc;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
    }

    public void Tick()
    {
        if (Time.time - _lastRoam < _roamCooldown) { return; };
        
            _lastRoam = Time.time;
            _navMeshAgent.speed = 2;
            Vector3 randomPosition = Random.insideUnitSphere * _walkRadius;
            randomPosition += _npc.transform.position;

            _navMeshAgent.SetDestination(randomPosition);
        

    }

    public void OnEnter()
    {
         
        
    }

    public void OnExit()
    {
        
    }
}
