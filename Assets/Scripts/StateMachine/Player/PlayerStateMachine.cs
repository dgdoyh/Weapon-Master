using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(CharacterController), typeof(Animator))]
[RequireComponent(typeof(ForceReceiver))]

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; } = 7f;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 10f;
    [field: SerializeField] public float JumpForce { get; private set; } = 4f;

    public Transform MainCameraTransform { get; private set; }
    public InputReader InputReader { get; private set; }
    public CharacterController CharacterController { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }
    public Animator Animator { get; private set; }


    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
        InputReader = GetComponent<InputReader>();
        CharacterController = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();
        Animator = GetComponent<Animator>();

        SwitchState(new PlayerFreeLookState(this));
    }
}
