using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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
    [SerializeField] private float cloudDistance;
    private Transform flockPoint;
    private float playerSpeed;
    [SerializeField] private float playerSpeedAmplitude;
    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        flock = Instantiate(flock);
        flock.SetActive(false);

        flockPoint = flock.GetComponentInChildren<BirdFlock>().transform;
        StartCoroutine(Spawning());

    }

    void FixedUpdate()
    {
        playerSpeed = Vector3.Distance(previousPosition, player.position) * playerSpeedAmplitude;
        if(Vector3.Distance(player.transform.position, flockPoint.transform.position) >= despawnDistance)
        {
            flock.SetActive(false);
        }

        Debug.Log(playerSpeed);

        previousPosition = player.position;
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
        Vector3 positionOffset = player.rotation * Vector3.forward;
        positionOffset.y = 0;
        flock.transform.position = (player.position + positionOffset * playerSpeed) + (flock.transform.rotation * Vector3.back * spawnDistance);
        
        //birds always spawn above terrain
        RaycastHit hit;
//        Debug.Log("doing raycast at" + flock.transform.position);
        if(Physics.Raycast(flock.transform.position + Vector3.up * 100f, Vector3.down, out hit, Mathf.Infinity, terrainLayer))
        {
//            Debug.Log("birds should spawn at "  + hit.point);
            flock.transform.position = hit.point + Vector3.up * cloudDistance;
        }

        flockPoint.localPosition = Vector3.zero;
        flockPoint.localRotation = Quaternion.Euler(Vector3.zero);
        flock.SetActive(true);
    }
}
