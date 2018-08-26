using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorUnit : Unit {

	void buildBuilding(Building building){

	}

	// Update is called once per frame
	override
	public void updateUnit() {


	}
	override
	public void attack(){

	}
	override
	public void Activate (UserInteraction interactor)
	{
		base.Activate (interactor);
		GameMaster gm = GameMaster.Instance;
		User user = gm.Players [owner];
		user.ui.showCollectorMenu ();
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
