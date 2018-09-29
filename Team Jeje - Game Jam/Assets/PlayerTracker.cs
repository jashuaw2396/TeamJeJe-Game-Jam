using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    // Adult player
    public GameObject adultPlayer;
    // Child Player
    public GameObject childPlayer;

    // Player Bools
    public bool m_isAlive = true;
    public bool m_grounded = true;
    public bool m_running = false;
    public bool m_adult = true;
    public bool m_movingObject = false;

    private void Update()
    {
        // alive check
        if (m_isAlive)
        {
            // Transition button
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PlayerTransition();
            }

            // Calling the update of the player that's being used
            if (m_adult)
                adultPlayer.GetComponent<PlayerInput>().PlayerUpdate();
            else if (!m_adult)
                childPlayer.GetComponent<PlayerInput>().PlayerUpdate();
        }
    }

    private void FixedUpdate()
    {
        // alive check
        if (m_isAlive)
        {
            // Calling the fixed update of the player that's being used
            if (m_adult)
                adultPlayer.GetComponent<PlayerInput>().PlayerFixedUpdate();
            else if (!m_adult)
                childPlayer.GetComponent<PlayerInput>().PlayerFixedUpdate();
        }
    }

    public void PlayerTransition()
    {
        // if it's the adult player, we go to child
        if (m_adult && m_isAlive)
        {
            // Setting the child playable
            m_adult = false;
            childPlayer.transform.position = adultPlayer.transform.position;
            childPlayer.GetComponent<Rigidbody2D>().isKinematic = false;

            // Setting the adult off
            // positioning
            adultPlayer.GetComponent<Rigidbody2D>().isKinematic = true;
            adultPlayer.transform.position = new Vector3(0, 3);
            adultPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        // else if it's the child, we go to adult
        else if (!m_adult && m_isAlive)
        {
            // Setting the adult playable
            m_adult = true;
            adultPlayer.transform.position = childPlayer.transform.position;
            adultPlayer.GetComponent<Rigidbody2D>().isKinematic = false;

            // Setting the child off
            // positioning
            childPlayer.GetComponent<Rigidbody2D>().isKinematic = true;
            childPlayer.transform.position = new Vector3(0, 3);
            childPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
