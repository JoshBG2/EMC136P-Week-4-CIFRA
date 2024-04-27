using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestinationManager destinationManager = GameObject.FindObjectOfType<DestinationManager>();
            if (destinationManager != null)
            {
                destinationManager.DestroyLineRenderer(); // Call the method to destroy the line renderer
            }

            Destroy(gameObject); // Destroy the waypoint when player makes contact
        }
    }
}
