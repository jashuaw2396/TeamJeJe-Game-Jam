using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    public bool on = false;
    private bool wasOn = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0)
        {
            if (on)
            {
                if (!wasOn)
                {
                    ChangeStatePlatform();
                    wasOn = true;
                }
            }
            else
            {
                if (wasOn)
                {
                    ChangeStatePlatform();
                    wasOn = false;
                }
            }
        }
    }

    void ChangeStatePlatform()
    {
        if (on)
        {
            BoxCollider2D[] coliders = GetComponents<BoxCollider2D>();

            for(int i=0; i < coliders.Length;++i)
            {
                coliders[i].enabled = false;
            }
        }
        else
        {
            BoxCollider2D[] coliders = GetComponents<BoxCollider2D>();

            for (int i = 0; i < coliders.Length; ++i)
            {
                coliders[i].enabled = true;
            }
        }
    }
}
