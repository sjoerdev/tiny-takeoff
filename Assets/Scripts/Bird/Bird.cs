using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private Transform point;
    [Range(0f,1f)]
    [SerializeField] private float moveLerp; 
    [Range(0f,1f)]
    [SerializeField] private float rotationLerp; 

    void OnEnable()
    {
        transform.position = point.position;
        transform.rotation = point.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.Instance.gameState == GameStates.paused)
        {
            return;
        }

        if(Vector3.Distance(transform.position, point.position) != 0f)
        {
            Quaternion newRotation = Quaternion.LookRotation(point.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationLerp);
        }

        transform.position = Vector3.Lerp(transform.position,point.position,moveLerp);

    }
}
