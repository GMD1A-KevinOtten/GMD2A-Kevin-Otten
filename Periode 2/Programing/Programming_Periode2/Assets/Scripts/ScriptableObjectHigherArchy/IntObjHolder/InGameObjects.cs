using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameObjects : MonoBehaviour {
    //dit script is MissingReferenceException holder voor interactabelObjects dus zowel harvastables als need objects
    public InteractableObject iO;
    public int resourcesLeft;


    void Start()
    {
        //decide hoeveel recourses er over zijn voor als harvest hierop word getriggerd
        resourcesLeft = iO.rawResources;   
    }

    //waneer replenische needs word getriggerd word aangezien dat instant word geregeld het object destroyed
    public void ReplenishNeeds()
    {
        Destroy (gameObject);
    }
}
