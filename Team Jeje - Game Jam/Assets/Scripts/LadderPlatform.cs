using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPlatform : MonoBehaviour
{
    PlatformEffector2D effector;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            if (Input.GetKey(KeyCode.W))
                effector.rotationalOffset = 0;
            if (Input.GetKey(KeyCode.S))
                effector.rotationalOffset = 180;
        }

    }
}
