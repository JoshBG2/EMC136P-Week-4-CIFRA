using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the AI movement
    public float rotationSpeed = 10f; // Speed of rotation
    public float stoppingDistance = 1f; // Distance at which the AI stops moving

    private Vector3 targetPosition; // Target position to move towards
    private bool shouldMove = true; // Flag to control movement

    // Update is called once per frame
    void Update()
    {
        // Check for right mouse button click to start or resume movement
        if (Input.GetMouseButtonDown(1))
        {
            shouldMove = true;
            // Set target position to the position clicked by the player
            targetPosition = GetMouseWorldPosition();
        }

        // Move towards the target position if movement is enabled
        if (shouldMove && Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            MoveTowardsTarget();
        }
    }

    // Move towards the target position using Greedy pathfinding algorithm
    void MoveTowardsTarget()
    {
        // Calculate the direction towards the target position
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Calculate the rotation towards the target position
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate towards the target direction
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move the AI towards the target position
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    // Get the position in the world where the mouse is clicked
    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    // OnTrigger event when colliding with a waypoint
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            shouldMove = false; // Stop moving
        }
    }

    // OnTriggerExit event when exiting collision with a waypoint
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            shouldMove = true; // Resume moving
        }
    }
}