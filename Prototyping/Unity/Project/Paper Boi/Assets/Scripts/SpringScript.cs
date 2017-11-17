using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringScript : MonoBehaviour {

    public float bounceForce = 10;
    public static bool jumpingOnSpring;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.rigidbody.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bounceForce);
            jumpingOnSpring = true;
        }
    }
}
