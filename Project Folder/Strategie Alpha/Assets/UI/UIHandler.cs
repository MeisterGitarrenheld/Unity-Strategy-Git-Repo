﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {
	private enum Menu{
		COLLECTOR_BUILDING,
		FACTORY_BUILDING,
		MAIN_BUILDING,
		NONE
	}
	private Menu currentMenu = Menu.NONE;
    public Text selectedUnitText;
    public Image mouseIndicator;

    public float Width { get; private set; }
    public float Height { get; private set; }

    private GameMaster gm;
    private RectTransform rectTrans;
	public GameObject buildingButton;
	public GameObject buildingList;
	public GameObject[] buildings;
	public GameObject[] units;

	void Start () {
        gm = GameMaster.Instance;
        gm.RegisterUI(this);
        rectTrans = GetComponent<RectTransform>();
        Width = rectTrans.rect.width;
        Height = rectTrans.rect.height;
	}

	public void showCollectorMenu(){
		showMenu (Menu.COLLECTOR_BUILDING);
	}
	public void showFactoryMenu(){
		showMenu (Menu.FACTORY_BUILDING);
	}
	public void showMainBuildingMenu(){
		showMenu (Menu.MAIN_BUILDING);
	}
	private void showMenu(Menu menu) {
		if (currentMenu.Equals (menu)) {
			return;
		}
		currentMenu = menu;
		GameObject[] menuItems;
		switch (menu) {
		case Menu.COLLECTOR_BUILDING:
			menuItems = buildings;
			break;
		case Menu.FACTORY_BUILDING:
			menuItems = units;
			break;
		case Menu.MAIN_BUILDING:
			menuItems = new GameObject[1];
			menuItems [0] = units [0];
			break;
		}
		foreach(GameObject building in menuItems){
			Sprite icon = building.GetComponent<Building>().icon;
			GameObject button = Instantiate (buildingButton) as GameObject;
			button.transform.SetParent (buildingList.transform);
			button.GetComponent<Image> ().sprite = icon;

			button.GetComponent<Button> ().onClick.AddListener (() => clickBuildingButton(building));
		}
	}
	public void clickUnitButton(GameObject build){
		Debug.Log("Build: "+ build.GetComponent<Unit>().name);
	}
	public void clickBuildingButton(GameObject build){
		//Display "Ghost Building" to place it
		Debug.Log("Build: "+ build.GetComponent<Building>().name);
	}
	public void showNoneMenu(){
		//Menü bereits sichtbar?
		if (currentMenu.Equals (Menu.NONE)) {
			return;
		}
		currentMenu = Menu.NONE;
		foreach (Transform t in buildingList.transform) {
			Destroy (t.gameObject);
		}

	}
}
