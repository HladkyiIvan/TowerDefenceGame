using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public Waypoint Waypoint;
    public float Speed = 2f;

    // Update is called once per frame
    void Update()
    {
        if (Waypoint == null) return;
        transform.position = Vector3.MoveTowards(transform.position, Waypoint.transform.position, Time.deltaTime * Speed);

        if (Vector3.SqrMagnitude(transform.position - Waypoint.transform.position) < float.Epsilon)
        {
            Waypoint = Waypoint.GetNextWaypoint();
        }
    }
}
