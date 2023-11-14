using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Glide : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float forwardFactor;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minVelocity;
    private float currentForwardSpeed;
    float forwardVelocity;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {

        xRotation += 30 * Input.GetAxis("Vertical") * Time.deltaTime;
        
        yRotation += 30 * Input.GetAxis("Horizontal") * Time.deltaTime;



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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}