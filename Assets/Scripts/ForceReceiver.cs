using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;  // optional
    [SerializeField] private float drag = 0.3f;

    private CharacterController controller;

    private Vector3 currentVelocity;
    private Vector3 dampingVelocity;
    private float verticalVelocity;
    private float minimumVelocity = 0.2f;

    public Vector3 Movement => currentVelocity + Vector3.up * verticalVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Adjust vertical velocity when it jumps
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        // Smoothly stop movement
        currentVelocity = Vector3.SmoothDamp(currentVelocity, Vector3.zero, ref dampingVelocity, drag);

        // If current velocity is small enough, set is as zero.
        // This prevents it slightly moving while it should be stopped.
        // Used sqr because it's calculated faster.
        if (currentVelocity.sqrMagnitude < Mathf.Sqrt(minimumVelocity))
        {
            currentVelocity = Vector3.zero;

            // optional
            if (agent != null)
            {
                // When it is stopped completely, turn on NavMeshAgent
                agent.enabled = true;
            }           
        }
    }

    public void AddForce(Vector3 force)
    {
        currentVelocity += force;

        if (agent != null)
        {
            agent.enabled = false;
        }
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }
}
