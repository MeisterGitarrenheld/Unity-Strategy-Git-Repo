using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteraction : MonoBehaviour {

    private GameMaster gm;
    private Camera cam;
    private Transform camTransform;
    private Vector2 UpperBox;
    private Vector2 LowerBox;
    private User user;

    public List<Transform> activeInteractable { get; private set; }

    
	void Start ()
    {
        gm = GameMaster.Instance;
        cam = gm.MainCamera;
        camTransform = cam.transform;
        activeInteractable = new List<Transform>();
        user = GetComponent<User>();
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
        //print(cam.ViewportToScreenPoint(UpperBox) + ", " + cam.ViewportToScreenPoint(LowerBox));
        //Debug.DrawLine(cam.ViewportToScreenPoint(UpperBox), cam.ViewportToWorldPoint(LowerBox),Color.blue);
    }

    void Select()
    {
        if(UpperBox.x < LowerBox.x)
        {
            float tmp = UpperBox.x;
            UpperBox.x = LowerBox.x;
            LowerBox.x = tmp;
        }
        if (UpperBox.y < LowerBox.y)
        {
            float tmp = UpperBox.y;
            UpperBox.y = LowerBox.y;
            LowerBox.y = tmp;
        }
        activeInteractable.Clear();
        bool buildingSelect = false;
        foreach (Transform t in gm.PlayerInteractable[user.PlayerNum])
        {
            Vector2 screenObject = cam.WorldToViewportPoint(t.position);
            if(screenObject.x < UpperBox.x && screenObject.x > LowerBox.x
                && screenObject.y < UpperBox.y && screenObject.y > LowerBox.y)
            {
                Unit unit;
                if ((unit = t.GetComponent<Unit>()) != null )//&& unit.getOwner() == user.PlayerNum)
                {
                    if(buildingSelect)
                        activeInteractable.Clear();
                    activeInteractable.Add(t);
                    buildingSelect = false;
                }
                if(t.GetComponent<Building>() != null && activeInteractable.Count == 0)
                {
                    activeInteractable.Clear();
                    activeInteractable.Add(t);
                    buildingSelect = true;
                }
            }
        }

        foreach (Transform t in activeInteractable)
        {
            t.GetComponent<Interactable>().Activate(this);
            print(t.name);
        }

        /* // Per Raycast Auswählen, nur eine Figur
        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 1000, 1 << LayerMask.NameToLayer("UserInteraction")))
        {
            print(hit.collider.name);
        }
        */
    }

    void MoveSelect()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 1000, 1 << LayerMask.NameToLayer("Ground")))
        {
            foreach (Transform t in activeInteractable)
            {
                t.GetComponent<Interactable>().setTarget(new WalkType(hit.point));
                print(t.name);
            }
        }
    }



}
