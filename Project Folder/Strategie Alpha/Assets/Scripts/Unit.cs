using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : MonoBehaviour,Interactable {
	public enum UnitType {
		AXE_UNIT,
		BOW_UNIT,
		COLLECTOR_UNIT
	}
	protected byte owner;
	public float AttackSpeed;
	public NavMeshAgent agent;
	public int Health;
	public UnitType Type;
	public WalkType target { get; protected set; }
    protected UnitUi unitUi;

	public void setOwner(byte owner){
		this.owner = owner;
	}
	public byte getOwner(){
		return owner;
	}

	public void setTarget(WalkType newTarget){
		this.target = newTarget;
	}

	// Use this for initialization
	void Start () {
        unitUi = transform.GetComponentInChildren<UnitUi>();
        unitUi.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		if (Health <= 0) {
			Die ();
		}
		if (target != null) {
			agent.SetDestination (target.getTargetPosition ());
		}
		updateUnit ();
	}

	public UnitType getType(){
		return Type;
	}

	void Die() {
		Destroy (this.gameObject);
	}

	public abstract void attack();


	#region Interactable implementation

	public void Activate (UserInteraction interactor)
	{
        //TODO
        unitUi.gameObject.SetActive(true);
	}
    public void Deactivate(UserInteraction interactor)
    {
        unitUi.gameObject.SetActive(false);
    }

    #endregion

    public abstract void updateUnit();

}
