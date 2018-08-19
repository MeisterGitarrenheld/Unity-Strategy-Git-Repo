using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class WalkType
	{
		private bool justMove;
		private Vector3 moveTarget;
		private Transform attackTarget;

		public WalkType (Vector3 target)
		{
			justMove = true;
			moveTarget = target;
		}
		public WalkType (Transform target)
		{
			justMove = false;
			attackTarget = target;
		}

		public Vector3 getTargetPosition(){
			if (justMove) {
				return moveTarget;
			}
			return attackTarget.position;
		}
	}
}

