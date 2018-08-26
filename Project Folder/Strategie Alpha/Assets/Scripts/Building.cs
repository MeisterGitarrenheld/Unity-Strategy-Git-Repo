using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour,Interactable {

	public int Health;
	public WalkType target { get; protected set; }
	protected byte owner;
    protected BuildingUi buildingUi;

	public byte getOwner(){
		return owner;
	}

	public void setOwner(byte owner){
		this.owner = owner;
	}
    public void Start()
    {
        buildingUi = GetComponentInChildren<BuildingUi>();
        buildingUi.gameObject.SetActive(false);
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
        buildingUi.gameObject.SetActive(true);
	}

    public void Deactivate(UserInteraction interactor)
    {
        buildingUi.gameObject.SetActive(false);
    }
    #endregion

    public void setTarget (WalkType newTarget)
	{
		target = newTarget;
	}

}
