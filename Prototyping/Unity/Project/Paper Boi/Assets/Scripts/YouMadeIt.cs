using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouMadeIt : MonoBehaviour {

    GameManager gm;
    public string message;

	void Start ()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        GameManager.gamePlaying = true;
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gm.Win(message);
        }
    }
}
