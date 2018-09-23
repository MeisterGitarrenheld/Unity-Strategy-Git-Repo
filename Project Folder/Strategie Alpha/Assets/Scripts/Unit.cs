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
    protected GameMaster gm;
    protected byte owner;
	public float timer = 0;
	public int carryMax;
	public int carry { get; protected set; }
	public int cost;
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

    public string TargetType;
    public float ResourceRange;

    public void setOwner(byte owner){
		this.owner = owner;
	}
	public byte getOwner(){
		return owner;
	}

	public void setTarget(WalkType newTarget){
        agent.isStopped = false;
		this.target = newTarget;
	}

	// Use this for initialization
	void Start () {
        gm = GameMaster.Instance;
        unitUi = transform.GetComponentInChildren<UnitUi>();
        unitUi.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Die();
        }
        updateUnit();

        if (target != null && target.WType == WType.Collect)
        {
            if (collided == null)
            {
                if(agent.isOnNavMesh)
                    agent.isStopped = false;
                if (Vector3.Distance(transform.position, target.getTargetPosition()) < 1.4f || 
                    (Vector3.Distance(transform.position, target.getTargetPosition()) < 1.4f &&
                    agent.velocity.magnitude < 3f))
                {
                    target = null;
                    Transform selectedResource = null;
                    float minDist = ResourceRange;
                    foreach (var r in gm.Resources)
                    {
                        float tempdist = 0;
                        if ((tempdist = Vector3.Distance(r.position, transform.position)) < minDist)
                        {
                            minDist = tempdist;
                            selectedResource = r;
                        }
                    }
                    if (selectedResource != null)
                    {
                        target = new WalkType(selectedResource.position, WType.Collect);
                    }
                }
            }
        }
        TargetType = target == null ? "" : target.WType.ToString();
    }

	public UnitType getType(){
		return Type;
	}

	protected virtual void Die() {
        dead = true;
        GameMaster.Instance.UnRegisterInteractable(transform, owner);
        if (GameMaster.Instance.Players[owner].UType == UserType.Local)
            GameMaster.Instance.Players[owner].uInteraction.activeInteractable.Remove(transform);
        Destroy (this.gameObject);
	}

	public abstract void attack();
    
    void OnTriggerStay(Collider collider)
    {
		if(target != null && target.WType == WType.Collect && collider.tag.Equals("Resource")) {
			//Sammel Resourcen ein!
			collided = collider.gameObject;
			collectRessources (collided);
			//agent.SetDestination(transform.position);
		}
        else if (target != null && target.WType == WType.ReturnResources && carry > 0 && collider.tag.Equals("MainBuilding") && collider.GetComponent<MainBuilding>().getOwner() == owner)
        {
            //Füge die Resourcen dem Lager hinzu
            User user = GameMaster.Instance.Players[owner];
            user.IncreaseResources(carry);
            carry = 0;
            if (saveTarget != null)
                target = saveTarget;
        }

    }

    void OnTriggerExit(Collider collider)
    {
        if (collided != null && target != null && target.WType == WType.Collect)
        {
            //Sammel Resourcen ein!
            //agent.SetDestination(collided.transform.position);
            agent.isStopped = false;
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

    public void collectRessources(GameObject collided)
    {
        if (target != null && target.WType == WType.Collect)
            agent.isStopped = true;
        if (timer <= 0)
        {
            timer = 1;
            bool empty = false;
            carry += 20;
            empty = collided.GetComponent<Resource>().collectResources(20 < carry - carryMax ? carry - carryMax : 20);
            if (carry >= carryMax)
            {
                carry = carryMax;
                GameMaster gm = GameMaster.Instance;
                List<Transform> interact = new List<Transform>();
                gm.PlayerInteractable.TryGetValue(owner, out interact);
                Transform closest = null;
                float minDist = float.MaxValue;
                foreach (Transform t in interact)
                {
                    if (t.tag.Equals("MainBuilding"))
                    {
                        float dist = Vector3.Distance(t.position, transform.position);
                        if (closest == null)
                        {
                            closest = t;
                            minDist = dist;
                            continue;
                        }
                        if (minDist > dist)
                        {
                            closest = t;
                            minDist = dist;
                        }
                    }
                }
                saveTarget = empty ? new WalkType(transform.position) : target;
                if (closest != null)
                    target = new WalkType(closest.position, WType.ReturnResources);
                agent.isStopped = false;

            }

            if (empty)
            {
                Transform selectedResource = null;
                float minDist = ResourceRange;
                foreach (var r in gm.Resources)
                {
                    float tempdist = 0;
                    if ((tempdist = Vector3.Distance(r.position, transform.position)) < minDist && r != collided.transform)
                    {
                        minDist = tempdist;
                        selectedResource = r;
                    }
                }
                if (selectedResource != null)
                {
                    if (carry < carryMax)
                        target = new WalkType(selectedResource.position, WType.Collect);
                    saveTarget = new WalkType(selectedResource.position, WType.Collect);
                }
            }

        }
        timer -= Time.deltaTime;
    }

}
