using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBuilding : Building {

	private List<Unit.UnitType> buildOrder = new List<Unit.UnitType> ();
	private float currentBuildingTime = 0;
	public float factorySpeed;
	public GameObject[] listOfUnits;
	private Unit.UnitType currentJob = 0;
	public float[] buildTimes;

	public void buildUnit(Unit.UnitType type){
		buildOrder.Add (type);
	}
	public void discardUnit(Unit.UnitType type){
		for (int i = buildOrder.Count - 1; i >= 0; i--) {
			if (buildOrder [i].Equals (type)) {
				buildOrder.RemoveAt (i);
				return;
			}
		}
	}

	// Update is called once per frame
	override
	public void updateBuilding () {
		currentBuildingTime -= factorySpeed;
		if (currentBuildingTime <= 0) {			
			if (currentJob != 0) {
				Instantiate (listOfUnits [(int)currentJob], this.transform.position, this.transform.rotation);
				currentJob = 0;
			}
			if (buildOrder.Count != 0) {
				currentJob = buildOrder [0];
				buildOrder.Remove (0);
				currentBuildingTime = buildTimes [(int)currentJob];
			}
		} 
	}
}
