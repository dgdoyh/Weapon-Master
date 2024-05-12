using System.Collections;
using System.Collections.Generic;
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
    }

    public override void Tick(float deltaTime)
    {
        Move(currVelocity, deltaTime);

        // ...
    }

    public override void Exit()
    {

    }
}
