using UnityEngine;
using UnityEngine.AI;


public class EnemyPatrolState : State
{
    EnemyStateMachine stateMachine;

    Animator animator;
    NavMeshAgent navMeshAgent;
    PatrolPath patrolPath;
    Transform transform;
    PlayerDetector playerDetector;

    private int currWaypointIndex = 0;
    private float waypointRange = 0.1f;

    private float timeSinceReachWaypoint;
    private float pauseTime = 2f;

    private float animDampTime = 0.1f;


    public EnemyPatrolState(EnemyStateMachine stateMachine) 
    {
        this.stateMachine = stateMachine;

        animator = stateMachine.Animator;
        navMeshAgent = stateMachine.NavMeshAgent;
        patrolPath = stateMachine.PatrolPath;
        transform = stateMachine.transform;
        playerDetector = stateMachine.PlayerDetector;

        navMeshAgent.speed = stateMachine.PatrolSpeed;
    }

    public override void Enter()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        Patrol(deltaTime);

        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude, animDampTime, deltaTime);

        if (playerDetector.IsDetected)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
    }

    public override void Exit()
    {
       
    }

    private void Patrol(float deltaTime)
    {
        MoveToWaypoint();

        if (IsAtWayPoint())
        {
            // Pause at the waypoint
            navMeshAgent.velocity = Vector3.zero;

            timeSinceReachWaypoint += deltaTime;

            if (timeSinceReachWaypoint > pauseTime)
            {
                // Move to the next waypoint
                currWaypointIndex = patrolPath.GetNextIndex(currWaypointIndex);

                timeSinceReachWaypoint = 0;
            }            
        }
    }

    private Vector3 GetCurrWaypoint()
    {
        return patrolPath.GetWayPoint(currWaypointIndex);
    }

    private void MoveToWaypoint()
    {
        navMeshAgent.destination = GetCurrWaypoint();
    }

    private bool IsAtWayPoint()
    {
        float distance = Vector3.Distance(transform.position, GetCurrWaypoint());

        return distance < waypointRange;
    }
}
