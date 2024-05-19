using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private const float AnimatorDampTime = 0.05f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        #region Event Subscription
        stateMachine.InputReader.JumpEvent += SwitchStateToJump;
        #endregion

        stateMachine.Animator.SetTrigger("FreeLook");
    }

    public override void Tick(float deltaTime)
    {
        #region Move
        Vector3 movement = CalculateMovement();
        float speed = stateMachine.FreeLookMovementSpeed;
        bool isMoving = stateMachine.InputReader.MovementValue != Vector2.zero;

        Move(movement * speed, deltaTime);

        if (isMoving)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, speed, AnimatorDampTime, deltaTime);
            FaceMovementDirection(movement, deltaTime);
        }
        else
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
        }
        #endregion
    }

    public override void Exit()
    {
        #region Event Unsubscription
        stateMachine.InputReader.JumpEvent -= SwitchStateToJump;
        #endregion
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        // Smoothly rotate player (based on RotationDamping) toward moving direction
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationSpeed);
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        float movementX = stateMachine.InputReader.MovementValue.x;
        float movementY = stateMachine.InputReader.MovementValue.y;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return (right * movementX) + (forward * movementY);
    }

    private void SwitchStateToJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }
}
