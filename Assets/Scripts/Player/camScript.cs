using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camScript : MonoBehaviour
{
    [SerializeField] private Transform camPoint;
    [Range(0f,1f)]
    [SerializeField] private float rotationDrag;
    [Range(0f,1f)]
    [SerializeField] private float positionDrag;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, camPoint.position, positionDrag);
        transform.rotation = Quaternion.Lerp(transform.rotation, camPoint.rotation, rotationDrag);
    }
}
