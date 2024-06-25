using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDuplicator : MonoBehaviour
{
    public GameObject prefab; // Assign the original red sphere prefab

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collider object is Entity 1 (Blue Sphere)
        if (collision.gameObject.CompareTag("Entity1"))
        {
            DuplicateSphere();
        }
    }

    void DuplicateSphere()
    {
        // Create a new sphere at the position of the current sphere
        GameObject newSphere = Instantiate(prefab, transform.position + new Vector3(1, 0, 0), Quaternion.identity);

        // Optionally, you can also add a slight offset to the position to prevent stacking
        newSphere.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0); // Add some initial velocity if desired
    }
}
