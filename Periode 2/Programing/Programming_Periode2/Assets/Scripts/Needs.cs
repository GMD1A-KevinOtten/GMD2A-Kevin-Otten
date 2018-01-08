using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needs : MonoBehaviour {
    private Enemy myWorker;

    public float Hapines;

    public float food;

    void Start()
    {
        myWorker = this.gameObject.GetComponent<Enemy>();
        TriggerLowerNeeds();
    }

    void Update()
    {
        CheckFood();
        CheckHapinnes();
    }
    public void CheckFood()
    {
        if (food > 100)
        {
            food = 100;
        }

        else if (food >= 75 && myWorker.states == Enemy.States.ReplenischNeeds)
        {
            myWorker.states = Enemy.States.Idle;
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

    public void CheckHapinnes()
    {
        if(Hapines > 100)
        {
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

    public void ReplenischMyNeeds(InGameObjects iob)
    {
        food += iob.iO.food;
    }

    public void TriggerLowerNeeds()
    {
        StartCoroutine(LowerNeeds());
    }

    public IEnumerator LowerNeeds()
    {
        yield return new WaitForSeconds(2);
        food -= 1;
        TriggerLowerNeeds();
    }
}
