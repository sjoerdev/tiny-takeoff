using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WindSound : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [Min(0f)]
    [SerializeField] private float minPitch = 0.1f;
    [SerializeField] private float maxPitch = 2f;
    [Min(0f)]
    [SerializeField] private float minVolume = 0.1f;
    [SerializeField] private float maxVolume = 1f;
    private Transform camera;
    [SerializeField] private Rigidbody rb;
    private AudioSource wind;
    private Vector3 previousPosition;

    private float volumeDelta;
    private float pitchDelta;

    void Start()
    {
        wind = GetComponent<AudioSource>();
        camera = GetComponentInParent<Transform>();
        volumeDelta = (maxVolume - minVolume) / 1f;
        pitchDelta = (maxPitch - minPitch) / 1f;
    }

    void FixedUpdate()
    {
        //calculates the speed of the player
        float speed = rb.velocity.magnitude;
        float speedPercentage = 0;
        if(speed != 0)
        {
            //speed percentage
            speedPercentage = speed / (maxSpeed - minSpeed);

        }
        speedPercentage = Mathf.Clamp(speedPercentage, 0f,1f);


        //linear formula between min and max depending on speed;
        wind.pitch = pitchDelta * speedPercentage + minPitch;
        wind.volume = volumeDelta * speedPercentage + minVolume;

        previousPosition = camera.position;
    }
}
