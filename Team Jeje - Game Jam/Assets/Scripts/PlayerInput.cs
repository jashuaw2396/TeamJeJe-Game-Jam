using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Player Rigid body
    [HideInInspector]
    public Rigidbody2D rb2d;
    // Movable object when pushing/pulling
    GameObject movableObject;

    // Player Bools
    public bool m_isAlive = true;
    public bool m_grounded = true;
    public bool m_running = false;

    public bool m_adult = true;
    public bool m_movingObject = false;

    // Player stats
    public int m_jumpHeight = 0;
    public int m_walkSpeed = 0;
    public int m_runningSpeed = 0;


	// Use this for initialization
	void Start ()
    {
        // Getting the component of the rigid body
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Make sure we're not dead
        if (m_isAlive)
        {
            // Running button
            if (Input.GetKeyDown(KeyCode.LeftShift) && !m_running)
                m_running = true;
            else if (Input.GetKeyUp(KeyCode.LeftShift) && m_running)
                m_running = false;

            // Moving button
            if (Input.GetKeyDown(KeyCode.Mouse1) && !m_movingObject)
                m_movingObject = true;
            else if (Input.GetKeyUp(KeyCode.Mouse1) && m_movingObject)
            {
                m_movingObject = false;
                if (movableObject != null)
                {
                    movableObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    movableObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    movableObject.transform.SetParent(null);
                    movableObject = null;
                }
            }

            // Jump check
            if (Input.GetButtonDown("Jump") && m_grounded && !m_movingObject)
            {
                // Allow the player to jump
                m_grounded = false;
                rb2d.velocity = new Vector2(rb2d.velocity.x, m_jumpHeight);
            }
        }
	}

    // Physics update
    private void FixedUpdate()
    {
        if (m_isAlive)
        {
            // Only gets called whenever the player isn't staying still
            if (Input.GetAxis("Horizontal") != 0 && (Input.GetKeyDown(KeyCode.A) != true && Input.GetKeyDown(KeyCode.D) != true))
            {
                // Getting the input from the player
                float moveHorizontal = Input.GetAxis("Horizontal");

                // If we're walking
                if (!m_running)
                {
                    // adding velocity
                    rb2d.velocity = new Vector2(moveHorizontal * m_walkSpeed, rb2d.velocity.y);
                    // now we're moving our object as well
                    if (movableObject != null)
                        movableObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveHorizontal * m_walkSpeed, rb2d.velocity.y);
                }

                // esle if we're running
                else if (m_running)
                {
                    // adding velocity
                    rb2d.velocity = new Vector2(moveHorizontal * m_runningSpeed, rb2d.velocity.y);
                    // now we're moving our object as well
                    if (movableObject != null)
                        movableObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveHorizontal * m_runningSpeed, rb2d.velocity.y);
                }
            }
        }
    }

    // Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // We check to see if we're colliding with a movabe object
        if (m_movingObject && collision.gameObject.tag == "MovableObject" && movableObject == null)
        {
            movableObject = collision.gameObject;
            movableObject.GetComponent<Rigidbody2D>().isKinematic = false;
            movableObject.transform.SetParent(GetComponent<Transform>());
        }
    }
}
