using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuilding : Building {
	
	// Update is called once per frame
	override
	public void updateBuilding () {
		if (Health <= 0) {
			//TODO GameEnd!

		}
	}
}
