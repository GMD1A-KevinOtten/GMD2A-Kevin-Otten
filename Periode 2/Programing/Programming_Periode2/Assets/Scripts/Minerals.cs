using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minerals : MonoBehaviour {

    public int mineralsLeft;

    void Update()
    {
        if(mineralsLeft <= 0)
        {
            Destroy(gameObject);
        }
    }
}
