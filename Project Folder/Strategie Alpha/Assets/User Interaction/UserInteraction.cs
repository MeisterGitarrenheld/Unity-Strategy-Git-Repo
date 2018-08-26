﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInteraction : MonoBehaviour {

    private GameMaster gm;
    private Camera cam;
    private Vector2 StartSelectPosition;
    private Vector2 UpperBox;
    private Vector2 LowerBox;
    private User user;

    public List<Transform> activeInteractable { get; private set; }
    public float SelectMouseYBorder;
    public float SelectMouseXBorder;

    private bool canSelect;

    private float DoubleclickTimer;
    private Unit.UnitType doubleClickType;
    private int noType = 10000;
    private bool doubleClickSelect;

    void Start ()
    {
        gm = GameMaster.Instance;
        cam = gm.MainCamera;
        activeInteractable = new List<Transform>();
        user = GetComponent<User>();
	}

	void Update ()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (DoubleclickTimer <= 0)
                StartSelect();
            else
                DoubleClickSelect();
        }
        if (Input.GetMouseButton(0))
        {
            MiddleSelect();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (DoubleclickTimer <= 0 && !doubleClickSelect)
                Select();
            doubleClickSelect = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            MoveSelect();
        }

        if (DoubleclickTimer >= 0)
            DoubleclickTimer -= Time.deltaTime;

	}

    void DoubleClickSelect()
    {
        if(doubleClickType != (Unit.UnitType)noType)
        {
            activeInteractable.Clear();
            doubleClickSelect = true;

            UpperBox = new Vector2(1, 1);
            LowerBox = new Vector2(0, 0);
            foreach (Transform t in gm.PlayerInteractable[user.PlayerNum])
            {
                Vector2 screenObject = cam.WorldToViewportPoint(t.position);
                if (screenObject.x < UpperBox.x + 0.01 && screenObject.x > LowerBox.x - 0.01
                    && screenObject.y < UpperBox.y + 0.01 && screenObject.y > LowerBox.y - 0.01)
                {
                    Unit unit;
                    if ((unit = t.GetComponent<Unit>()) != null && unit.getOwner() == user.PlayerNum
                        && unit.Type == doubleClickType)
                    {
                        activeInteractable.Add(t);
                    }
                }
            }
            string selected = "";
            foreach (Transform t in activeInteractable)
            {
                t.GetComponent<Interactable>().Activate(this);
                selected += t.name + "\n";
            }

            if (user.ui != null)
            {
                user.ui.selectedUnitText.text = selected;
                user.ui.mouseIndicator.rectTransform.localPosition = new Vector2(-user.ui.Width * 2, 0);
            }
        }
    }

    void StartSelect()
    {
        StartSelectPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        if (StartSelectPosition.y < SelectMouseYBorder)
        {
            canSelect = false;
            StartSelectPosition.y = SelectMouseYBorder;
            return;
        }
        canSelect = true;

        foreach (Transform t in activeInteractable)
        {
            t.GetComponent<Interactable>().Deactivate(this);
        }
        activeInteractable.Clear();
    }
    void MiddleSelect()
    {
        Vector2 mousePosition = cam.ScreenToViewportPoint(Input.mousePosition);
        if (mousePosition.y < SelectMouseYBorder)
            mousePosition.y = SelectMouseYBorder;

        if (!canSelect)
            return;

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
        if (!canSelect)
            return;
        activeInteractable.Clear();
        bool buildingSelect = false;
        bool collectorSelect = true;
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
                    if (unit.Type != Unit.UnitType.COLLECTOR_UNIT && collectorSelect)
                    {
                        activeInteractable.Clear();
                        collectorSelect = false;
                    }
                    else if(unit.Type == Unit.UnitType.COLLECTOR_UNIT && !collectorSelect)
                    {
                        continue;
                    }
                    activeInteractable.Add(t);
                    doubleClickType = unit.Type;
                    buildingSelect = false;
                }
                if(t.GetComponent<Building>() != null && activeInteractable.Count == 0)
                {
                    activeInteractable.Clear();
                    activeInteractable.Add(t);
                    doubleClickType = (Unit.UnitType)noType;
                    buildingSelect = true;
                }
            }
        }

        if(activeInteractable.Count == 1)
        {
            if (DoubleclickTimer <= 0)
                DoubleclickTimer = 0.1f;
        }

        string selected = "";
        foreach (Transform t in activeInteractable)
        {
            t.GetComponent<Interactable>().Activate(this);
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
            int squareSize = Mathf.CeilToInt(Mathf.Sqrt(activeInteractable.Count));
            for (int i = 0; i < squareSize; i++)
            {
                for (int j = 0; j < squareSize; j++)
                {
                    if (i * squareSize + j >= activeInteractable.Count)
                        return;
                    activeInteractable[i * squareSize + j].GetComponent<Interactable>().setTarget(new WalkType(hit.point + new Vector3(j*3, 0, i*3)));
                }
            }
        }
    }



}
