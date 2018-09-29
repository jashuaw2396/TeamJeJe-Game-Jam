using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // We need access to the player stats
    PlayerTracker playerStats;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("PlayerTrack").GetComponent<PlayerTracker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player hit's a checkpoint, we save the checkpoint
        //                              player
        if (collision.gameObject.layer == 9)
        {
            playerStats.m_checkpoint = this.transform.position;
        }
    }
}
