using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WindSound : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minPitch = 0.1f;
    [SerializeField] private float maxPitch = 2f;
    [SerializeField] private float minVolume = 0.1f;
    [SerializeField] private float maxVolume = 1f;
    private Transform camera;
    private AudioSource wind;
    private Vector3 previousPosition;

    private float volumeDelta;
    private float pitchDelta;
    // Start is called before the first frame update
    void Start()
    {
        wind = GetComponent<AudioSource>();
        camera = GetComponentInParent<Transform>();
        volumeDelta = (maxVolume - minVolume) / 1f;
        pitchDelta = (maxPitch - minPitch) / 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float speed = (camera.position - previousPosition).magnitude / Time.fixedDeltaTime;
        float speedPercentage = 0;
        if(speed != 0)
        {
            speedPercentage = speed / (maxSpeed - minSpeed);

        }
        Debug.Log(speedPercentage);
        wind.pitch = pitchDelta * speedPercentage + minPitch;
        wind.volume = volumeDelta * speedPercentage + minVolume;
        wind.pitch = Mathf.Clamp(wind.pitch, 0,maxPitch);
        wind.volume = Mathf.Clamp(wind.pitch, 0,maxVolume);

        previousPosition = camera.position;

    }
}
