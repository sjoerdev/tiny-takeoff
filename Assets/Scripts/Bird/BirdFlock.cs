using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlock : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    [Range(0f,1f)]
    [SerializeField] private float rotationLerp;

    [SerializeField] private float terraincheckDistance;
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private float maxY;
    [SerializeField] private float upAngle;
    [SerializeField] private float downAngle;
    // Update is called once per frame
    void FixedUpdate()
    {
        //if the bird goes up against a slope, rotate them up
        RaycastHit forwardHit;
        RaycastHit downHit;

        Vector3 forwardCast = transform.rotation * Vector3.forward;
        forwardCast.y = 0;

        Physics.Raycast(transform.position, Vector3.down, out downHit, Mathf.Infinity, terrainLayer);
        if(Physics.Raycast(transform.position,forwardCast.normalized, out forwardHit, terraincheckDistance, terrainLayer))
        {
                //get slope
                Vector3 slopeDir = forwardHit.point - downHit.point;
                
                float slopeX =  Quaternion.LookRotation(slopeDir).eulerAngles.x;

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(slopeX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z),rotationLerp);
        }
        else
        {
            if(Vector3.Distance(transform.position, downHit.point) <= terraincheckDistance)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(upAngle,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z),rotationLerp);
            }
            else if(transform.position.y > maxY)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(downAngle,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z),rotationLerp);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, transform.eulerAngles.y, transform.eulerAngles.z),rotationLerp);
            }
        }

        //move forward
        transform.position += transform.rotation * Vector3.forward * speed * Time.deltaTime;
    }
}
