using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static bool gamePlaying;
    public GameObject player;
    public Vector3 spawnPos;
    UIManager ui;

    void Start()
    {
        ui = GameObject.FindObjectOfType<UIManager>();
        player = GameObject.FindWithTag("Player");
        spawnPos.x = Camera.main.GetComponent<CameraScript>().cameraXMin;
    }

    //GameWonOrOver
    public void Win(string message)
    {
        gamePlaying = false;
        ui.NewWinMessage(message);
        StartCoroutine(Reset(3));
    }

    public void Died(string message)
    {
        ui.NewMessage(message);
        player.transform.position = spawnPos;
    }

    //Resets Scene
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator Reset(float i)
    {
        yield return new WaitForSeconds(i);
        RestartScene();
    }
}
