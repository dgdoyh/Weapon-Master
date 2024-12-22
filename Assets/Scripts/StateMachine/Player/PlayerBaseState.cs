using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 movement, float deltaTime)
    {
        stateMachine.CharacterController.Move((movement + stateMachine.ForceReceiver.Movement) * deltaTime);
    }   

    protected void ReturnToLocomotion()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    protected void FaceTarget()
    {
        if (stateMachine.Targeter.CurrTarget == null) { return; }

        Vector3 lookPos = stateMachine.Targeter.CurrTarget.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
