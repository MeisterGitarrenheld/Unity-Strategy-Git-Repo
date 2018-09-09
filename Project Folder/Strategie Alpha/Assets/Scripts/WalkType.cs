using System;
using UnityEngine;

public enum WType
{
    Walk,
    Attack,
    Build,
    Collect
}

public class WalkType
{
    public WType WType { get; private set; }
    private bool justMove;
    public Vector3 moveTarget { get; private set; }
    public Transform attackTarget { get; private set; }

    public WalkType(Vector3 target)
    {
        justMove = true;
        moveTarget = target;
        WType = WType.Walk;
    }
    public WalkType(Transform target)
    {
        justMove = false;
        attackTarget = target;
        WType = WType.Attack;
    }
    public WalkType(Vector3 target, WType type)
    {
        WType = type;
        justMove = true;
        moveTarget = target;
    }

    public Vector3 getTargetPosition()
    {
        if (justMove)
        {
            return moveTarget;
        }
        return attackTarget.position;
    }
}

