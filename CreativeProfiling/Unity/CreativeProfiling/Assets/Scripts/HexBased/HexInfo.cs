using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexInfo : MonoBehaviour {

    public List<GameObject> neighbourTiles = new List<GameObject>();

    public bool canPass;
    public int team;
    public int movementCost;
    public int rayPos = 30;
    public string tileType;
    public string tileInfo;
    public Vector3 rayCast;

    public Color BaseColor;
    public Renderer rend;

    public MoveUnit mu;
    public UIManager ui;
    public GameObject unit;

    void Start()
    {
        ui = GameObject.FindWithTag("Canvas").GetComponent<UIManager>();
        mu = GameObject.FindWithTag("Canvas").GetComponent<MoveUnit>();
        rend = GetComponent<Renderer>();
        BaseColor = rend.material.color;
        RaycastHit hit;
        for(int i = 0; i < 6; i++)
        {
            rayCast = Quaternion.AngleAxis(rayPos, transform.forward) * transform.up;
            if (Physics.Raycast(transform.position, rayCast, out hit))
            {
                neighbourTiles.Add(hit.collider.gameObject);
            }
            rayPos += 60;
        }
    }

    void OnMouseEnter()
    {
        rend.material.color += new Color32(40,0,0,0);
    }

    void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ui.TilePopup(this);
            mu.movingUnit = null;
        }

        if(Input.GetButtonDown("Fire2"))
        {
            if(mu.movingUnit != null && unit == null)
            {
                ProcesUnit(mu.movingUnit);
            }
        }
    }

    void OnMouseExit()
    {
        rend.material.color = BaseColor;
    }


    //can move vraagen en de movement zelf voor bestaande units en asigned var's aan nieuwe
    public void ProcesUnit(GameObject newUnit)
    {
        print("Works");
        if(mu.movingUnit != null)
        {
            print("works1");
            if(unit == null)
            {
                print("works2");
                for (int i = 0; i < neighbourTiles.Count; i++)
                {
                    print("works3");
                    if (neighbourTiles[i] == mu.movingUnit.GetComponent<UnitInfo>().tile)
                    {
                        print("Works4");
                        if(canPass == true)
                        {
                            print("Works5");
                            if(mu.movingUnit.GetComponent<UnitInfo>().toMove >= movementCost)
                            {
                                print("Works6");
                                if (mu.movingUnit.GetComponent<UnitInfo>().tile != null || mu.movingUnit.GetComponent<UnitInfo>().tile != this.gameObject)
                                {
                                    mu.movingUnit.GetComponent<UnitInfo>().tile.GetComponent<HexInfo>().unit = null;
                                    mu.movingUnit.GetComponent<UnitInfo>().tile = null;
                                }
                                unit = newUnit;
                                unit.GetComponent<UnitInfo>().tile = this.gameObject;
                                unit.transform.position = this.gameObject.transform.position;
                                mu.movingUnit.GetComponent<UnitInfo>().toMove -= movementCost;
                                ui.UpdateMovementLeft();
                            }
                        } 
                    }
                }
            } 
        }
        else
        {
            unit = newUnit;
            unit.GetComponent<UnitInfo>().tile = this.gameObject;
        }
    }
}
