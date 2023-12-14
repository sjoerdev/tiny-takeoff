using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerBeginningLogic : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 startRotation;
    [SerializeField] private GameObject camera;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 idealPointInCamera;
    [SerializeField] private float beginningTime = 1f;

    [Range(0f,1f)]
    [SerializeField] private float zRotationFactor;
    [SerializeField] private GameObject visuals;
    [SerializeField] private float beginningVelocity;
    private Glide glide;
    private Vector3 currentVelocity;

    bool playing;

    void Start()
    {
        glide = GetComponent<Glide>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        switch(GameManager.Instance.gameState)
        {
            case GameStates.start:
                transform.parent = camera.transform;
                transform.localPosition = startPosition;
                transform.localRotation = Quaternion.Euler(startRotation); 
                glide.xRotation = transform.eulerAngles.x;
                glide.yRotation = transform.eulerAngles.y;
                break;
            case GameStates.beginning:
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, idealPointInCamera,ref currentVelocity, beginningTime);
                StartCoroutine(Beginning());
                playing = false;
                rb.useGravity = false;
                rb.drag = 0f;
                break;
            case GameStates.playing:
                if(playing == false)
                {
//                    UnityEngine.Debug.Log("beginningVelocity");
                    playing = true;
                    rb.useGravity = true;
                    rb.drag = 1f;
                    //Vector3 velocity = transform.InverseTransformDirection(rb.velocity);
                    rb.velocity = transform.rotation * (Vector3.forward * beginningVelocity);
                }
                break;
        }
    }

    private IEnumerator Beginning()
    {
        yield return new WaitForSecondsRealtime(beginningTime);
        GameManager.Instance.gameState = GameStates.playing;
        transform.SetParent(null);
        yield return null;
    }
}
