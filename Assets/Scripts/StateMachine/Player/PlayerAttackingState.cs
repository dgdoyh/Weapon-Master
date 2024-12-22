using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private Attack attack;

    private float prevNormalizedTime;
    private bool isForceApplied = false;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine) 
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetDamage(attack.Damage);
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, 0.1f);

        // + play sfx
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        float normalizedTime = GetNormalizedTime(stateMachine.Animator);

        // Apply force & Combo attack
        if (normalizedTime >= prevNormalizedTime && normalizedTime < 1f)
        {
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }

            if (stateMachine.InputReader.isAttacking)
            {
                TryComboAttack(normalizedTime);              
            }
        }
        // State transition
        else
        {
            if (stateMachine.Targeter.CurrTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }           
        }

        prevNormalizedTime = normalizedTime;
    }

    public override void Exit()
    {

    }

    public void TryApplyForce()
    {
        if (isForceApplied) { return; }

        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.ForwardForce);
        isForceApplied = true;
    }

    private void TryComboAttack(float normalizedTime)
    {
        int comboIndex = attack.NextComboIndex;

        // The last combo index is -1.
        if (comboIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }
        
        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, comboIndex));
    }
}
