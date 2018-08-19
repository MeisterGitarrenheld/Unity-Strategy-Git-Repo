using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour {

    public byte PlayerNum { get; private set; }

    private GameMaster gm;

	void Start ()
    {
        gm = GameMaster.Instance;
        PlayerNum = gm.RegisterPlayer(this);

        foreach (Unit un in FindObjectsOfType<Unit>())
        {
            gm.RegisterInteractable(un.transform, 0);
        }

        foreach (Building un in FindObjectsOfType<Building>())
        {
            gm.RegisterInteractable(un.transform, 0);
        }
    }
	

	void Update ()
    {
		
	}
}
