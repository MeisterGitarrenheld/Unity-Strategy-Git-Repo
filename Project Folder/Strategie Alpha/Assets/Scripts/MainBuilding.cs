using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuilding : Building, HitInterface
{
    public void Hit(int damage)
    {

    }

    // Update is called once per frame
    override
	public void updateBuilding () {
		if (Health <= 0) {
			//TODO GameEnd!

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
