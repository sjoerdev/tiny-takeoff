using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float minimumGlideSpeed;
    [SerializeField] private float maximumGlideSpeed;
    [SerializeField] private Rigidbody rb;

    void FixedUpdate()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Terrain"))
        {

        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Terrain"))
        {
            
        }
    }
}
