using System.Collections;
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
                    buildFactory = buildFactory || t.GetComponent<CollectorUnit>().toBuild.GetComponent<FactoryBuilding>() != null;
                    buildMain = buildMain || t.GetComponent<CollectorUnit>().toBuild.GetComponent<MainBuilding>() != null;
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


        if(mainBuildings.Count <= 0 && !buildMain && ownUser.Resources > 1000 && colUnits.Count > 0)
        {
            GameObject building = Instantiate(MainBuilding, ownUser.transform.position, colUnits[0].transform.rotation);
            building.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;
            building.GetComponent<Building>().setOwner(ownUser.PlayerNum);
            gm.RegisterInteractable(building.transform, ownUser.PlayerNum);
            colUnits[0].GetComponent<CollectorUnit>().StopBuild();
            colUnits[0].GetComponent<Interactable>().setTarget(new WalkType(building.transform.position, WType.Build));
            colUnits[0].GetComponent<CollectorUnit>().SetBuildBuilding(building);
        }

        if (mainBuildings.Count > 0 && colUnits.Count < 3 && ownUser.Resources > 20 && mainBuildings.Count > 0)
        {
            int buildUnits = 0;
            foreach (Unit.UnitType uty in mainBuildings[0].getBO())
                if (uty == Unit.UnitType.COLLECTOR_UNIT)
                    buildUnits++;
            if (colUnits.Count + buildUnits < 5)
                mainBuildings[0].addToList(Unit.UnitType.COLLECTOR_UNIT);
        }

        if (mainBuildings.Count > 0 && !buildFactory && colUnits.Count > 0 && ownUser.Resources > 300 && factoryBuildings.Count <= 0)
        {
            GameObject building = Instantiate(FactoryBuilding, mainBuildings[0].transform.position + Vector3.right * 5 + Vector3.up * 3, colUnits[0].transform.rotation);
            building.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;
            building.GetComponent<Building>().setOwner(ownUser.PlayerNum);
            gm.RegisterInteractable(building.transform, ownUser.PlayerNum);
            colUnits[0].GetComponent<CollectorUnit>().StopBuild();
            colUnits[0].GetComponent<Interactable>().setTarget(new WalkType(building.transform.position, WType.Build));
            colUnits[0].GetComponent<CollectorUnit>().SetBuildBuilding(building);
        }

        if (factoryBuildings.Count > 0 && attackUnits.Count < 5)
        {
            factoryBuildings[0].setTarget(new WalkType(factoryBuildings[0].transform.position + Vector3.right * 3 + Vector3.forward * 5));
            factoryBuildings[0].addToList((Unit.UnitType)Random.Range(1, 3));
        }

        foreach(CollectorUnit cu in colUnits)
        {
            if (cu.target == null)
            {
                GameObject res = GameObject.Find("ResourceField Comp");
                if (res != null && res.transform.childCount > 0)
                    cu.setTarget(new WalkType(res.transform.GetChild(Random.Range(0, res.transform.childCount)).position, WType.Collect));
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
