using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public void StartButton()
    {
        GameManager.Instance.gameState = GameStates.beginning;
    }
}
