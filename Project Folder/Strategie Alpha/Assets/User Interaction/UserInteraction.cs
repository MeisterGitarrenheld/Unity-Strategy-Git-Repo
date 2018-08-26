using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInteraction : MonoBehaviour {

    private GameMaster gm;
    private Camera cam;
    private Transform camTransform;
    private Vector2 StartSelectPosition;
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
        StartSelectPosition = cam.ScreenToViewportPoint(Input.mousePosition);
    }
    void MiddleSelect()
    {
        Vector2 mousePosition = cam.ScreenToViewportPoint(Input.mousePosition);
        if (StartSelectPosition.x < mousePosition.x)
        {
            UpperBox.x = mousePosition.x;
            LowerBox.x = StartSelectPosition.x;
        }
        else
        {
            UpperBox.x = StartSelectPosition.x;
            LowerBox.x = mousePosition.x;
        }
        if (StartSelectPosition.y < mousePosition.y)
        {
            UpperBox.y = mousePosition.y;
            LowerBox.y = StartSelectPosition.y;
        }
        else
        {
            UpperBox.y = StartSelectPosition.y;
            LowerBox.y = mousePosition.y;
        }
        if (user.ui != null)
        {
            float scaleX = UpperBox.x - LowerBox.x;
            float scaleY = UpperBox.y - LowerBox.y;
            user.ui.mouseIndicator.rectTransform.sizeDelta = new Vector2(
                scaleX * user.ui.Width,
                scaleY * user.ui.Height);
            user.ui.mouseIndicator.rectTransform.localPosition = new Vector2(
                -user.ui.Width / 2 + UpperBox.x * user.ui.Width - scaleX * user.ui.Width / 2,
                -user.ui.Height / 2 + UpperBox.y * user.ui.Height - scaleY * user.ui.Height / 2);
        }
        //print(cam.ViewportToScreenPoint(UpperBox) + ", " + cam.ViewportToScreenPoint(LowerBox));
        //Debug.DrawLine(cam.ViewportToScreenPoint(UpperBox), cam.ViewportToWorldPoint(LowerBox),Color.blue);
    }

    void Select()
    {
        activeInteractable.Clear();
        bool buildingSelect = false;
        foreach (Transform t in gm.PlayerInteractable[user.PlayerNum])
        {
            Vector2 screenObject = cam.WorldToViewportPoint(t.position);
            if(screenObject.x < UpperBox.x + 0.01 && screenObject.x > LowerBox.x - 0.01
                && screenObject.y < UpperBox.y + 0.01 && screenObject.y > LowerBox.y - 0.01)
            {
                Unit unit;
                if ((unit = t.GetComponent<Unit>()) != null && unit.getOwner() == user.PlayerNum)
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

        string selected = "";
        foreach (Transform t in activeInteractable)
        {
            t.GetComponent<Interactable>().Activate(this);
            print(t.name);
            selected += t.name + "\n";
        }

        if (user.ui != null)
        {
            user.ui.selectedUnitText.text = selected;
            user.ui.mouseIndicator.rectTransform.localPosition = new Vector2(-user.ui.Width * 2, 0);
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
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 1000, 1 << LayerMask.NameToLayer("UserInteraction")))
        {
            foreach (Transform t in activeInteractable)
            {
                t.GetComponent<Interactable>().setTarget(new WalkType(hit.collider.transform));
            }
        }
        else if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 1000, 1 << LayerMask.NameToLayer("Ground")))
        {
            foreach (Transform t in activeInteractable)
            {
                t.GetComponent<Interactable>().setTarget(new WalkType(hit.point));
            }
        }
    }



}
