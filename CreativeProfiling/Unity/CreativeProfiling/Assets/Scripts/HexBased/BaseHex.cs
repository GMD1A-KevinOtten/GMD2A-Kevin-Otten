using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHex : MonoBehaviour {

    public UIManager ui;
    public Spawner sp;

    private void Start()
    {
        ui = GameObject.FindWithTag("Canvas").GetComponent<UIManager>();
        sp = GameObject.FindWithTag("Canvas").GetComponent<Spawner>();
    }

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ui.TurnOnBuyPannel();
            sp.spawnPos = this.gameObject;
        }
    }
}
