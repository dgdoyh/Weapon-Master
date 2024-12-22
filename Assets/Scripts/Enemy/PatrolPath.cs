using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatrolPath : MonoBehaviour
{
    [SerializeField] public Transform[] wayPoints;

    private float sphereRadius;


    public Vector3 GetWayPoint(int index)
    {
        return wayPoints[index].position;
    }

    public int GetNextIndex(int currIndex)
    {
        int nextIndex = currIndex + 1;

        if (nextIndex >= wayPoints.Length)
        {
            nextIndex = 0;
        }

        return nextIndex;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < wayPoints.Length; i++)
        {
            Gizmos.DrawSphere(wayPoints[i].position, sphereRadius);
            Gizmos.DrawLine(wayPoints[i].position, wayPoints[GetNextIndex(i)].position);
        }
    }
}
