using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteraction : MonoBehaviour {

    private GameMaster gm;
    private Camera cam;
    private Transform camTransform;
    private Vector2 UpperBox;
    private Vector2 LowerBox;

    public List<Interactable> activeInteractable { get; private set; }
    public GameObject activeObject { get; private set; }

    
	void Start ()
    {
        gm = GameMaster.Instance;
        cam = gm.MainCamera;
        camTransform = cam.transform;
        activeInteractable = new List<Interactable>();
	}
	
	void Update ()
    {

        if (Input.GetMouseButtonDown(0))
        {
            StartSelect();
        }
        if (Input.GetMouseButton(0))
        {
            MiddleSelect();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Select();
        }
        if (Input.GetMouseButtonUp(1))
        {
            MoveSelect();
        }

	}

    void StartSelect()
    {
        UpperBox = cam.ScreenToViewportPoint(Input.mousePosition);
    }
    void MiddleSelect()
    {
        LowerBox = cam.ScreenToViewportPoint(Input.mousePosition);
    }

    void Select()
    {
        

        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 1000, 1 << LayerMask.NameToLayer("UserInteraction")))
        {
            print(hit.collider.name);
            hit.collider.GetComponent<Interactable>().Activate(this);
        }
    }

    void MoveSelect()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 1000, 1 << LayerMask.NameToLayer("Ground")) && activeObject != null)
        {
            print(hit.collider.name);
            print(activeObject.name);
            activeObject.transform.parent.transform.position = hit.point + Vector3.up * 10;
        }
    }

    public void UpdateActiveObject(GameObject newActiveObjetc)
    {
        activeObject = newActiveObjetc;
    }



}
