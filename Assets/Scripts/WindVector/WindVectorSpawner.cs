using System.Collections.Generic;
using UnityEngine;

public class WindVectorSpawner : MonoBehaviour
{
    public VectorPool pool;
    public float spawnInterval = 5f;
    public float spawnRadius = 1500f; // The radius around the player where Vectors can spawn
    private float timer;
    private Transform playerTransform;
    private float spawnHeight = 30;
    public float minSpawnDistance = 30f;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnVector();
            timer = 0f;
        }
    }

    private void SpawnVector()
    {
        if (playerTransform == null) return;

        Vector3 spawnPosition;
        do
        {
            Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
            randomDirection.y = 0; // Maintain the fixed height for Y-axis

            spawnPosition = playerTransform.position + randomDirection;
            pool.prefab.transform.rotation = playerTransform.rotation;
        }
        while (Vector3.Distance(spawnPosition, playerTransform.position) < minSpawnDistance);

        spawnPosition.y = spawnHeight; // Set the Y-coordinate to the fixed spawn height

        GameObject island = pool.GetVector();
        island.transform.position = spawnPosition;
    }


}
