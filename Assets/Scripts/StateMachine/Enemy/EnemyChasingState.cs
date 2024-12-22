using UnityEngine;
using UnityEngine.AI;


public class EnemyChasingState : State
{
    private EnemyStateMachine stateMachine;

    private NavMeshAgent navMeshAgent;
    private Transform player;
    private PlayerDetector playerDetector;
    private Animator animator;

    private float attackingRadius;


    public EnemyChasingState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        navMeshAgent = stateMachine.NavMeshAgent;
        player = stateMachine.Player;
        playerDetector = stateMachine.PlayerDetector;
        attackingRadius = stateMachine.AttackingRadius;
        animator = stateMachine.Animator;
    }

    public override void Enter()
    {
        player.GetComponent<Health>().OnDie += SwitchStateToPatrol;

        animator.SetTrigger("Chase");
    }

    public override void Tick(float deltaTime)
    {
        if (IsInAttackRange())
        {
            // Chasing -> Attacking
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            return;
        }

        else if (!playerDetector.IsDetected)
        {
            // Chasing -> Patrol
            SwitchStateToPatrol();
            return;
        }

        // Chase
        navMeshAgent.destination = player.position;

        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }

    public override void Exit()
    {
        player.GetComponent<Health>().OnDie -= SwitchStateToPatrol;

        navMeshAgent.ResetPath();
        navMeshAgent.velocity = Vector3.zero;

        animator.SetFloat("Speed", 0f);
    }

    public bool IsInAttackRange()
    {
        float distance = (player.position - navMeshAgent.transform.position).sqrMagnitude;

        return distance <= attackingRadius * attackingRadius;
    }

    private void SwitchStateToPatrol()
    {
        stateMachine.SwitchState(new EnemyPatrolState(stateMachine));
    }
}
