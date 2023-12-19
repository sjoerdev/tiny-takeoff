using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string textBeforeScore;

    // Update is called once per frame
    void Update()
    {
        text.text = textBeforeScore + GameManager.Instance.score.ToString("0,0");
    }
}
