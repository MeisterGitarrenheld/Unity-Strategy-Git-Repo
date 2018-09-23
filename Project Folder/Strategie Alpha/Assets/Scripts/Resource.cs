using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

	public int resourceCount;

    private void Start()
    {
        GameMaster.Instance.UpdateResources();
    }

    public bool collectResources(int ammount){
		resourceCount -= ammount;
		if (resourceCount <= 0) {
			Destroy (gameObject);
            if (transform.parent.childCount == 1 && !transform.parent.name.Contains("pos"))
                Destroy(transform.parent.gameObject, 0.01f);
		}
        return resourceCount <= 0;
	}

    private void OnDestroy()
    {
        GameMaster.Instance.UpdateResources();
    }
}
