using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
	public enum UnitType {
		BOW_UNIT,
		AXE_UNIT,
		COLLECTOR_UNIT
	}
	public float MovementSpeed;
	public float AttackSpeed;
	public int Health;
	public UnitType Type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
