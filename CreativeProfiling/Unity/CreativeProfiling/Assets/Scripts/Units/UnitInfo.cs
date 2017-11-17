using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour {

    public Vector3 baseScale;
    public MoveUnit mu;
    public UIManager ui;
    public GameObject tile;
    public string name;
    public string info;
    public int baseToMove;
    public int toMove;
    public int team;
    public int hp;
    public int dmg;

    private void OnEnable()
    {
        UIManager.TurnEnd += AddMovement;
    }

    private void OnDisable()
    {
        UIManager.TurnEnd -= AddMovement;
    }

    public virtual void Start()
    {
        mu = GameObject.FindWithTag("Canvas").GetComponent<MoveUnit>();
        ui = GameObject.FindWithTag("Canvas").GetComponent<UIManager>();
        baseScale = gameObject.transform.localScale;
    }

    void OnMouseEnter()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ui.UnitPopup(this);
            mu.movingUnit = this.gameObject;
        }
    }

    void OnMouseExit()
    {
        transform.localScale = baseScale;
    }

    public void AddMovement()
    {
        toMove = baseToMove;
    }
}
