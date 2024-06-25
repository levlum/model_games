using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TeleportOnCollision : MonoBehaviour
{
    // The height to teleport the sphere upwards
    public float teleportHeight = 100f;
    public float xRange = 10f;
    public float zRange = 10f;
    // Method called when the sphere collides with another collider
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the floor
        if (collision.gameObject.CompareTag("Floor"))
        {
            // Get the current position of the sphere
            Vector3 currentPosition = transform.position;

            // Generate random shifts for the x and z axes
            float randomXShift = Random.Range(-xRange, xRange);
            float randomZShift = Random.Range(-zRange, zRange);

            // Teleport the sphere upwards by the specified height with random x and z shifts
            transform.position = new Vector3(currentPosition.x + randomXShift, currentPosition.y + teleportHeight, currentPosition.z + randomZShift);
        }
    }
}
