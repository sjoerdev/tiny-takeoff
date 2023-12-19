using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class CamLogic : MonoBehaviour
{
    [SerializeField] private float startYHeight;
    [Range(0f,1f)]
    [SerializeField] private float startYHeightLerpStrenght;
    [SerializeField] private float startMovementSpeed;

    [Header("Play logic")]
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Vector3 position;
    // Update is called once per frame
    void Update()
    {
        switch(GameManager.Instance.gameState)
        {
            case GameStates.start:
                StartLogic();
                break;
            case GameStates.beginning:
                StartLogic();
                break;
            case GameStates.playing:
                PlayLogic();
                break;
        }
    }

    private void StartLogic()
    {
        transform.SetParent(null);
        transform.rotation = Quaternion.Euler(rotation.x,transform.eulerAngles.y, rotation.z);
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, startYHeight, transform.position.z), startYHeightLerpStrenght);

        //move forward
        Vector3 forwardMovement = transform.forward;
        forwardMovement.y = 0;
        transform.position += forwardMovement.normalized * startMovementSpeed;
    }

    private void PlayLogic()
    {
        transform.SetParent(player.transform);
        transform.localPosition = position;
        transform.localRotation = Quaternion.Euler(rotation);
    }
}
