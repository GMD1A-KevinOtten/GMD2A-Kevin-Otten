using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnit : MonoBehaviour {

    public GameObject movingUnit;

    void Update()
    {
        if(Input.GetButtonDown("Escape"))
        {
            movingUnit = null;
        }
    }
}
