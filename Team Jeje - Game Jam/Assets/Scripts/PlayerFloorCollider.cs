using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorCollider : MonoBehaviour
{
    // Player object
    private GameObject playerStats;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerTracker");
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check to see if we're hitting the floor
                                      // Jump
        if (collision.gameObject.layer == 8)
        {
            playerStats.GetComponent<PlayerTracker>().m_grounded = true;
            playerStats.GetComponent<PlayerTracker>().m_running = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerStats.GetComponent<PlayerTracker>().m_grounded = false;
    }
}
