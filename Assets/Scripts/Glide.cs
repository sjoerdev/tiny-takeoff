using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Glide : MonoBehaviour
{
    [Header("glide")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float forwardFactor;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minVelocity;
    private float currentForwardSpeed;

    float xRotation;
    float yRotation;


    [Header("Controls")]
    [Range(0f, 1f)] 
    [SerializeField] float rotationFactor;
    [SerializeField] private float xRotationForce = 30;
    
    [SerializeField] private float yRotationForce = 50;

    [Header("WindVector")]
    //windvector
    [SerializeField] private float vectorThrust = 20f;

    [Header("Cloud Skitting settings")]
    //cloud skit parameters
    [SerializeField] private float raycastDistance;
    [SerializeField] private float raycastHeightStart;
    [SerializeField] private float cloudForce;
    [SerializeField] private LayerMask terrainlayer;
    [Range(0f,1f)]
    [SerializeField] private float cloudSkitRotationLerp = 0.4f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //controlls
        xRotation += xRotationForce * Input.GetAxis("Vertical") * Time.deltaTime;
        
        yRotation += yRotationForce * Input.GetAxis("Horizontal") * Time.deltaTime;

        //if player is looking down its positive, if player is looking up its negative
        float mappedPitch = Mathf.Sin(transform.rotation.eulerAngles.x * Mathf.Deg2Rad) * forwardFactor;


        if(rb.velocity.magnitude >= minVelocity)
        {
            currentForwardSpeed += mappedPitch;           
            Mathf.Clamp(currentForwardSpeed, 0, maxSpeed);
        }
        else
        {
            currentForwardSpeed = 0f;
        }

        //force in direction player is looking
        transform.rotation = Quaternion.Euler(xRotation, yRotation,0);
        rb.AddRelativeForce(Vector3.forward * currentForwardSpeed);

        //minimum force so player always goes forward
        if( transform.InverseTransformDirection(rb.velocity).z <= minVelocity)
        {
            rb.AddRelativeForce(Vector3.forward * minVelocity, ForceMode.VelocityChange);
        }

        //skitting over clouds
        CloudSkit();
    }

    private void CloudSkit()
    {
        
        
        RaycastHit forwardHit;
        Vector3 forwardDir = rb.velocity.normalized;

        RaycastHit downHit;
        Vector3 downDir = transform.rotation * Vector3.down;

        
        if(Physics.Raycast(transform.position, forwardDir, out forwardHit, rb.velocity.magnitude, terrainlayer))
        {
            if(Physics.Raycast(transform.position, downDir, out downHit, raycastDistance, terrainlayer))
            {
                //get slope
                Vector3 slopeDir = forwardHit.point - downHit.point;

                //lerp rb velocity in direction of slope
                float speed = rb.velocity.magnitude;
                rb.velocity = Vector3.Lerp(rb.velocity, slopeDir.normalized * speed,0.4f);

                //rotate player in direction of slope
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rb.velocity), 0.05f);
                xRotation = transform.rotation.eulerAngles.x;

                //add force forward so the player can climb up
                rb.AddForce(transform.forward * cloudForce, ForceMode.VelocityChange);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.rotation *new Vector3(0,-1,1)*raycastDistance);
    }

    public void WindVector()
    {
        rb.AddForce(transform.forward * vectorThrust, ForceMode.Impulse);
    }


}
