using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowUnit : Unit, HitInterface
{

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

    public void Hit(int damage)
    {

    }
}
