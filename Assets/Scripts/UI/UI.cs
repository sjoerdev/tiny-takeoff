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

    [SerializeField] private GameObject quitText;

    void Awake()
    {
        playerControlls = new PlayerControlls();
        quitText.SetActive(false);
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
                quitText.SetActive(true);
            }
            else if(GameManager.Instance.gameState == GameStates.paused)
            {
                Debug.Log("We play");
                Time.timeScale = 1f;
                GameManager.Instance.gameState = GameStates.playing;
                
                quitText?.SetActive(false);
            }
    }

    public void StartButton()
    {
        GameManager.Instance.gameState = GameStates.beginning;
    }
}
