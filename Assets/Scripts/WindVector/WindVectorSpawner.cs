using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindVectorSpawner : MonoBehaviour
{
    public VectorPool speedBoostPool;
    public float spawnInterval = 5f;
    public float spawnHeightOffset = 2f;
    public float raycastHeight = 10f; // Height from which to start the raycast
    public LayerMask groundLayer; // Layer mask to identify the ground
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnSpeedBoost();
            timer = 0;
        }
    }

    void SpawnSpeedBoost()
    {
        GameObject speedBoost = speedBoostPool.GetObject();
        if (speedBoost != null)
        {
            Vector3 spawnPosition = GetRandomGroundPosition();
            if (spawnPosition != Vector3.zero)
            {
                spawnPosition.y += spawnHeightOffset;
                speedBoost.transform.position = spawnPosition;
            }
            else
            {
                // If no ground found, return the object back to the pool
                speedBoostPool.ReturnObject(speedBoost);
            }
        }
    }

    Vector3 GetRandomGroundPosition()
    {
        Vector3 origin = new Vector3(Random.Range(-10, 10), raycastHeight, Random.Range(-10, 10));
        RaycastHit hit;

        if (Physics.Raycast(origin, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
