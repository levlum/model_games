using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Array to hold different prefab objects
    public float spawnRadius = 10f; // Radius around the player to spawn objects
    public float spawnInterval = 30f; // Time interval between spawns
    public Transform playerTransform; // Reference to the player's transform

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnRandomObject();
        }
    }

    void SpawnRandomObject()
    {
        // Randomly select an object to spawn
        int randomIndex = Random.Range(0, objectsToSpawn.Length);
        GameObject objectToSpawn = objectsToSpawn[randomIndex];

        // Randomly generate a position within the spawn radius
        Vector3 spawnPosition = playerTransform.position + (Random.insideUnitSphere * spawnRadius);
        spawnPosition.y = 10; // Assuming the ground is at y=0

        // Instantiate the object
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // Randomize the size of the object, ensuring it is smaller than the player
        float playerSize = playerTransform.localScale.x; // Assuming uniform scaling
        float randomScale = Random.Range(0.5f, 3f);
        spawnedObject.transform.localScale = Vector3.one * randomScale;

        // Tag the spawned object as "Collectible"
        spawnedObject.tag = "Collectible";
    }
}

