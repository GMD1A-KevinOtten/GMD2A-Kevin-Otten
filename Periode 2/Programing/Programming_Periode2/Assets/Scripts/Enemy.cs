using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public GameObject target;
    public Recources rc;
    public Minerals harvestRC;
    public NavMeshAgent do7;
    //int die naar een bepaalde need wijst check voor info in needs script
    public int need;

    public enum States
    {
        Idle,
        Search,
        Harvest,
        ReplenischNeeds
    }

    public States states;

    public void ForceNeeds()
    {
        states = States.ReplenischNeeds;
    }

    public void MabyNeeds()
    {
        if(states != States.Harvest)
        {
            states = States.ReplenischNeeds;
        }
    }

    void Update()
    {
        UpdateStates();
    }

    public void UpdateStates()
    {
        if (states == States.ReplenischNeeds)
        {
            if(need == 1 && IntObjManager.foodObjects.Count != 0)
            {
                target = IntObjManager.foodObjects[0];
                float closestFoodItem = Vector3.Distance(this.transform.position, IntObjManager.foodObjects[0].transform.position);
                for (int i = 0; i < IntObjManager.foodObjects.Count; i++)
                {
                    float foodItem = Vector3.Distance(this.transform.position, IntObjManager.foodObjects[i].transform.position);
                    if(foodItem < closestFoodItem)
                    {
                        closestFoodItem = foodItem;
                        target = IntObjManager.foodObjects[i];
                    }
                }
                do7.destination = target.transform.position;
            }
        }

        if (states == States.Idle && rc.minerals < rc.minMinerals)
        {
            if (target != null)
            {
                states = States.Search;
            }
            else
            {
                ChangeTarget();
            }
        }

        if (states == States.Harvest && rc.minerals >= rc.minMinerals || states == States.Search && rc.minerals >= rc.minMinerals)
        {
            states = States.Idle;
        }

        if (states == States.Search)
        {
            ChangeTarget();
            if (target == null)
            {
                states = States.Idle;
            }
            else
            {
                do7.destination = target.transform.position;
            }
        }

        if (states == States.Harvest && harvestRC.mineralsLeft == 0)
        {
            states = States.Search;
        }

        if (states == States.Harvest && rc.minerals == rc.mineralCap || states == States.Search && rc.minerals == rc.mineralCap)
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
        if(states == States.ReplenischNeeds && collision.gameObject.tag == "NeedsObject")
        {
            print("replenisching needs");
            this.GetComponent<Needs>().ReplenischMyNeeds(collision.gameObject.GetComponent<InGameObjects>());
            IntObjManager.foodObjects.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (states == States.Harvest)
        {
            states = States.Search;
        }
    }

    public void ChangeTarget()
    {
        GameObject[] allResources = GameObject.FindGameObjectsWithTag("Minerals");
        float dist = Mathf.Infinity;
        foreach (GameObject g in allResources)
        {
            Vector3 diff = g.transform.position - transform.position;
            float curDust = diff.sqrMagnitude;
            if (curDust < dist)
            {
                target = g;
                dist = curDust;
            }
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
