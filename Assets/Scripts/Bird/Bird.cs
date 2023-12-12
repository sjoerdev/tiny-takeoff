using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private Transform point;
    [Range(0f,1f)]
    [SerializeField] private float moveLerp; 
    [Range(0f,1f)]
    [SerializeField] private float rotationLerp; 
    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion newRotation = Quaternion.LookRotation(point.position - transform.position);

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationLerp);
        transform.position = Vector3.Lerp(transform.position,point.position,moveLerp);
    }
}
