using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkyBox : MonoBehaviour
{
    [SerializeField] private Material skyBox;
    private LightingManager lightingManager;

    // Start is called before the first frame update
    void Start()
    {
        lightingManager = GetComponentInParent<LightingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        skyBox.SetFloat("_CubemapTransition", 0.2314f);
    }
}
