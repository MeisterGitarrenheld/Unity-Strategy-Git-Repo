using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour, Interactable
{

    public int Health;
    public WalkType target { get; protected set; }
    protected byte owner;
    protected BuildingUi buildingUi;
	public int costs;
    public Sprite icon;
    private bool placed;

    public string TargetType;
    public int buildTime;

    public byte getOwner()
    {
        return owner;
    }

    public void setOwner(byte owner)
    {
        this.owner = owner;
    }
    public void Start()
    {
        buildingUi = GetComponentInChildren<BuildingUi>();
        buildingUi.gameObject.SetActive(false);
		target = null;
    }
    // Update is called once per frame
    void Update()
    {
        if (placed)
            updateBuilding();

        if (Health <= 0)
        {
            Die();
        }

        TargetType = target == null ? "" : target.WType.ToString();

    }

    protected virtual void Die()
    {
        GameMaster.Instance.UnRegisterInteractable(transform, owner);
        if (GameMaster.Instance.Players[owner].UType == UserType.Local)
            GameMaster.Instance.Players[owner].uInteraction.activeInteractable.Remove(transform);
        Destroy(this.gameObject);
    }

    public abstract void updateBuilding();

    #region Interactable implementation

    public virtual void Activate(UserInteraction interactor)
    {
        //TODO  
        buildingUi.gameObject.SetActive(true);
    }

    public virtual void Deactivate(UserInteraction interactor)
    {
        buildingUi.gameObject.SetActive(false);
    }
    #endregion

    public void setTarget(WalkType newTarget)
    {
        target = newTarget;
    }

    public void Init()
    {
        GetComponent<Collider>().enabled = false;
        placed = false;
    }

    public void Place()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        placed = true;
    }

}