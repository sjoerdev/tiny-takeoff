using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandBehaviour : MonoBehaviour
{
    public ObjectPool pool;
    public float lifeTime = 10f; 

    void OnEnable()
    {
        pool = FindObjectOfType<ObjectPool>();
        Invoke(nameof(ReturnToPool), lifeTime);
    }

    private void ReturnToPool()
    {
        pool.ReturnObject(gameObject);
    }

    // Implement logic to detect when the player passes the island
    // And call ReturnToPool()
}
