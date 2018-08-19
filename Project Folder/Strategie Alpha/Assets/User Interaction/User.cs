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
	}
	

	void Update ()
    {
		
	}
}
