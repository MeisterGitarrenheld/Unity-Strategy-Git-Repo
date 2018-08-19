using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteraction : MonoBehaviour {

    private GameMaster gm;
    private Camera cam;
    private Transform camTransform;

	// Use this for initialization
	void Start () {
        gm = GameMaster.Instance;
        cam = gm.MainCamera;
        camTransform = cam.transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            MouseClicked();
        }
	}

    void MouseClicked()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 1000, 1 << LayerMask.NameToLayer("UserInteraction")))
        {
            print(hit.collider.name);
            hit.collider.GetComponent<InteractionHandler>().Activate();
        }
    }

}
