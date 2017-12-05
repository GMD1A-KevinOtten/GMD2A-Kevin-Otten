using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject target;
    public Recources rc;
    public Minerals harvestRC;

    public enum States
    {
        Idle,
        Search,
        Harvest
    }

    public States states;

    void Update()
    {
        if(states == States.Idle && rc.minerals < rc.minMinerals)
        {
            states = States.Search;
        }

        if(states == States.Harvest && rc.minerals >= rc.minMinerals || states == States.Search && rc.minerals >= rc.minMinerals)
        {
            states = States.Idle;
        }

        if(states == States.Search)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, 0.2f);
        }

        if(states == States.Harvest && harvestRC.mineralsLeft == 0)
        {
            states = States.Search;
        }

        if(states == States.Harvest && rc.minerals == rc.mineralCap || states == States.Search && rc.minerals == rc.mineralCap)
        {
            states = States.Idle;
        }

    }

    public void HarvestTrigger()
    {
        if(rc.minerals != rc.mineralCap)
        {
            StartCoroutine(Harvesting(1));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(states == States.Search && collision.gameObject.tag == "Minerals")
        {
            states = States.Harvest;
            harvestRC = collision.gameObject.GetComponent<Minerals>();
            HarvestTrigger();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (states == States.Harvest)
        {
            states = States.Search;
        }
    }

    public IEnumerator Harvesting(float time)
    {
        yield return new WaitForSeconds(time);
        harvestRC.mineralsLeft -= 100;
        rc.minerals += 100;
        if(harvestRC.mineralsLeft > 0 && rc.minerals <= rc.mineralCap)
        {
            HarvestTrigger();
        }
    }
}
