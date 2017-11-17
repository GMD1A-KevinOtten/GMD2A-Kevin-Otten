using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBallMovement : MonoBehaviour {

    public int speed;
    public Vector3 movement;
    public GameManager gm;
    public string message;

    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    void FixedUpdate ()
    {
        movement.x = speed * Time.deltaTime; 
        transform.Translate(movement);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gm.Died(message);
            collision.gameObject.GetComponent<PlayerManager>().deaths += 1;
        }
        else
        {
            speed -= speed * 2;
            if(this.GetComponent<SpriteRenderer>().flipX == false)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}
