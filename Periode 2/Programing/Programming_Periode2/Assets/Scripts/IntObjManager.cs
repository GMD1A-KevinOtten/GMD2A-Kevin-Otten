using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntObjManager : MonoBehaviour {

    //array die gevuld word met alle variaties van ingameobject
    InGameObjects[] allIntObjects;
    public static List<GameObject> foodObjects = new List<GameObject>();

    private void Start()
    {   
        allIntObjects = (InGameObjects[])GameObject.FindObjectsOfType(typeof(InGameObjects));
        //array word opgedeeld door te checken naar wat voor needs het fixed zoals de food hieronder en pakt dan game object van dat script en plakt het in de list
        foreach (InGameObjects ingameobject in allIntObjects)
        {
            if(ingameobject.iO.food > 0)
            {
                foodObjects.Add(ingameobject.gameObject);
            }
        }

    }
}
