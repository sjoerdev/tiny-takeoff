using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindVector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Glide>().WindVector();
        print("Vector");

    }
}
