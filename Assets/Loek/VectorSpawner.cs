using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject resourcePrefab;
    public float spawnChance;

    [Header("Raycast Settings")]
    public float distanceBetweenChecks;
    public float heightOfCheck = 10f, rangeOfCheck = 30f;
    public LayerMask layerMask;
    public Vector2 positivePos, negativePos;

    private void Start()
    {
        SpawnResources();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Instantiate(resourcePrefab);
        }
    }

    void SpawnResources()
    {
        
        for (float x = negativePos.x; x < positivePos.x; x += distanceBetweenChecks)
        {
            for (float z = negativePos.y; z < positivePos.y; z += distanceBetweenChecks)
            { 
                Debug.Log("Working");
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(x, heightOfCheck, z), Vector3.down, out hit, rangeOfCheck, layerMask))
                {
                    if (spawnChance > Random.Range(0, 101))
                    {
                        Instantiate(resourcePrefab, hit.point, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), transform);
                    }
                }   

            }
        }
        
    }
}
