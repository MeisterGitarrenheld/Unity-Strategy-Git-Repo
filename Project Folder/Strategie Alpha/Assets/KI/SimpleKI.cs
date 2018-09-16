﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleKI : MonoBehaviour {

    private GameMaster gm;
    private User ownUser;

    public GameObject MainBuilding;
    public GameObject FactoryBuilding;

	// Use this for initialization
	void Start () {
        gm = GameMaster.Instance;
        ownUser = GetComponent<ComputerUser>();
	}
	
	// Update is called once per frame
	void Update () {

        List<Transform> ownInteractable = gm.PlayerInteractable[ownUser.PlayerNum];
        List<MainBuilding> mainBuildings = new List<MainBuilding>();
        List<FactoryBuilding> factoryBuildings = new List<FactoryBuilding>();
        List<CollectorUnit> colUnits = new List<CollectorUnit>();
        List<Unit> attackUnits = new List<Unit>();
        bool buildFactory = false;
        bool buildMain = false;
        foreach (Transform t in ownInteractable)
        {
            if (t.GetComponent<CollectorUnit>() != null)
            {
                if (t.GetComponent<CollectorUnit>().toBuild != null)
                {
                    buildFactory = t.GetComponent<CollectorUnit>().toBuild.GetComponent<FactoryBuilding>() != null;
                    buildMain = t.GetComponent<CollectorUnit>().toBuild.GetComponent<MainBuilding>() != null;
                }
                colUnits.Add(t.GetComponent<CollectorUnit>());
            }
            else if (t.GetComponent<Unit>() != null && t.GetComponent<Unit>().Type != Unit.UnitType.COLLECTOR_UNIT)
                attackUnits.Add(t.GetComponent<Unit>());
            else if (t.GetComponent<MainBuilding>() != null)
                mainBuildings.Add(t.GetComponent<MainBuilding>());
            else if (t.GetComponent<FactoryBuilding>() != null)
                factoryBuildings.Add(t.GetComponent<FactoryBuilding>());

        }

        if(colUnits.Count < 3 && ownUser.Resources > 20 && mainBuildings.Count > 0)
        {
            mainBuildings[0].addToList(Unit.UnitType.COLLECTOR_UNIT);
        }

        if (!buildFactory && colUnits.Count > 0 && ownUser.Resources > 300 && factoryBuildings.Count <= 0)
        {
            GameObject building = Instantiate(FactoryBuilding, colUnits[0].transform.position + Vector3.up * 3, colUnits[0].transform.rotation);
            building.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;
            building.GetComponent<Building>().setOwner(ownUser.PlayerNum);
            gm.RegisterInteractable(building.transform, ownUser.PlayerNum);
            colUnits[0].GetComponent<CollectorUnit>().StopBuild();
            colUnits[0].GetComponent<Interactable>().setTarget(new WalkType(building.transform.position, WType.Build));
            colUnits[0].GetComponent<CollectorUnit>().SetBuildBuilding(building);
        }
        
        foreach(CollectorUnit cu in colUnits)
        {
            if(cu.target == null)
            {
                cu.setTarget(new WalkType(GameObject.Find("ResourceField").transform.GetChild(0).position, WType.Collect));
            }
        }

	}

    void SendCollect(Unit unit)
    {

    }

    void BuildUnit(Building build, Unit.UnitType type)
    {

    }

}
