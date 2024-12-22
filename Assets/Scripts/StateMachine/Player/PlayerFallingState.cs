using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private Vector3 currVelocity;

    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        currVelocity = stateMachine.CharacterController.velocity;
        currVelocity.y = 0;

        stateMachine.Animator.SetTrigger("Fall");
    }

    public override void Tick(float deltaTime)
    {
        Move(currVelocity, deltaTime);

        if (stateMachine.CharacterController.isGrounded)
        {
            ReturnToLocomotion();
        }

        FaceTarget();
    }

    public override void Exit()
    {

    }
}
