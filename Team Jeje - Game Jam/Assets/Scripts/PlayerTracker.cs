using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    // Adult player
    public GameObject adultPlayer;
    // Child Player
    public GameObject childPlayer;
    // Game camera
    CameraFollow gameCamera;

    // Player Bools
    public bool m_isAlive = true;
    public bool m_grounded = true;
    public bool m_running = false;
    public bool m_adult = true;
    public bool m_movingObject = false;
    public bool m_canTransition = true;
    public bool m_canClimb = false;

    // Respawn timer
    private float timer = 0.0f;
    public float maxRespawnTimer = 0.0f;
    bool startTimer = false;

    // Adult position that gets tracked
    [HideInInspector]
    public Vector3 m_lastPosition;
    // Checkpoint position
    [HideInInspector]
    public Vector3 m_checkpoint;

    private void Start()
    {
        gameCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }

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


        // Calling the respawn
        if (startTimer)
        {
            // We start the timer
            timer += Time.deltaTime;
            // If the timer is done
            if (timer >= maxRespawnTimer)
            {
                // We stop the timer
                startTimer = false;
                timer = 0;
                // Call Respawn
                Respawn();
            }
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
        if (m_canTransition)
        {
            // if it's the adult player, we go to child
            if (m_adult && m_isAlive)
            {
                // Setting the child playable
                m_adult = false;
                childPlayer.transform.position = adultPlayer.transform.position;
                childPlayer.GetComponent<Rigidbody2D>().isKinematic = false;
                gameCamera.target = childPlayer.transform;

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
                adultPlayer.transform.position = new Vector3(childPlayer.transform.position.x, -3);// childPlayer.transform.position;
                adultPlayer.GetComponent<Rigidbody2D>().isKinematic = false;
                gameCamera.target = adultPlayer.transform;

                // Setting the child off
                // positioning
                childPlayer.GetComponent<Rigidbody2D>().isKinematic = true;
                childPlayer.transform.position = new Vector3(0, 3);
                childPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
    }

    public void Death()
    {
        m_isAlive = false;
        adultPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        childPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        startTimer = true;
    }

    public void Respawn()
    {
        // Making the player alive
        m_isAlive = true;
        // Setting the adult playable
        m_adult = true;
        adultPlayer.transform.position = m_checkpoint;
        adultPlayer.GetComponent<Rigidbody2D>().isKinematic = false;

        // Setting the child off
        // positioning
        childPlayer.GetComponent<Rigidbody2D>().isKinematic = true;
        childPlayer.transform.position = new Vector3(0, 3);
        childPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
