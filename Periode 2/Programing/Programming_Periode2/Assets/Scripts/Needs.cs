using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needs : MonoBehaviour {
    private Worker myWorker;

    public float Hapines;

    public float food;

    void Start()
    {
        myWorker = this.gameObject.GetComponent<Worker>();
        TriggerLowerNeeds();
    }

    void Update()
    {
        CheckFood();
        CheckHapinnes();
    }

    //alle vragen om food heen
    public void CheckFood()
    {
        if (food > 100)
        {
            //als food teveel word gaat het automatish terug naar 100 (mag niet hoger dan 100)
            food = 100;
        }

        else if (food >= 75 && myWorker.states == Worker.States.ReplenischNeeds)
        {
            myWorker.states = Worker.States.Idle;
        }

        else if (food <= 25)
        {
            myWorker.need = 1;
            myWorker.ForceNeeds();
        }

        else if (food <= 75)
        {
            myWorker.MabyNeeds();
        }

        else if (food <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //alle vragen om Happines heen
    public void CheckHapinnes()
    {
        if(Hapines > 100)
        {
            //als Hapinnes teveel word gaat het automatish terug naar 100 (mag niet hoger dan 100)
            Hapines = 100;
        }

        else if(Hapines == -100)
        {
            Destroy(this.gameObject);
        }

        else if (Hapines <= -50)
        {
            myWorker.ForceNeeds();
        }

        else if (Hapines <= 75)
        {
            myWorker.MabyNeeds();
        }
        //als hapines is gelinked aan needs kan dit weer aan en die in food weer weg
        //else if (Hapines >= 75 && myWorker.states == Enemy.States.ReplenischNeeds)
        //{
        //    myWorker.states = Enemy.States.Idle;
        //}
    }

    //krijgt ingameobject en replenishes needs daarmee
    public void ReplenischMyNeeds(InGameObjects iob)
    {
        food += iob.iO.food;
    }

    //initial trigger voor het verlagen van needs wat steeds weer repeat
    public void TriggerLowerNeeds()
    {
        StartCoroutine(LowerNeeds());
    }

    //lowerd de needs
    public IEnumerator LowerNeeds()
    {
        yield return new WaitForSeconds(2);
        food -= 1;
        TriggerLowerNeeds();
    }
}
