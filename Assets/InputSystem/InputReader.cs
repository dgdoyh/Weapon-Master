using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputActions.IPlayerActions
{
    private InputActions inputActions;

    private event Action JumpEvent;

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
        JumpEvent?.Invoke();
    }
}
