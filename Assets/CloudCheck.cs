using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloudCheck : MonoBehaviour
{
    public bool IsGrounded;

    public GameObject endMenu;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Clouds")
            {
                NotGrounded();
            }
        }
    }

    void NotGrounded()
    {
        Time.timeScale = 0f;
        endMenu.SetActive(true);

    }

    public void ReturnToStart()
    {
        SceneManager.LoadScene("StartScene");
    }

}
