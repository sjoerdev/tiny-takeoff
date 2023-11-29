using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI : MonoBehaviour
{
    PlayerControlls playerControlls;
    private InputAction pause;
    [Range(0f,1f)]
    [SerializeField] private float pauseTimeScale;

    void Awake()
    {
        playerControlls = new PlayerControlls();
    }

    void OnEnable()
    {
        pause = playerControlls.Player.Pause;
        pause.Enable();
        pause.performed += Pause;
    }

    void OnDisable()
    {
        pause.Disable();
    }

    private void Pause(InputAction.CallbackContext context)
    {
        if(GameManager.Instance.gameState != GameStates.start)
            if(GameManager.Instance.gameState == GameStates.playing)
            {
                Debug.Log("We paused");
                Time.timeScale = pauseTimeScale;
                GameManager.Instance.gameState = GameStates.paused;
            }
            else if(GameManager.Instance.gameState == GameStates.paused)
            {
                Debug.Log("We play");
                Time.timeScale = 1f;
                GameManager.Instance.gameState = GameStates.playing;
            }
    }

    public void StartButton()
    {
        GameManager.Instance.gameState = GameStates.beginning;
    }
}
