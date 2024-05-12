using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    private const float AnimatorDampTime = 0.05f;

    private const float CrossFadeDuration = 0.1f;  // for smooth animation transition

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        // previous animation -> FreeLookBlendTree
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
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
    }

    public override void Exit()
    {

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

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        // Smoothly rotate player (based on RotationDamping) toward moving direction
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * stateMachine.RotationSpeed);
    }
}
