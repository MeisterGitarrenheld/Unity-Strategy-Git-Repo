using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUi : MonoBehaviour {


    private Unit ownUnit;
    private Text unitDisplay;
    private Transform wayPoint;

	// Use this for initialization
	void Start () {
        ownUnit = GetComponentInParent<Unit>();
        unitDisplay = transform.GetComponentInChildren<Text>();
        unitDisplay.text = ownUnit.name;
        wayPoint = transform.GetChild(1);
	}

    // Update is called once per frame
    void Update()
    {
        wayPoint.position = ownUnit.agent.destination;
    }
}
