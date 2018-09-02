using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUi : MonoBehaviour {

    private Building ownBuilding;
    private Text unitDisplay;
    private Transform wayPoint;


    // Use this for initialization
    void Start()
    {
        ownBuilding = GetComponentInParent<Building>();
        unitDisplay = transform.GetComponentInChildren<Text>();
        unitDisplay.text = ownBuilding.name;
        wayPoint = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (ownBuilding.target != null)
            wayPoint.position = ownBuilding.target.getTargetPosition();
    }
}
