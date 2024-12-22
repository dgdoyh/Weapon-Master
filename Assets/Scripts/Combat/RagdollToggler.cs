using UnityEngine;

public class RagdollToggler : MonoBehaviour
{
    private Animator animator;

    private Collider[] colliders;
    private Rigidbody[] rigidbodies;

    private void Start()
    {
        animator = GetComponent<Animator>();

        colliders = GetComponentsInChildren<Collider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public void ToggleRagdoll(bool isActive)
    {
        // Active body parts' colliders when ragdoll is active
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Ragdoll"))
            {
                collider.enabled = isActive;
            }
        }

        // Active gravity & disactive other physical interaction on body parts when ragdoll is active
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            if (rigidbody.gameObject.CompareTag("Ragdoll"))
            {
                rigidbody.isKinematic = !isActive;
                rigidbody.useGravity = isActive;
            }
        }

        animator.enabled = !isActive;

        // + turn on/off controller
    }
}
