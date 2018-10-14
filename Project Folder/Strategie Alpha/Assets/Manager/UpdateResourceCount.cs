using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateResourceCount : MonoBehaviour {

	private GameMaster gm;

	// Use this for initialization
	void Start () {
		gm = GameMaster.Instance;	
	}
	
	// Update is called once per frame
	void Update () {
		Text t = GetComponent<Text> ();
		//t.text = gm.PlayerInteractable
	}
}
