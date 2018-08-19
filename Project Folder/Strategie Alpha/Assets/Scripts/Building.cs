using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour,Interactable {

	public int Health;
	private WalkType target;
	private byte owner;


	public byte getOwner(){
		return owner;
	}

	public void setOwner(byte owner){
		this.owner = owner;
	}
	
	// Update is called once per frame
	void Update () {
		updateBuilding ();

		if (Health <= 0) {
			Die ();
		}
	}

	void Die() {
		Destroy (this.gameObject);
	}

	public abstract void updateBuilding();

	#region Interactable implementation

	public void Activate (UserInteraction interactor)
	{
		//TODO  
	}

	#endregion

	public void setTarget (WalkType newTarget)
	{
		target = newTarget;
	}
}
