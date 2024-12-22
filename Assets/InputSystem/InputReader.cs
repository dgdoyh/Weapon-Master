using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputActions.IPlayerActions
{
    private InputActions inputActions;

    public event Action JumpEvent;
    public event Action TargetEvent;
    public event Action CancelEvent;

    private bool isTargeting = false;
    public bool isAttacking { get; private set; }
    public Vector2 MovementValue { get; private set; }

    void Start()
    {
        inputActions = new InputActions();

        inputActions.Player.SetCallbacks(this);
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; } // This line prevents to invoke JumpEvent several times by one click

        JumpEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        // While pressing the attack key, player keeps attacking
        if (context.performed)
        {
            isAttacking = true;
        }
        else if (context.canceled)
        {
            isAttacking = false;
        }
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; } // This line prevents to invoke JumpEvent several times by one click

        // Toggle targeting mode (on/off)
        if (!isTargeting)
        {
            TargetEvent?.Invoke();
            isTargeting = true;
        }
        else
        {
            CancelEvent?.Invoke();
            isTargeting= false;
        }
    }
}
