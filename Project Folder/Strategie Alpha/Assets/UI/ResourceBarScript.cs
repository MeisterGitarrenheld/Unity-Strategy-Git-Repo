using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBarScript : MonoBehaviour {


    private RectTransform Bar;
    private Unit unit;
    private Building building;

    private int oldRes;
    private int maxRes;
    private float initXScale;

    // Use this for initialization
    void Start()
    {
        if ((unit = GetComponentInParent<Unit>()) == null)
            building = GetComponentInParent<Building>();

        Bar = transform.GetChild(1).GetComponent<RectTransform>();

        if (unit == null)
            maxRes = 0;
        else
            maxRes = unit.carryMax;
        oldRes = -1;
        initXScale = Bar.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (getRes() != oldRes)
        {
            oldRes = getRes();
            Bar.localScale = new Vector3(initXScale * oldRes / maxRes, Bar.localScale.y, Bar.localScale.z);
            Bar.localPosition = new Vector3(-initXScale * 50f * (1f - ((float)oldRes / (float)maxRes)), Bar.localPosition.y, Bar.localPosition.z);
        }
    }

    int getRes()
    {
        if (unit == null)
            return maxRes;
        return unit.carry;
    }
}
