using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCameraFollow : MonoBehaviour
{
    CameraFollow gameCamera;

    private void Start()
    {
        gameCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            gameCamera.cameraFollow = !gameCamera.cameraFollow;
        }
    }
}