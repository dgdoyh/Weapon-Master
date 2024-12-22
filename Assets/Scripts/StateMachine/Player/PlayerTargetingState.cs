using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

    private const float AnimTransitionTime = 0.05f;

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        // + Event Subscription (OnDodge)
        #region Event Subscription

        stateMachine.InputReader.CancelEvent += ExitTargetingMode;

        #endregion

        stateMachine.Animator.SetTrigger("Targeting");
    }

    public override void Tick(float deltaTime)
    {
        #region Attack
        if (stateMachine.InputReader.isAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
        }
        #endregion       

        #region Move
        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);
        UpdateAnimator(deltaTime);
        #endregion

        #region Transition (Targeting -> FreeLook)
        if (stateMachine.Targeter.CurrTarget == null)
        {
            ReturnToLocomotion();
        }
        #endregion

        FaceTarget();

        // + Transit to Dodging State
    }

    public override void Exit()
    {
        // + Event unsubscription (OnDodge)
        #region Event Unsubscription

        stateMachine.InputReader.CancelEvent -= ExitTargetingMode;

        #endregion
    }

    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        float xSpeed = stateMachine.InputReader.MovementValue.x;
        float ySpeed = stateMachine.InputReader.MovementValue.y;

        stateMachine.Animator.SetFloat(TargetingForwardHash, ySpeed, AnimTransitionTime, deltaTime);
        stateMachine.Animator.SetFloat(TargetingRightHash, xSpeed, AnimTransitionTime, deltaTime);
    }

    private void SwitchStateToJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }

    private void ExitTargetingMode()
    {
        stateMachine.Targeter.RemoveCurrTarget();

        ReturnToLocomotion();
    }
}
