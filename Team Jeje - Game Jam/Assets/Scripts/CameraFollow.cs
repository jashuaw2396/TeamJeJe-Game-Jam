using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // player that the camera will be following
    public Transform target;

    //Private variable to store the offset distance between the player and camera
    private Vector3 velocity = Vector3.zero;

    public float smoothTime = .15f;

    public float minY = 0;
    public bool minYEnabled = false;
    public bool cameraFollow = true;

    // LateUpdate is called after Update each frame
    void FixedUpdate()
    {
        if (!cameraFollow)
            return;

        Vector3 targetPos = target.position;

        if (minYEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, minY, target.position.y);

        //if (minXEnabled && maxXEnabled)
        //    // clamping the x
        //    targetPos.x = Mathf.Clamp(target.position.x, minX, maxX);
        //else if (minXEnabled)
        //    // clamping the x
        //    targetPos.x = Mathf.Clamp(target.position.x, minX, target.position.x);
        //else if (maxXEnabled)
        //    // clamping the x
        //    targetPos.x = Mathf.Clamp(target.position.x, target.position.x, maxX);

        // allign the camera and the target's z
        targetPos.z = transform.position.z;

        // using smooth damp we will gradually change the camera transform pos to the target position based on the camera's transform velocity & our smooth time
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
