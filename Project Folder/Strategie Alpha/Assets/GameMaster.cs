using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    /// <summary>
    /// Gamemaster hat alle Referenzen die im Spiel gebraucht werden
    /// </summary>
    public static GameMaster Instance;

    [HideInInspector]
    public Terrain Terrain;
    [HideInInspector]
    public Camera MainCamera;


	void Start ()
    {
        Instance = this;

        MainCamera = Camera.main;
	}
	
}
