using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestinationManager : MonoBehaviour
{
    public GameObject waypointPrefab; // Prefab of the waypoint
    public float spawnHeight = 1f; // Height above the ground to spawn the waypoint

    private GameObject spawnedWaypoint; // Reference to the spawned waypoint
    private LineRenderer lineRenderer; // Reference to the LineRenderer component
    private GameObject player; // Reference to the AI/Player GameObject

    private void Start()
    {
        // Find the AI/Player GameObject by tag (assuming it has a unique tag)
        player = GameObject.FindGameObjectWithTag("Player");

        /* Create a new GameObject to hold the LineRenderer component
        GameObject lineRendererObject = new GameObject("LineRenderer");
        lineRenderer = lineRendererObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material.color = Color.magenta; // Set the color to purple
        */
    }

    private void Update()
    {
        // Check if the right mouse button is pressed
        if (Input.GetMouseButtonDown(1))
        {
            // Destroy the existing waypoint if it exists
            DestroyPreviousWaypoint();

            // Spawn a new waypoint
            SpawnWaypoint();
        }

        // Update the line renderer to connect the AI/Player to the waypoint
        UpdateLineRenderer();
    }

    private void SpawnWaypoint()
    {
        // Raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the raycast hits a valid surface (not UI or other objects)
            if (hit.collider != null)
            {
                // Calculate the spawn position with the desired height
                Vector3 spawnPosition = new Vector3(hit.point.x, spawnHeight, hit.point.z);

                // Spawn the waypoint prefab at the spawn position
                spawnedWaypoint = Instantiate(waypointPrefab, spawnPosition, Quaternion.identity);

                // Create a new line renderer
                CreateLineRenderer();
            }
        }
    }

    private void UpdateLineRenderer()
    {
        // Check if there is a waypoint and if there is a line renderer
        if (spawnedWaypoint != null && lineRenderer != null && player != null)
        {
            // Set line renderer positions to connect the AI/Player to the waypoint
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, player.transform.position);
            lineRenderer.SetPosition(1, spawnedWaypoint.transform.position);
        }
    }

    private void DestroyPreviousWaypoint()
    {
        // Check if there is an existing waypoint
        if (spawnedWaypoint != null)
        {
            // Destroy the waypoint
            Destroy(spawnedWaypoint);
        }

        // Check if there is an existing line renderer
        if (lineRenderer != null)
        {
            // Destroy the line renderer
            Destroy(lineRenderer.gameObject);
        }
    }

    private void CreateLineRenderer()
    {
        // Create a new GameObject to hold the LineRenderer component
        GameObject lineRendererObject = new GameObject("LineRenderer");
        lineRenderer = lineRendererObject.AddComponent<LineRenderer>();

        // Set line renderer properties
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material.color = Color.magenta; // Set the color to purple
    }

    // Method to destroy the line renderer
    public void DestroyLineRenderer()
    {
        if (lineRenderer != null)
        {
            // Destroy the line renderer
            Destroy(lineRenderer.gameObject);
        }
    }
}