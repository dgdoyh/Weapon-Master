using UnityEngine;


public class PlayerDetector : MonoBehaviour
{
    public bool IsDetected { get; private set; }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsDetected = false;
        }
    }
}
