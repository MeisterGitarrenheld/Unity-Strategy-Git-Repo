using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorUnit : Unit, HitInterface
{

	void buildBuilding(Building building){

	}

	// Update is called once per frame
	override
	public void updateUnit() {
        if (target != null)
        {
            agent.SetDestination(target.getTargetPosition());
        }

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

    public void Hit(int damage)
    {
        Health -= damage;
    }
}
