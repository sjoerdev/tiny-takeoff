using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float minimumGlideSpeed;
    [SerializeField] private float maxNessesairyGlideSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float raycastDistance;

    bool isInCloud;

    void FixedUpdate()
    {

        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down * raycastDistance, out hit))
        {
            Vector3.Dot(transform.rotation * Vector3.up, hit.normal);
        }
    }
}
