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
    [SerializeField] private float startingSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float forwardFactor;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float gravity;
    private float currentForwardSpeed;

    [SerializeField] private float startStallingSpeed;
    [SerializeField] private float endStallingSpeed;


    public float xRotation;
    public float yRotation;


    [Header("Controls")]
    PlayerControlls playerControlls;
    private InputAction move;
    [Range(0f, 1f)] 
    [SerializeField] float rotationFactor;
    [SerializeField] private float xRotationForce = 30;
    
    [SerializeField] private float yRotationForce = 50;
    [Range(0f,1f)]
    [SerializeField] private float rotationForceLerpStrenght = 0.9f;
    [SerializeField] private float zRotationFactor = 20f;
    [SerializeField] private GameObject visuals;

    [SerializeField] private float stallingRotation;
    [SerializeField] private float stallingLerp;

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

    //windvector
    [SerializeField] private AudioSource windVectorSound;

    Coroutine stalling;


    void Awake()
    {
        playerControlls = new PlayerControlls();
        Physics.gravity = Vector3.down * gravity;
        currentForwardSpeed = startingSpeed;
    }

    void OnEnable()
    {
        move = playerControlls.Player.Move;
        move.Enable();
    }
    
    void OnDisable()
    {
        move.Disable();
    }

    private void FixedUpdate()
    {
        //controlls

        if(GameManager.Instance.gameState == GameStates.playing)
        {
            lerpedXRotationForce = Mathf.Lerp(lerpedXRotationForce, xRotationForce * move.ReadValue<Vector2>().y, rotationForceLerpStrenght);
            lerpedYRotationForce = Mathf.Lerp(lerpedYRotationForce, yRotationForce * move.ReadValue<Vector2>().x, rotationForceLerpStrenght);
            if(stalling == null)
            {
                xRotation += lerpedXRotationForce * Time.deltaTime;
            }
            yRotation += lerpedYRotationForce * Time.deltaTime;
        }
        if(GameManager.Instance.gameState == GameStates.beginning)
        {
            return;
        }

        //if player is looking down its positive, if player is looking up its negative
        float mappedPitch = Mathf.Sin(transform.rotation.eulerAngles.x * Mathf.Deg2Rad) * forwardFactor;


        currentForwardSpeed += mappedPitch;           
        Mathf.Clamp(currentForwardSpeed, 0, maxSpeed);

        if(currentForwardSpeed < startStallingSpeed)
        {
            stalling = StartCoroutine(Stalling());
        }

        //force in direction player is looking
        transform.rotation = Quaternion.Euler(xRotation, yRotation,0f);
        rb.AddRelativeForce(Vector3.forward * currentForwardSpeed);

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
                //get slope of terrain
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
        currentForwardSpeed = transform.InverseTransformDirection(rb.velocity).z;
        windVectorSound.Play();
    }

    void OnCollisionStay()
    {
        currentForwardSpeed = transform.InverseTransformDirection(rb.velocity).z;
    }

    private IEnumerator Stalling()
    {
        while(currentForwardSpeed <= endStallingSpeed)
        {
            xRotation = Mathf.Lerp(xRotation, stallingRotation, stallingLerp);
            yield return new WaitForFixedUpdate();
        }
        stalling = null;
        yield return null;
    }
}
