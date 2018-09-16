using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour,Interactable {
	public enum UnitType {
		COLLECTOR_UNIT,
		AXE_UNIT,
		BOW_UNIT,
		NONE
	}
    protected byte owner;
	public float timer = 0;
	public int carryMax;
	public int carry { get; protected set; }
    public float AttackRange;
    public int Damage;
	public float AttackSpeed;
	public NavMeshAgent agent;
	public int Health;
	public UnitType Type;
    public Sprite icon;
	public WalkType target { get; protected set; }
    protected UnitUi unitUi;
    protected bool dead;
	private GameObject collided = null;

	private WalkType saveTarget;

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
		if (collided != null) {
			collectRessources ();
		}
		updateUnit ();
	}

	public UnitType getType(){
		return Type;
	}

	void Die() {
        dead = true;
        GameMaster.Instance.UnRegisterInteractable(transform, owner);
        if (GameMaster.Instance.Players[owner].UType == UserType.Local)
            GameMaster.Instance.Players[owner].uInteraction.activeInteractable.Remove(transform);
        Destroy (this.gameObject);
	}

	public abstract void attack();

	void OnTriggerEnter(Collider collider){
		if(collider.tag.Equals("Resource")) {
			//Sammel Resourcen ein!
			collided = collider.gameObject;
			collectRessources ();
			agent.SetDestination(transform.position);
		}
		else if(collider.tag.Equals("MainBuilding")){
			//Füge die Resourcen dem Lager hinzu
			GameMaster gm = GameMaster.Instance;
			User user = gm.Players [owner];
			user.IncreaseResources (carry);
			carry = 0;
			target = saveTarget;
		}
		
	}
	#region Interactable implementation
	public virtual void Activate (UserInteraction interactor)
	{
        //TODO
        unitUi.gameObject.SetActive(true);
	}
    public virtual void Deactivate(UserInteraction interactor)
    {
        unitUi.gameObject.SetActive(false);
    }

    #endregion

    public abstract void updateUnit();

	public void collectRessources(){
		agent.isStopped = true;
		if (timer <= 0) {
			timer = 1;
			if (carry < carryMax) {
				carry += 20;
				if (carry > carryMax) {
					collided.GetComponent<Resource> ().collectResources (carry - carryMax);
					carry = carryMax;
					collided = null;
					GameMaster gm = GameMaster.Instance;
					List<Transform> interact = new List<Transform> ();
					gm.PlayerInteractable.TryGetValue (owner,out interact);
					Transform closest = null;
					float minDist = float.MaxValue;
					foreach (Transform t in interact) {
						if (t.tag.Equals ("MainBuilding")) {
							float dist = Vector3.Distance (t.position, transform.position);
							if (closest == null) {
								closest = t;
								minDist = dist;
								continue;
							}
							if (minDist > dist) {
								closest = t;
								minDist = dist;
							}
						}
					}
					saveTarget = target;
					target = new WalkType (closest.position);
					agent.isStopped = false;

				} else {
					collided.GetComponent<Resource> ().collectResources (20);
				}

			}
		}
		timer -= Time.deltaTime;
	}

}
