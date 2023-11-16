using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public GridManager gridManager;
    private Vector3 targetPosition;
    public float groundLevel;
    private void Start()
    {
        // Initialize the target position to the initial position
        targetPosition = transform.position;
    }

    private void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (moveDirection != Vector3.zero)
        {
            // Calculate the next target position
            Vector3 nextTargetPosition = transform.position + moveDirection * gridManager.cellSize;

            // Check if the path is obstructed before updating the target position
            if (!IsPathObstructed(transform.position, nextTargetPosition))
            {
                targetPosition = nextTargetPosition;
            }
        }

        // Move towards the target position
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Round only the x and z components of the player's position
        int gridX = Mathf.RoundToInt(newPosition.x / gridManager.cellSize);
        int gridY = Mathf.RoundToInt(newPosition.z / gridManager.cellSize);

        // Update the target position to the center of the nearest grid cell
        targetPosition = new Vector3(gridX * gridManager.cellSize, transform.position.y, gridY * gridManager.cellSize);

        // Update the player's position
        transform.position = newPosition;
    }

    // Check if the path between two positions is obstructed
    bool IsPathObstructed(Vector3 start, Vector3 end)
    {
        // Perform raycast or other checks based on your specific game logic
        // For example, you can use Physics.Raycast to check for obstacles in the path
        RaycastHit hit;
        if (Physics.Raycast(start, end - start, out hit, Vector3.Distance(start, end)))
        {
            // Adjust this condition based on your obstacle detection criteria
            if (hit.collider.CompareTag("Wall"))
            {
                // Obstacle found, path is obstructed
                return true;
            }
        }

        // Path is clear
        return false;
    }
}
