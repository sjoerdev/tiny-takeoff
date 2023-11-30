using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Callbacks;
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
    [SerializeField] private float beginningLerpFactor;
    [SerializeField] private float zRotationFactor;
    [SerializeField] private GameObject visuals;
    private Glide glide;

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
                transform.position = Vector3.Lerp(transform.position, camera.transform.position + camera.transform.rotation*idealPointInCamera, beginningLerpFactor);
                StartCoroutine(Beginning());
                break;
        }
    }

    private IEnumerator Beginning()
    {
        yield return new WaitForSeconds(beginningTime);
        GameManager.Instance.gameState = GameStates.playing;
        transform.SetParent(null);
        yield return null;
    }
}
