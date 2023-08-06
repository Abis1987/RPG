using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

internal class MoveToTarget : IState
{
    private readonly AIEnemyController _npc;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    
    public MoveToTarget(AIEnemyController npc, NavMeshAgent navMeshAgent, Animator animator)
    {
        _npc = npc;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
    }
    
    public void Tick()
    {
       if(_npc.target != null)
        {
            Vector3 direction = (_npc.target.position - _npc.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            _npc.transform.rotation = Quaternion.Slerp(_npc.transform.rotation, lookRotation, Time.deltaTime * 5f);

            _navMeshAgent.SetDestination(_npc.target.transform.position);

            
        }
       
        
        
    }

    public void OnEnter()
    {


        _navMeshAgent.speed = 4f;
        _navMeshAgent.enabled = true;
        
    }

    public void OnExit()
    {
        
    }
}