using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkyBox : MonoBehaviour
{
    [SerializeField] private Material skyBox;
    private LightingManager lightingManager;
    [SerializeField] private float maxTime = 600;
    private float dayTime;

    // Start is called before the first frame update
    void Start()
    {
        lightingManager = GetComponentInParent<LightingManager>();
        dayTime = maxTime/2f;
    }

    // Update is called once per frame
    void Update()
    {
        float time = lightingManager.GetTimeOfDay();
        float timePercent = 0f;
        if(time >= dayTime)
        {
            time -= dayTime;
            float adjustedMaxTime = maxTime - dayTime;
            timePercent = time/adjustedMaxTime;
        }
        else
        {
            timePercent = time/dayTime;
            timePercent *= -1f;
            timePercent += 1f;
        }
        skyBox.SetFloat("_CubemapTransition", timePercent);
    }
}
