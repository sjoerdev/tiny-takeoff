using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    Vector3 previousPosition;
    float traveledDistance;
    [SerializeField] private float scoreMagnitude;
    float score;

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if(GameManager.Instance.gameState == GameStates.playing){

        traveledDistance += Vector3.Distance(transform.position, previousPosition);
        score = traveledDistance / scoreMagnitude;
        GameManager.Instance.score = score;
        }
        previousPosition = transform.position;
    }

    public void Reset()
    {
        traveledDistance = 0f;
        
        score = 0f;
        GameManager.Instance.score = 0f;
    }
}
