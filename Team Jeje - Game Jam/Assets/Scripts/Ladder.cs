using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void Start()
    {
        //effector = GetComponent<PlatformEffector2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            GameObject.FindGameObjectWithTag("PlayerTracker").GetComponent<PlayerTracker>().m_canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            GameObject.FindGameObjectWithTag("PlayerTracker").GetComponent<PlayerTracker>().m_canClimb = false;
        }
    }
}
