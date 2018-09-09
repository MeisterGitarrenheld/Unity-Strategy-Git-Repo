using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

	public int resourceCount;

	public void collectResources(int ammount){
		resourceCount -= ammount;
		if (resourceCount <= 0) {
			Destroy (this.gameObject);
		}
	}
}
