using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

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
    PlayerControlls playerControlls;
    private InputAction move;
    private InputAction fire;
    [Range(0f, 1f)] 
    [SerializeField] float rotationFactor;
    [SerializeField] private float xRotationForce = 30;
    
    [SerializeField] private float yRotationForce = 50;
    [Range(0f,1f)]
    [SerializeField] private float rotationForceLerpStrenght = 0.9f;
    [SerializeField] private float zRotationFactor = 20f;
    [SerializeField] private GameObject visuals;

    private float lerpedXRotationForce;
    private float lerpedYRotationForce;
    private float zRotation;

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
    [SerializeField] private float cloudSkitRotationLerp = 0.05f;
    [Range(0f,1f)]
    [SerializeField] private float cloudSkitSpeedLerp = 0.4f;


    void Awake()
    {
        playerControlls = new PlayerControlls();
    }

    void OnEnable()
    {
        move = playerControlls.Player.Move;
        move.Enable();

        fire = playerControlls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }
    
    void OnDisable()
    {
        move.Disable();
        fire.Disable();
    }


    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("We fired");
    }

    private void FixedUpdate()
    {
        //controlls

        if(GameManager.Instance.gameState == GameStates.playing)
        {
            lerpedXRotationForce = Mathf.Lerp(lerpedXRotationForce, xRotationForce * move.ReadValue<Vector2>().y, rotationForceLerpStrenght);
            lerpedYRotationForce = Mathf.Lerp(lerpedYRotationForce, yRotationForce * move.ReadValue<Vector2>().x, rotationForceLerpStrenght);
            xRotation += lerpedXRotationForce * Time.deltaTime;
            yRotation += lerpedYRotationForce * Time.deltaTime;
        }

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
        transform.rotation = Quaternion.Euler(xRotation, yRotation,0f);
        rb.AddRelativeForce(Vector3.forward * currentForwardSpeed);

        //minimum force so player always goes forward
        if( transform.InverseTransformDirection(rb.velocity).z <= minVelocity)
        {
            rb.AddRelativeForce(Vector3.forward * minVelocity, ForceMode.VelocityChange);
        }

        //skitting over clouds
        CloudSkit();

        //max velocity
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        //rotation on z axis
        transform.rotation = Quaternion.Euler(xRotation,yRotation, 0f);
        zRotation =  transform.InverseTransformVector(rb.velocity).normalized.x * zRotationFactor;
        visuals.transform.localRotation = Quaternion.Euler(0,0,zRotation);
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
                rb.velocity = Vector3.Lerp(rb.velocity, slopeDir.normalized * speed,cloudSkitSpeedLerp);

                //rotate player in direction of slope
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rb.velocity), cloudSkitRotationLerp);
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
