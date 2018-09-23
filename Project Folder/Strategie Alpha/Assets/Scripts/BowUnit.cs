using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowUnit : Unit, HitInterface
{
    private HitInterface HitTarget;
    private float attackTimer;

    // Update is called once per frame
    override
    public void updateUnit()
    {
        if (target != null && !dead)
        {
            if (target.WType == WType.Attack && target.attackTarget != null)
            {
                if (HitTarget == null)
                    HitTarget = target.attackTarget.GetComponent<HitInterface>();
                if (Vector3.Distance(transform.position, target.getTargetPosition()) > AttackRange)
                {
                    agent.isStopped = false;
                    if (agent.isOnNavMesh)
                        agent.SetDestination(target.getTargetPosition());
                }
                else
                {
                    agent.isStopped = true;
                    attack();
                }
            }
            else if (target.WType == WType.Walk && Vector3.Distance(transform.position, target.getTargetPosition()) > 2)
			{
                agent.isStopped = false;
                if (agent.isOnNavMesh)
                    agent.SetDestination(target.getTargetPosition());
            }
            else if (target.WType == WType.Collect)
            {
                agent.isStopped = false;
                if (agent.isOnNavMesh)
                    agent.SetDestination(target.getTargetPosition());
            }
            else
            {
                target = null;
                agent.isStopped = true;
            }
        }
		if (target == null) {
			SearchInRangeTargets ();
		}
    }

    override
    public void attack()
    {
        if (HitTarget != null && target.attackTarget != null)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= AttackSpeed)
            {

                target.attackTarget.GetComponent<HitInterface>().Hit(Damage);
                attackTimer = 0;
            }
        }
        else
            target = null;
    }

    public void Hit(int damage)
    {
        Health -= damage;
    }
	private void SearchInRangeTargets(){
		Collider[] colliders = Physics.OverlapSphere (transform.position, AttackRange);
		Transform attackTarget = null;
		float minDistance = float.MaxValue;
		foreach (Collider col in colliders) {
			if (col.gameObject.GetComponent<Unit> () == null) {
				continue;
			}
			Unit foundUnit = col.gameObject.GetComponent<Unit> ();

			if (foundUnit.getOwner () == owner) {
				continue;
			}
			float distance = Vector3.Distance (transform.position, foundUnit.transform.position);
			if (distance < minDistance) {
				attackTarget = foundUnit.transform;
				minDistance = distance; 
			}

		}
		if (attackTarget == null) {
			return;
		}
		//Debug.Log ("Attack: " + attackTarget.name);
		//Debug.Log ("-Distance: " + minDistance);
		target = new WalkType (attackTarget);
	}
}
