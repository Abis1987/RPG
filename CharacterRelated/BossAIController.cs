using System;
using UnityEngine;
using UnityEngine.AI;

public class BossAIController : AIEnemyController
{
    public bool _canPerformSpecialAttack = false;
    [SerializeField] private float damageTreshholdForSpecialAttack = 600f;
    protected override void Awake()
    {

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        var enemy = GetComponent<Enemy>();
       
        

        _stateMachine = new StateMachine();


        var moveToSelected = new MoveToTarget(this, _navMeshAgent, _animator);
        var randomRoam = new RandomRoam(this, _navMeshAgent, _animator);
        var returnToStartPosition = new ReturnToStartPosition(this, _navMeshAgent, _animator);
        var enemyAttack = new BossAttack(this, _navMeshAgent, _animator);
        var enemySpecialAttack = new BossSpecialAttack(this, _navMeshAgent, _animator);

        At(randomRoam, moveToSelected, PlayerInRange());
        At(moveToSelected, returnToStartPosition, PlayerOutsideRange());
        At(returnToStartPosition, randomRoam, ArrivedToStartLocation());
        At(moveToSelected, enemyAttack, CanAttack());
        At(enemyAttack, moveToSelected, OutOfAttackRange());
        At(enemyAttack, moveToSelected, IsDead());
        At(randomRoam, moveToSelected, IsEnraged());
        At(randomRoam, enemySpecialAttack, CanSpecialAttack());
        At(moveToSelected, enemySpecialAttack, CanSpecialAttack());
        At(returnToStartPosition, enemySpecialAttack, CanSpecialAttack());
        At(enemyAttack, enemySpecialAttack, CanSpecialAttack());
        At(enemySpecialAttack, moveToSelected, OutOfAttackRange());
        At(enemySpecialAttack, enemyAttack, CanAttack());


        _stateMachine.SetState(randomRoam);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);


        Func<bool> PlayerInRange() => () => _distance <= lookRadius;
        Func<bool> PlayerOutsideRange() => () => _distance > lookRadius && !_wasAttacked;
        Func<bool> ArrivedToStartLocation() => () => _toDestination < 4f;
        Func<bool> CanAttack() => () => _distance <= _navMeshAgent.stoppingDistance && _canPerformSpecialAttack == false ;
        Func<bool> OutOfAttackRange() => () => _distance > _navMeshAgent.stoppingDistance && _canPerformSpecialAttack == false;
        Func<bool> IsEnraged() => () => _wasAttacked;
        Func<bool> IsDead() => () => _enemy.IsDead;
        Func<bool> CanSpecialAttack() => () => _canPerformSpecialAttack;
    }

    protected override void Update()
    {
        base.Update();

        if(_enemy.MyDamageTreshhold >= damageTreshholdForSpecialAttack)
        {
            _canPerformSpecialAttack = true;
            _enemy.MyDamageTreshhold = 0f;
        }
    }
    

}
