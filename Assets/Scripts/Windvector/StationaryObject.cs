using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryObject : MonoBehaviour
{
    public Transform playerTransform;
    public float returnToPoolDistance = 15f;
    private VectorPoolManager objectPool;

    void Start()
    {
        objectPool = FindObjectOfType<VectorPoolManager>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) > returnToPoolDistance)
        {
            objectPool.ReturnObject(gameObject);
        }
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }
}
