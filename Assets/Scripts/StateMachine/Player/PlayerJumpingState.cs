using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private Vector3 currVelocity;

    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        float jumpForce = stateMachine.JumpForce;

        stateMachine.ForceReceiver.Jump(jumpForce);

        currVelocity = stateMachine.CharacterController.velocity;
        currVelocity.y = 0;

        stateMachine.Animator.SetTrigger("Jump");
    }

    public override void Tick(float deltaTime)
    {
        Move(currVelocity, deltaTime);

        #region Transition (Jump -> Fall)

        if (stateMachine.CharacterController.velocity.y >= 0f)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }

        #endregion
    }

    public override void Exit()
    {

    }
}
