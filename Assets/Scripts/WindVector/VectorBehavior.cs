using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorBehavior : MonoBehaviour
{
    public VectorPool pool;
    public Transform playerTransform;
    public float deactivateDistance = 100f; // Distance at which the island deactivates

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        pool = GameObject.FindObjectOfType<VectorPool>();
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
            pool.ReturnVector(gameObject);
        }
        else
        {
            Debug.LogError("ObjectPool reference is null.");
        }
    }
}
