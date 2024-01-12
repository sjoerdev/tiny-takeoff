using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorSpawnManager : MonoBehaviour
{
    public VectorPoolManager objectPool;
    public Transform playerTransform;
    public float spawnInterval = 3f;
    public float spawnDistance = 10f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0;
        }
    }

    void SpawnObject()
    {
        GameObject obj = objectPool.GetObject();
        if (obj != null)
        {
            obj.transform.position = transform.position + Random.onUnitSphere * spawnDistance;
            obj.GetComponent<StationaryObject>().SetPlayerTransform(playerTransform);
        }
    }
}
