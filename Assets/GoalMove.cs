using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMove : MonoBehaviour
{ public Transform waypointA; // First waypoint
    public Transform waypointB; // Second waypoint
    public float movementSpeed = 2.0f; // Speed at which the NPC moves
    private Transform currentTarget; // Current target waypoint

    void Start()
    {
        // Set the initial target waypoint to waypointA
        currentTarget = waypointA;
    }

    void Update()
    {
        // Move the NPC towards the current target waypoint
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, movementSpeed * Time.deltaTime);

        // Rotate the NPC to face the current target waypoint
        Vector3 direction = currentTarget.position - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 5.0f * Time.deltaTime);
        }

        // Check if the NPC has reached the current target waypoint
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            // Switch the target waypoint when the NPC reaches one of the waypoints
            if (currentTarget == waypointA)
            {
                currentTarget = waypointB;
            }
            else
            {
                currentTarget = waypointA;
            }
        }
    }
}