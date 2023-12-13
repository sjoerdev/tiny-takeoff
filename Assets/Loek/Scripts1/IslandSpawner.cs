using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandSpawner : MonoBehaviour
{
    public ObjectPool pool;
    public float spawnInterval = 5f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnIsland();
            timer = 0f;
        }
    }
        
    private void SpawnIsland()
    {
        GameObject island = pool.GetObject();
       
        island.transform.position = new Vector3(0, 600, 0); // Example position
    }
}
