using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorUnit : Unit, HitInterface
{
    public GameObject toBuild { get; private set; }

    public void SetBuildBuilding(GameObject building)
    {
        toBuild = building;
    }

    public void StopBuild()
    {
        if (toBuild != null)
        {
            GameMaster.Instance.UnRegisterInteractable(toBuild.transform, owner);
            Destroy(toBuild);
            toBuild = null;
        }
    }

	public void buildBuilding(){
        if(toBuild != null)
        {
            toBuild.GetComponent<Building>().Place();
            toBuild = null;
        }
	}

	// Update is called once per frame
	override
	public void updateUnit() {
        if (target != null)
        {
            if(agent.isOnNavMesh)
                agent.SetDestination(target.getTargetPosition());
            if (target.WType == WType.Build && Vector3.Distance(transform.position, target.getTargetPosition()) < 5)
            {
                buildBuilding();
                target = null;
                if (agent.isOnNavMesh)
                    agent.SetDestination(transform.position);
            }
            else if (target.WType != WType.Build && toBuild != null)
            {
                StopBuild();
            }

            if (target != null && target.WType == WType.Walk && Vector3.Distance(transform.position, target.getTargetPosition()) < 1)
            {
                if (agent.isOnNavMesh)
                    agent.SetDestination(transform.position);
                target = null;
            }
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

    protected override void Die()
    {
        dead = true;
        GameMaster.Instance.UnRegisterInteractable(transform, owner);
        if (GameMaster.Instance.Players[owner].UType == UserType.Local)
            GameMaster.Instance.Players[owner].uInteraction.activeInteractable.Remove(transform);
        StopBuild();
        Destroy(this.gameObject);
    }
}
