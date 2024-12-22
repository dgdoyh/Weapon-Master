using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackingState : State
{
    EnemyStateMachine stateMachine;
    Animator animator;
    NavMeshAgent navMeshAgent;


    public EnemyAttackingState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        animator = stateMachine.Animator;
        navMeshAgent = stateMachine.NavMeshAgent;
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetDamage(stateMachine.AttackDamage);
        animator.SetTrigger("Attack");
    }

    public override void Tick(float deltaTime)
    {
        // Attacking -> Chasing (when the animation is done)
        if (GetNormalizedTime(animator) >= 1f)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
}
