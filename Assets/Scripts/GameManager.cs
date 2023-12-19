using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    start,
    beginning,
    playing,
    paused
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameStates gameState;
    public float score;
    void Awake()
    {
        if(GameManager.Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one GameManager found");
        }
    }
}
