using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public Vector3 horizontalSpeed;
    public float horizontal;
    public bool downForce;
    public float gravity;
    public float hSpeed;
    public float jumpInput;
    public float airSpeed;
    public float backwardsAirSpeed;
    public float groundSpeed;
    public float jumpveloc;
    public bool onGround;
    public bool jumped;

    void Start()
    {
        GameManager.gamePlaying = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        if(GameManager.gamePlaying == true)
        {
            horizontalSpeed.x = Input.GetAxis("Horizontal") * hSpeed * Time.deltaTime;
            this.transform.Translate(horizontalSpeed);
            horizontal = Input.GetAxis("Horizontal");

            if (downForce == true)
            {
                this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, gravity * Time.deltaTime), ForceMode2D.Impulse);
            }

            //Flip sprite naar kan van movement
            if (horizontal < 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (horizontal > 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }

            // Jumping regels
            if (Input.GetButtonDown("Space") && onGround == true && jumped == false)
            {
                onGround = false;
                jumped = true;
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpveloc);
                jumpInput = Input.GetAxis("Horizontal");
            }

            if (onGround == false && jumpInput >= 0 && horizontal <= 0)
            {
                hSpeed = backwardsAirSpeed;
            }
            else if (onGround == false && horizontal >= 0)
            {
                hSpeed = airSpeed;
            }

            if (onGround == false && jumpInput <= 0 && horizontal >= 0)
            {
                hSpeed = backwardsAirSpeed;
            }
            else if (onGround == false && jumpInput <= 0 && horizontal <= 0)
            {
                hSpeed = airSpeed;
            }

            if (onGround == true)
            {
                hSpeed = groundSpeed;
                downForce = false;
            }
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            onGround = true;
            SpringScript.jumpingOnSpring = false;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            onGround = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            jumped = false;
        }
    }
}
