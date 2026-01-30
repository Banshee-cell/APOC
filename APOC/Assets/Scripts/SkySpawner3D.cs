using UnityEngine;

public class SpawnerAtObject : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject creaturePrefab;    // Creature prefab
    public int numberOfCreatures = 5;    // How many to spawn
    public float spawnHeight = 20f;      // Height above the spawner

    void Start()
    {
        SpawnCreatures();
    }

    void SpawnCreatures()
    {
        for (int i = 0; i < numberOfCreatures; i++)
        {
            // Spawn directly above the spawner object
            Vector3 spawnPos = transform.position + Vector3.up * spawnHeight;

            Instantiate(creaturePrefab, spawnPos, Quaternion.identity);
        }
    }
}
