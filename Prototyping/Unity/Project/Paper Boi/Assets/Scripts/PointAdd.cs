using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAdd : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerManager>().points += 1;
            Destroy(this.gameObject);
        }
    }
}
