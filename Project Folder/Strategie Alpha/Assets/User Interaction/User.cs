using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour {

    public byte PlayerNum { get; private set; }
    [HideInInspector]
    public UIHandler ui;

    private GameMaster gm;
    public UserInteraction uInteraction { get; private set; }

	void Start ()
    {
        gm = GameMaster.Instance;
        PlayerNum = gm.RegisterPlayer(this);
        uInteraction = GetComponent<UserInteraction>();


        foreach (Unit un in FindObjectsOfType<Unit>())
        {
            gm.RegisterInteractable(un.transform, PlayerNum);
        }

        foreach (Building un in FindObjectsOfType<Building>())
        {
            gm.RegisterInteractable(un.transform, PlayerNum);
        }
    }
	

	void Update ()
    {
		
	}
}
