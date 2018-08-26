using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    private RectTransform Bar;
    private Unit unit;
    private Building building;

    private int oldLife;
    private int maxLife;
    private float initXScale;

	// Use this for initialization
	void Start () {
        if ((unit = GetComponentInParent<Unit>()) == null)
            building = GetComponentInParent<Building>();

        Bar = transform.GetChild(1).GetComponent<RectTransform>();

        oldLife = maxLife = getLife();
        initXScale = Bar.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {
		if(getLife() != oldLife)
        {
            oldLife = getLife();
            Bar.localScale = new Vector3(initXScale * oldLife / maxLife, Bar.localScale.y, Bar.localScale.z);
            Bar.localPosition = new Vector3(-initXScale * 50f * (1f - ((float)oldLife / (float)maxLife)), Bar.localPosition.y, Bar.localPosition.z);
        }
	}

    int getLife()
    {
        if (unit == null)
            return building.Health;
        return unit.Health;
    }
}
