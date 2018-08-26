using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUi : MonoBehaviour {

    private Building ownBuilding;
    private Text unitDisplay;


    // Use this for initialization
    void Start()
    {
        ownBuilding = GetComponentInParent<Building>();
        unitDisplay = transform.GetComponentInChildren<Text>();
        unitDisplay.text = ownBuilding.name;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
