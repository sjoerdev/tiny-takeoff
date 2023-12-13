using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] private GameObject flock;
    [SerializeField] private Transform player;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private float spawnDistance;
    [SerializeField] private float despawnDistance;
    [SerializeField] private LayerMask terrainLayer;
    private Transform flockPoint;

    // Start is called before the first frame update
    void Start()
    {
        flock = Instantiate(flock);
        flock.SetActive(false);

        flockPoint = flock.GetComponentInChildren<BirdFlock>().transform;
        StartCoroutine(Spawning());

    }

    void Update()
    {
        if(Vector3.Distance(player.transform.position, flockPoint.transform.position) >= despawnDistance)
        {
            flock.SetActive(false);
        }
    }

    private IEnumerator Spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnTime, maxSpawnTime));

            if(!flock.activeSelf && GameManager.Instance.gameState != GameStates.paused)
            {
                SpawnFlock();
            }
        }
    }

    private void SpawnFlock()
    {
        //select new point for birds to spawn
        flock.transform.rotation = Quaternion.Euler(0,UnityEngine.Random.Range(0f,360f),0);
        flock.transform.position = player.position + (flock.transform.rotation * Vector3.back * spawnDistance);
        
        //birds always spawn above terrain
        RaycastHit hit;
        Debug.Log("doing raycast at" + flock.transform.position);
        if(Physics.Raycast(flock.transform.position, Vector3.up, out hit, Mathf.Infinity, terrainLayer))
        {
            Debug.Log("birds should spawn at "  + hit.point);
            flock.transform.position = hit.point;
        }

        flockPoint.localPosition = Vector3.zero;
        flockPoint.localRotation = Quaternion.Euler(Vector3.zero);
        flock.SetActive(true);
    }
}
