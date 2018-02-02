using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour {

    public GameObject target;
    public Recources rc;
    public InGameObjects harvestRC;
    public NavMeshAgent nMA;
    //int die naar een bepaalde need wijst check voor info in "Needs" script
    public int need;
    //Enum's die de State waar de worker inzit aangeeft
    public enum States
    {
        Idle,
        Search,
        Harvest,
        ReplenischNeeds
    }

    public States states;

    //functie die het replenishen van needs forceerd
    public void ForceNeeds()
    {
        states = States.ReplenischNeeds;
    }

    //functie die needs repleniched als de werker niet iets aa n het harvesten is
    public void MabyNeeds()
    {
        if(states != States.Harvest)
        {
            states = States.ReplenischNeeds;
        }
    }
    //update word aleen gebruikt voor debug prints en functies aan te roepen die als de update moeten runnen zodat het wat schooner blijft
    void Update()
    {
        UpdateStates();
    }

    //functie die de state van de werker checked en zo beslist wat te doen
    public void UpdateStates()
    {
        if (states == States.ReplenischNeeds)
        {
            if(need == 1 && IntObjManager.foodObjects.Count != 0)
            {
                target = IntObjManager.foodObjects[0];
                float dist = Mathf.Infinity;
                foreach (GameObject g in IntObjManager.foodObjects)
                {
                    Vector3 diff = g.transform.position - transform.position;
                    float curDust = diff.sqrMagnitude;
                    if (curDust < dist)
                    {
                        target = g;
                        dist = curDust;
                    }
                }
                nMA.destination = target.transform.position;
            }
            else
            {
                states = States.Idle;
            }
        }

        if (states == States.Idle && rc.resources < rc.minResources)
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

        if (states == States.Harvest && rc.resources >= rc.minResources || states == States.Search && rc.resources >= rc.minResources)
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
                nMA.destination = target.transform.position;
            }
        }

        if (states == States.Harvest && harvestRC.resourcesLeft == 0)
        {
            states = States.Search;
        }

        if (states == States.Harvest && rc.resources == rc.resourceCap || states == States.Search && rc.resources == rc.resourceCap)
        {
            states = States.Idle;
        }
    }

    //regelt het harvesten van resource objects en loopt de coroutine zolang de resource niet is depleted
    public void HarvestTrigger()
    {
        if(rc.resources != rc.resourceCap)
        {
            StartCoroutine(Harvesting(1));
        }
    }

    //Collisions met objecten worden hier bekeken en als het relefant is beantwoord
    void OnCollisionEnter(Collision collision)
    {
        if(states == States.Search && collision.gameObject.tag == "Minerals")
        {
            states = States.Harvest;
            harvestRC = collision.gameObject.GetComponent<InGameObjects>();
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

    //als een havestabele object verdwijnd door de worker word dit getriggerd
    void OnCollisionExit(Collision collision)
    {
        if (states == States.Harvest)
        {
            states = States.Search;
        }
    }

    //de code die de dichtsbezijnde recourse object om te harvesten
    public void ChangeTarget()
    {
        float dist = Mathf.Infinity;
        foreach (GameObject g in IntObjManager.resourceObjects)
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

    //de enum die de harvest regeld
    public IEnumerator Harvesting(float time)
    {
        yield return new WaitForSeconds(time);
        harvestRC.resourcesLeft -= 100;
        rc.resources += 100;
        if(harvestRC.resourcesLeft > 0 && rc.resources <= rc.resourceCap)
        {
            HarvestTrigger();
        }
        else if(harvestRC.resourcesLeft <= 0)
        {
            Destroy(harvestRC.gameObject);
        }
    }
}
