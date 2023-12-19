using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandBehaviour : MonoBehaviour
{
    public ObjectPool pool;
    public Transform playerTransform;
    public float deactivateDistance = 100f; // Distance at which the island deactivates

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        pool = GameObject.FindObjectOfType<ObjectPool>();
    }

    private void Update()
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance > deactivateDistance)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        if (pool != null)
        {
            pool.ReturnObject(gameObject);
        }
        else
        {
            Debug.LogError("ObjectPool reference is null.");
        }
    }
}
