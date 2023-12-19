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

        if(GameManager.Instance.gameState == GameStates.playing && Physics.Raycast(transform.position + Vector3.up * failDistance, Vector3.down, out hit, 1f, terrainLayer))
        {
            if(Vector3.Distance(transform.position, hit.point) <= failDistance)
            {
                GameManager.Instance.gameState = GameStates.start;
            }
        }
    }
}
