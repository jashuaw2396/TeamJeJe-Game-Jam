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

    // "Stats" class
    [HideInInspector]
    public PlayerTracker m_playerStats;

    // Player stats
    public float yveloc;
    public float m_jumpVelocity = 0;
    public float m_bounceJumpVelocity = 0;
    public float m_fallMultiplier = 2.5f;
    public float m_lowJumpMultiplier = 2f;
    public float m_walkSpeed = 0;
    public float m_runningSpeed = 0;
    public float m_climbSpeed = 0;
    private float m_tempWalkSpeed = 0;
    private float m_tempRunningSpeed = 0;
    private bool m_onBouncer = false;


    // Use this for initialization
    void Start()
    {
        // Getting the component of the rigid body
        rb2d = GetComponent<Rigidbody2D>();

        // Getting the stats class
        m_playerStats = GameObject.FindGameObjectWithTag("PlayerTracker").GetComponent<PlayerTracker>();
    }

    // Update is called once per frame
    public void PlayerUpdate()
    {
        // Jump check
        if (Input.GetButtonDown("Jump") && m_playerStats.m_grounded && !m_playerStats.m_movingObject)
        {
            // Allow the player to jump
            if (m_onBouncer)
                rb2d.velocity = new Vector2(0, m_bounceJumpVelocity);
            else
                rb2d.velocity = new Vector2(0, m_jumpVelocity);// Vector2.up * m_jumpVelocity;
            m_playerStats.m_grounded = false;
        }
        
        // When we're falling after letting go, apply down force
        if (rb2d.velocity.y < 0)
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (m_fallMultiplier - 1) * Time.deltaTime;

        // When we press the jump button quickly, we apply the low jump force instead
        else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump"))
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (m_lowJumpMultiplier - 1) * Time.deltaTime;


        // Running button
        if (Input.GetKeyDown(KeyCode.LeftShift) && !m_playerStats.m_running)
            m_playerStats.m_running = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift) && m_playerStats.m_running)
            m_playerStats.m_running = false;

        // Moving button
        if (Input.GetKeyDown(KeyCode.Mouse1) && !m_playerStats.m_movingObject)
            m_playerStats.m_movingObject = true;
        else if (Input.GetKeyUp(KeyCode.Mouse1) && m_playerStats.m_movingObject)
        {
            m_playerStats.m_movingObject = false;
            if (movableObject != null)
            {
                movableObject.GetComponent<Rigidbody2D>().isKinematic = true;
                movableObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                movableObject.transform.SetParent(null);
                movableObject = null;

                m_walkSpeed = m_tempWalkSpeed;
                m_runningSpeed = m_tempRunningSpeed;
            }
        }
    }

    // Physics update
    public void PlayerFixedUpdate()
    {
        // Only gets called whenever the player isn't staying still
        if (Input.GetAxis("Horizontal") != 0 && (Input.GetKeyDown(KeyCode.A) != true && Input.GetKeyDown(KeyCode.D) != true))
        {
            // Getting the input from the player
            float moveHorizontal = Input.GetAxis("Horizontal");

            // If we're walking
            if (!m_playerStats.m_running)
            {
                // adding velocity
                rb2d.velocity = new Vector2(moveHorizontal * m_walkSpeed, rb2d.velocity.y);
                // now we're moving our object as well
                if (movableObject != null)
                    movableObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveHorizontal * m_walkSpeed, rb2d.velocity.y);
            }

            // esle if we're running
            else if (m_playerStats.m_running)
            {
                // adding velocity
                rb2d.velocity = new Vector2(moveHorizontal * m_runningSpeed, rb2d.velocity.y);
                // now we're moving our object as well
                if (movableObject != null)
                    movableObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveHorizontal * m_runningSpeed, rb2d.velocity.y);
            }
        }
        // Climbing ladders
        if (m_playerStats.m_canClimb)
        {
            // Stopping gravity
            rb2d.gravityScale = 0.1f;
            // Getting the input from the player
            float moveVertical = Input.GetAxis("Vertical");
            // adding velocity
            rb2d.velocity = new Vector2(rb2d.velocity.x, moveVertical * m_climbSpeed);
        }

        else
            rb2d.gravityScale = 1;
    }


    // Collision
    private void OnTriggerStay2D(Collider2D collision)
    {
        // We check to see if we're colliding with a movabe object
        if (m_playerStats.m_movingObject && collision.gameObject.tag == "MovableObject" && movableObject == null)
        {
            movableObject = collision.gameObject;
            movableObject.GetComponent<Rigidbody2D>().isKinematic = false;
            movableObject.transform.SetParent(GetComponent<Transform>());

            m_tempWalkSpeed = m_walkSpeed;
            m_tempRunningSpeed = m_runningSpeed;
            float tempDecimal = movableObject.GetComponent<MovableBox>().m_playerMovementSpeedDecimal;
            m_walkSpeed = (m_walkSpeed * tempDecimal);
            m_runningSpeed = (m_runningSpeed * tempDecimal);
        }

        if (collision.gameObject.tag == "Bouncer")
        {
            m_onBouncer = true;
            yveloc = rb2d.velocity.y;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bouncer")
        {
            m_onBouncer = false;
            if (yveloc > m_bounceJumpVelocity)
                
                rb2d.velocity = new Vector2(rb2d.velocity.x, m_bounceJumpVelocity);
        }
    }
}
