using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    // Adult player
    public GameObject adultPlayer;
    // Child Player
    public GameObject childPlayer;

    private void Update()
    {
        // Transition button
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerTransition();
        }
    }

    public void PlayerTransition()
    {
        // if it's the adult player, we go to child
        if (adultPlayer.GetComponent<PlayerInput>().m_adult && adultPlayer.GetComponent<PlayerInput>().m_isAlive)
        {
            // Setting the child playable
            childPlayer.transform.position = adultPlayer.transform.position;
            childPlayer.GetComponent<PlayerInput>().m_isAlive = true;
            childPlayer.GetComponent<Rigidbody2D>().isKinematic = false;

            // Setting the adult off
            adultPlayer.GetComponent<PlayerInput>().m_isAlive = false;
            // positioning
            adultPlayer.GetComponent<Rigidbody2D>().isKinematic = true;
            adultPlayer.transform.position = new Vector3(0, 3);
            adultPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        // else if it's the child, we go to adult
        else if (!childPlayer.GetComponent<PlayerInput>().m_adult && childPlayer.GetComponent<PlayerInput>().m_isAlive)
        {
            // Setting the adult playable
            adultPlayer.transform.position = childPlayer.transform.position;
            adultPlayer.GetComponent<PlayerInput>().m_isAlive = true;
            adultPlayer.GetComponent<Rigidbody2D>().isKinematic = false;

            // Setting the child off
            childPlayer.GetComponent<PlayerInput>().m_isAlive = false;
            // positioning
            childPlayer.GetComponent<Rigidbody2D>().isKinematic = true;
            childPlayer.transform.position = new Vector3(0, 3);
            childPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
