using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeUnit : Unit, HitInterface {

    public float AttackRange;
    public int Damage;
    private HitInterface HitTarget;
    private float attackTimer;

    // Update is called once per frame
    override
    public void updateUnit()
    {
        if (target != null)
        {
            agent.SetDestination(target.getTargetPosition());
        }
    }

    override
    public void attack()
    {
        if (HitTarget != null)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= AttackSpeed)
            {
                HitTarget.Hit(Damage);
                attackTimer = 0;
            }
        }
    }

    public void Hit(int damage)
    {
        Health -= damage;
    }
}
