using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntObjManager : MonoBehaviour {

    //array die gevuld word met alle variaties van ingameobject
    InGameObjects[] allIntObjects;
    public static List<GameObject> foodObjects = new List<GameObject>();
    public static List<GameObject> resourceObjects = new List<GameObject>();

    private void Start()
    {   
        //vult array met alle objects van type InGameObjects
        allIntObjects = (InGameObjects[])GameObject.FindObjectsOfType(typeof(InGameObjects));
        //array word opgedeeld door te checken naar wat voor needs het fixed zoals de food hieronder en pakt dan game object van dat script en plakt het in de list
        foreach (InGameObjects ingameobject in allIntObjects)
        {
            //Gameobjects inplaats van classes ind de lists zodat het makelijker kan gebruikt worden om positie van het object te bepalen
            if (ingameobject.iO.food > 0)
            {
                foodObjects.Add(ingameobject.gameObject);
            }
            if(ingameobject.iO.rawResources > 0)
            {
                resourceObjects.Add(ingameobject.gameObject);
            }
        }

    }
}
