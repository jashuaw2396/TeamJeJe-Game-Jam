using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public bool open = false;
    public int m_doorNumber = 0;
    private bool wasOpen = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (Time.timeScale > 0)
        {

            if (open)
            {
                if (!wasOpen)
                {
                    OpenDoor();
                    wasOpen = true;
                }
            }
            else
            {
                if (wasOpen)
                {
                    OpenDoor();
                    wasOpen = false;
                }
            }
        }
	}

    void OpenDoor()
    {
        if (open)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
