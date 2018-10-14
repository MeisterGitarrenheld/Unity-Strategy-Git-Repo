using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuilding : Building, HitInterface
{
	private List<Unit.UnitType> buildOrder = new List<Unit.UnitType> ();
	private float currentBuildingTime = 0;
	public float factorySpeed;
	public GameObject[] listOfUnits;
	private Unit.UnitType currentJob = Unit.UnitType.NONE;
	public float[] buildTimes;

    public void Hit(int damage)
    {
		Health -= damage;
    }

    public List<Unit.UnitType> getBO()
    {
        return new List<Unit.UnitType>(buildOrder);
    }

    // Update is called once per frame
    override
	public void updateBuilding () {
		if (Health <= 0) {
			//TODO GameEnd!

		}
		if (currentJob.Equals(Unit.UnitType.NONE) && buildOrder.Count == 0) {
			return;
		}
		currentBuildingTime -= factorySpeed * Time.deltaTime;
		if (currentBuildingTime <= 0) {

			if ((int)currentJob != 3) {
				GameObject obj = Instantiate (listOfUnits [(int)currentJob], this.transform.position, this.transform.rotation);
                if(target != null)
                    obj.GetComponent<Unit>().setTarget(target);
				obj.GetComponent<Unit> ().setOwner (owner);
				GameMaster.Instance.RegisterInteractable(obj.transform, owner);
				currentJob = 0;
			}
			if (buildOrder.Count != 0) {
				currentJob = buildOrder [0];
				buildOrder.RemoveAt (0);
				currentBuildingTime = buildTimes [(int)currentJob];
			} else {
				currentJob = Unit.UnitType.NONE;
			}
		} 
	}
	public void discardUnit(Unit.UnitType type){
		for (int i = buildOrder.Count - 1; i >= 0; i--) {
			if (buildOrder [i].Equals (type)) {
				buildOrder.RemoveAt (i);
				return;
			}
		}
	}
	public void addToList(Unit.UnitType type) {
		GameMaster gm = GameMaster.Instance;
		User user = gm.Players [owner];
		if (user.DecreaseResources (listOfUnits [(int)type].GetComponent<Unit> ().cost)) {			
			buildOrder.Add (type);
		}
	}

	override
	public void Activate (UserInteraction interactor)
	{
		base.Activate (interactor);
		GameMaster gm = GameMaster.Instance;
		User user = gm.Players [owner];
		user.ui.showMainBuildingMenu ();
	}
	override
	public void Deactivate(UserInteraction interactor)
	{
		base.Deactivate (interactor);
		GameMaster gm = GameMaster.Instance;
		User user = gm.Players [owner];
		user.ui.showNoneMenu ();
	}
}
