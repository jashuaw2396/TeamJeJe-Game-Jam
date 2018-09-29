using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorCollider : MonoBehaviour
{
    // Player object
    private GameObject player;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check to see if we're hitting the floor
        if (collision.gameObject.tag == "Floor")
            player.GetComponent<PlayerInput>().m_grounded = true;
    }
}
