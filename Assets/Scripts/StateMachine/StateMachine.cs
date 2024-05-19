using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private PlayerBaseState currentState;

    public void SwitchState(PlayerBaseState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
}
