using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Glide : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float forwardFactor;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minVelocity;
    private float currentForwardSpeed;

    float xRotation;
    float yRotation;
    [SerializeField] float maxXRotation;
    [Range(0f, 1f)] 
    [SerializeField] float rotationFactor;


    //glide shit
    
    [SerializeField] private float minCloudSpeed;
    [SerializeField] private float maxCloudSpeed;
    [SerializeField] private float raycastDistance;
    [SerializeField] private float raycastHeightStart;
    [SerializeField] private float cloudForce;
    [SerializeField] private LayerMask terrainlayer;


    //for testing
    public Vector3 slopeDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {

        xRotation += 30 * Input.GetAxis("Vertical") * Time.deltaTime;
        
        yRotation += 50 * Input.GetAxis("Horizontal") * Time.deltaTime;

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

        transform.rotation = Quaternion.Euler(xRotation, yRotation,0);
        rb.AddRelativeForce(Vector3.forward * currentForwardSpeed);

        if( transform.InverseTransformDirection(rb.velocity).z <= minVelocity)
        {
            rb.AddRelativeForce(Vector3.forward * minVelocity, ForceMode.VelocityChange);
        }

        CloudSkit();
    }

    private void CloudSkit()
    {
        //skit over the clouds
        
        RaycastHit forwardHit;
        Vector3 forwardDir = rb.velocity.normalized;

        RaycastHit downHit;
        Vector3 downDir = transform.rotation * Vector3.down;

        
        if(Physics.Raycast(transform.position, forwardDir, out forwardHit, rb.velocity.magnitude, terrainlayer))
        {
            if(Physics.Raycast(transform.position, downDir, out downHit, raycastDistance, terrainlayer))
            {

            Debug.Log("in cloud");

            

            slopeDir = forwardHit.point - downHit.point;

            float speed = rb.velocity.magnitude;
            rb.velocity = Vector3.Lerp(rb.velocity, slopeDir.normalized * speed,0.4f);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rb.velocity), 0.05f);
            xRotation = transform.rotation.eulerAngles.x;

            rb.AddForce(transform.forward * cloudForce, ForceMode.VelocityChange);
            }
        }
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.rotation *new Vector3(0,-1,1)*raycastDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + slopeDir * 50);
    }
}
