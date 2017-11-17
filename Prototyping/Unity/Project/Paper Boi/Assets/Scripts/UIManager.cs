using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public PlayerManager pm;

    //Timer
    public float timer;
    //UI Text's
    public Text uITimer;
    public Text message;
    public Text time;
    public Text pointsMessage;
    public Text points;
    public Text deaths;
    public Text deathsMessage;

    void Update ()
    {
		if(GameManager.gamePlaying == true)
        {
            timer += Time.deltaTime;
            uITimer.text = "Timer: " + timer.ToString("F2");
            points.text = "Points: " + pm.points;
            deaths.text = "Deaths: " + pm.deaths;
        }
	}

    //Message Text
    public void NewWinMessage(string messageText)
    {
        message.text = messageText;
        time.text = "Time Taken: " + timer.ToString("F2")+ " Seconds";
        pointsMessage.text = "Points: " + pm.points;
        deathsMessage.text = "Deaths: " + pm.deaths;
    }

    public void NewMessage(string messageText)
    {
        message.text = messageText;
        StartCoroutine(ResetUITimer(3));
    }

    //reset Message
    public void ResetUI()
    {
        message.text = "";
        time.text = "";
        pointsMessage.text = "";
    }

    public IEnumerator ResetUITimer(float time)
    {
        yield return new WaitForSeconds(time);
        ResetUI();
    }
}
