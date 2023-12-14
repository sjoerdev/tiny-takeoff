using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailCheck : MonoBehaviour
{
    [SerializeField] private float failDistance;
    [SerializeField] private LayerMask terrainLayer;

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.rotation * Vector3.up, out hit, failDistance, terrainLayer))
        {
            GameManager.Instance.gameState = GameStates.start;
        }
    }
}
