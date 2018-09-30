using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // player that the camera will be following
    public Transform target;

    //Private variable to store the offset distance between the player and camera
    private Vector3 velocity = Vector3.zero;

    public float smoothTime = .15f;

    // List of positions
    public List<SceneSwitcher> positionList;

    // index used
    private int firstIndex = 0, secondIndex = 0;
    public float minX = 0, maxX = 0, minY = 0;
    public bool minXEnabled = false, maxXEnabled = false, minYEnabled = false;

    public bool cameraFollow = true;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("SceneSwitcher"))
            positionList.Add(obj.GetComponent<SceneSwitcher>());

        positionList.Sort(SortByScore);

        firstIndex = 0;
        secondIndex = 1;
        minX = positionList[firstIndex].transform.position.x + 5.0f;
        maxX = positionList[secondIndex].transform.position.x - 4.2f;
    }

    // LateUpdate is called after Update each frame
    void FixedUpdate()
    {
        if (!cameraFollow)
            return;

        Vector3 targetPos = target.position;

        if (minYEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, minY, target.position.y);

        if (minXEnabled && maxXEnabled)
            // clamping the x
            targetPos.x = Mathf.Clamp(target.position.x, minX, maxX);
        else if (minXEnabled)
            // clamping the x
            targetPos.x = Mathf.Clamp(target.position.x, minX, target.position.x);
        else if (maxXEnabled)
            // clamping the x
            targetPos.x = Mathf.Clamp(target.position.x, target.position.x, maxX);

        // allign the camera and the target's z
        targetPos.z = transform.position.z;

        // using smooth damp we will gradually change the camera transform pos to the target position based on the camera's transform velocity & our smooth time
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }

    // Scene switcher list Updater
    public void SceneSwitchUpdate(bool _rightDirection)
    {
        if (_rightDirection)
        {
            if (secondIndex + 1 > positionList.Count)
                return;

            firstIndex = secondIndex;
            secondIndex++;
            minX = positionList[firstIndex].transform.position.x;// + 5.0f;
            maxX = positionList[secondIndex].transform.position.x;// - 4.2f;
        }
        
        else if (!_rightDirection)
        {
            if (firstIndex - 1 < 0)
                return;

            secondIndex = firstIndex;
            firstIndex--;
            minX = positionList[firstIndex].transform.position.x;// + 5.0f;
            maxX = positionList[secondIndex].transform.position.x;// - 4.2f;
        }
    }

    // Sort
    static int SortByScore(SceneSwitcher p1, SceneSwitcher p2)
    {
        return p1.Index.CompareTo(p2.Index);
    }
}
