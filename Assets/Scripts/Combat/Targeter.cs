using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup targetGroup;

    private List<Health> targets = new List<Health>();
    private Camera mainCamera;

    public Health CurrTarget { get; private set; }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    // Add new object into the target list
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Health>(out Health target)) { return; }

        if (target.CompareTag("Player")) { return; }

        target.OnDie += RemoveCurrTarget;

        targets.Add(target);

        Debug.Log(target.name + " detected");
    }

    // Remove a target from the target list
    private void OnTriggerExit(Collider other)
    {
        //if (!other.TryGetComponent<Health>(out Health target)) { return; }

        RemoveCurrTarget();
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }

        Health closestTarget = null;

        float closestTargetDistance = Mathf.Infinity; 

        foreach (Health target in targets)
        {
            Vector2 targetViewPos = mainCamera.WorldToViewportPoint(target.transform.position);

            // Check if the target is in the main cam view
            if (targetViewPos.x < 0 || targetViewPos.x > 1 || targetViewPos.y < 0 || targetViewPos.y > 1)
            {
                continue;
            }

            // ** need to be polished
            Vector3 distance = target.transform.position - this.transform.position;

            // Set a target which is the closest to the center of main camera view as the closest target
            if (distance.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = distance.sqrMagnitude;
            }
        }

        if (closestTarget == null) { return false; }

        CurrTarget = closestTarget;

        targetGroup.AddMember(CurrTarget.transform, 1f, 2f);

        Debug.Log("Current Target: " + CurrTarget.name);

        return true;
    }

    public void RemoveCurrTarget()
    {
        if (CurrTarget == null) { return; }

        targetGroup.RemoveMember(CurrTarget.transform);
        CurrTarget = null;
    }

    //public void RemoveTarget(Health target)
    //{
    //    if (CurrTarget == target)
    //    {
    //        targetGroup.RemoveMember(CurrTarget.transform);

    //        CurrTarget = null;
    //    }

    //    target.OnDie -= RemoveTarget;

    //    targets.Remove(target);
    //}
}
