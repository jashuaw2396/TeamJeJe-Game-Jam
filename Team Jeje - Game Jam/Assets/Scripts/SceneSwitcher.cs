using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    // Here we have the game camera, which is the object we want to move
    GameObject gameCamera;
    // End position that we want to lerp to
    private Vector3 endPos;

    // distance value
    public float distance = 0;

    // Total time it'll take to lerp fully
    public float lerpTime = 0;
    // Current lerp time
    private float currentLerpTime = 0;

    //private bool startDelay = false;
    //private float delayTimer = 0;

    // Bool used to see which direction we're heading
    public bool rightDirection = true;
    // Bool used to start lerp
    private bool startLerp = false;

    // Index
    public int Index = 0;

	// Use this for initialization
	void Start ()
    {
        gameCamera = GameObject.FindWithTag("MainCamera");
	}

    private void Update()
    {
        if (startLerp)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
                startLerp = false;
                //gameCamera.GetComponent<CameraFollow>().SceneSwitchUpdate(rightDirection);
                rightDirection = !rightDirection;
            }

            float percentage = currentLerpTime / lerpTime;
            gameCamera.transform.position = Vector3.Lerp(gameCamera.transform.position, endPos, percentage);
        }

        else
            currentLerpTime = 0;

        //if (startDelay)
        //{
        //    delayTimer += Time.deltaTime;
        //    if (delayTimer >= 3)
        //    {
        //        delayTimer = 0;
        //        gameCamera.GetComponent<CameraFollow>().cameraFollow = true;
        //        startDelay = false;
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (Index == 0)
                return;

            // If we want to head to the right side of the map
            if (rightDirection)
            {
                // Making the distance that we want to move
                endPos = new Vector3(gameCamera.transform.position.x + distance, gameCamera.transform.position.y, gameCamera.transform.position.z);
                // Start the lerp process
                startLerp = true;
            }

            else if (!rightDirection)
            {
                // Making the distance that we want to move
                endPos = new Vector3(gameCamera.transform.position.x - distance, gameCamera.transform.position.y, gameCamera.transform.position.z);
                // Start the lerp process
                startLerp = true;
            }

            //gameCamera.GetComponent<CameraFollow>().cameraFollow = false;
            //startDelay = true;
        }
    }
}
