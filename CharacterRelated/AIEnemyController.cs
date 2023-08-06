using System;
using UnityEngine;
using UnityEngine.AI;


public class AIEnemyController : MonoBehaviour
{

    public Vector3 _startPosition;
    public Transform target;
    protected StateMachine _stateMachine;
    protected float _distance;
    protected float _toDestination;
    protected Enemy _enemy;

    [SerializeField] string state;

    protected NavMeshAgent _navMeshAgent;
    protected Animator _animator;




    public bool _wasAttacked = false;



    [SerializeField] protected float lookRadius = 10f;
    [SerializeField] private string[] attackAnimations;

    public string[] MyAttackAnimations { get => attackAnimations; }

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        _startPosition = transform.position;
        _enemy = GetComponent<Enemy>();
    }

    protected virtual void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        var enemy = GetComponent<Enemy>();


        _stateMachine = new StateMachine();


        var moveToSelected = new MoveToTarget(this, _navMeshAgent, _animator);
        var randomRoam = new RandomRoam(this, _navMeshAgent, _animator);
        var returnToStartPosition = new ReturnToStartPosition(this, _navMeshAgent, _animator);
        var enemyAttack = new EnemyAttack(this, _navMeshAgent, _animator);

        At(randomRoam, moveToSelected, PlayerInRange());
        At(moveToSelected, returnToStartPosition, PlayerOutsideRange());
        At(returnToStartPosition, randomRoam, ArrivedToStartLocation());
        At(moveToSelected, enemyAttack, CanAttack());
        At(enemyAttack, moveToSelected, OutOfAttackRange());
        At(enemyAttack, moveToSelected, IsDead());
        At(randomRoam, moveToSelected , IsEnraged() );


        _stateMachine.SetState(randomRoam);

        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
       
        
        Func<bool> PlayerInRange() => () => _distance <= lookRadius;
        Func<bool> PlayerOutsideRange() => () => _distance > lookRadius && !_wasAttacked;
        Func<bool> ArrivedToStartLocation() => () => _toDestination < 4f;
        Func<bool> CanAttack() => () => _distance <= _navMeshAgent.stoppingDistance;
        Func<bool> OutOfAttackRange() => () => _distance > _navMeshAgent.stoppingDistance ;
        Func<bool> IsEnraged() => () => _wasAttacked;
        Func<bool> IsDead() => () => _enemy.IsDead;
        


    }


    protected virtual void Update()
    {
        _toDestination = Vector3.Distance(transform.position, _startPosition);
        if (!_enemy.IsDead)
        {
            _distance = Vector3.Distance(target.transform.position, transform.position);
        }
        
        if (_enemy.IsDead)
        {
            target = null;
            _distance = 5;
        }

        _stateMachine.Tick();

        
       
    }
   
    

}