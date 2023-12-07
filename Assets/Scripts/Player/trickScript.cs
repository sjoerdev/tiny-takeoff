using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class trickScript : MonoBehaviour
{
    [SerializeField] private AudioSource trick;
    PlayerControlls playerControlls;
    
    private InputAction fire;


    void Awake()
    {
        playerControlls = new PlayerControlls();
    }

    void Start()
    {
        trick = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        fire = playerControlls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
    }

    void OnDisable()
    {
        fire.Disable();
    }

    private void Fire(InputAction.CallbackContext context)
    {
        trick.Play();
    }
}
