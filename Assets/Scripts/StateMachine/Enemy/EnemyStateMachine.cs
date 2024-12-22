using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(CharacterController), typeof(Animator), typeof(ForceReceiver))]
[RequireComponent(typeof(NavMeshAgent))]


public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Transform Player { get; private set; }
    [field: SerializeField] public PatrolPath PatrolPath { get; private set; }
    [field: SerializeField] public Weapon Weapon { get; private set; }

    [SerializeField] public float AttackingRadius { get; private set; } = 2f;
    [SerializeField] public float PatrolSpeed { get; private set; } = 2f;
    [SerializeField] public float ChasingSpeed { get; private set; } = 3f;
    [SerializeField] public int AttackDamage { get; private set; } = 10;

    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public PlayerDetector PlayerDetector { get; private set; }


    private void Start()
    {
        Animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>(); 
        NavMeshAgent = GetComponent<NavMeshAgent>();

        PlayerDetector = GetComponentInChildren<PlayerDetector>();

        SwitchState(new EnemyPatrolState(this));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, AttackingRadius);
    }
}
