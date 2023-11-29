using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI : MonoBehaviour
{
    public void StartButton()
    {
        GameManager.Instance.gameState = GameStates.beginning;
    }
}
