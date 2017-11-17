using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouDied : MonoBehaviour {

    GameManager gm;
    public string message;

    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gm.Died(message);
            collision.GetComponent<PlayerManager>().deaths += 1;
        }
    }
}
