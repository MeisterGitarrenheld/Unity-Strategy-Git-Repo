using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : MonoBehaviour,Interactable {
	public enum UnitType {
		BOW_UNIT,
		AXE_UNIT,
		COLLECTOR_UNIT
	}
	private byte owner;
	public float MovementSpeed;
	public float AttackSpeed;
	public NavMeshAgent agent;
	public int Health;
	public UnitType Type;
	protected WalkType target;

	public void setOwner(byte owner){
		this.owner = owner;
	}

	public void setTarget(WalkType newTarget){
		this.target = newTarget;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Health <= 0) {
			Die ();
		}
		if (target != null) {

		}
		updateUnit ();
	}

	void Die() {
		Destroy (this.gameObject);
	}

	public abstract void attack();


	#region Interactable implementation

	public void Activate (UserInteraction interactor)
	{
		//TODO
	}

	#endregion

	public abstract void updateUnit();
}
