using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameObjects : MonoBehaviour {

    public InteractableObject iO;
    public int resourcesLeft;


    void Start()
    {
        resourcesLeft = iO.rawResources;   
    }

    public void ReplenishNeeds()
    {
        Destroy (gameObject);
    }
}
