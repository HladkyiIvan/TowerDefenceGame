using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> NextWaypoints;

    public Waypoint GetNextWaypoint()
    {
        System.Random random = new System.Random();
        int index = random.Next(NextWaypoints.Count);
        return NextWaypoints[index];
    }

    void OnDrawGizmos()
    {
        if (NextWaypoints == null) return;
        Gizmos.color = Color.red;
        for (int i = 0; i < NextWaypoints.Count; i++)
        {
            Gizmos.DrawLine(transform.position, NextWaypoints[i].transform.position);
        }
    }
}
