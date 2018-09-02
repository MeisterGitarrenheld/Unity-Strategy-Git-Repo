using System.Collections;
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
	public GameObject[] completeBuildingList;
	public GameObject[] completeUnitList;
    private User user;

	private List<GameObject> factoryMenu = new List<GameObject> ();
	private List<GameObject> mainMenu = new List<GameObject> ();
	private List<GameObject> collectorMenu = new List<GameObject> ();

	void Start () {
        gm = GameMaster.Instance;
        user = gm.RegisterUI(this);
        rectTrans = GetComponent<RectTransform>();
        Width = rectTrans.rect.width;
        Height = rectTrans.rect.height;
		Debug.Log ("Test: " + completeBuildingList.Length);
		foreach(GameObject go in completeBuildingList){
			Debug.Log ("Test: " + (go.GetComponent<Building> () != null));
			if (go.GetComponent<Building> () != null) {
				collectorMenu.Add (go);
			} 
		}
		foreach (GameObject go in completeUnitList) {
			if (go.GetComponent<CollectorUnit> () != null) {
				mainMenu.Add (go);
			} else {
				factoryMenu.Add (go);
			}
		}
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
		GameObject[] menuItems = new GameObject[0];
		switch (menu) {
		case Menu.COLLECTOR_BUILDING:
			menuItems = collectorMenu.ToArray();
			break;
		case Menu.FACTORY_BUILDING:
			menuItems = factoryMenu.ToArray();
			break;
		case Menu.MAIN_BUILDING:
			menuItems = mainMenu.ToArray();
			break;
		}
		foreach(GameObject building in menuItems){
			Sprite icon = getSprite (building);
			GameObject button = Instantiate (buildingButton) as GameObject;
			button.transform.SetParent (buildingList.transform);
			button.GetComponent<Image> ().sprite = icon;

			if (currentMenu.Equals (Menu.COLLECTOR_BUILDING)) {
				button.GetComponent<Button> ().onClick.AddListener (() => clickBuildingButton (building));
			} else {
				button.GetComponent<Button> ().onClick.AddListener (() => clickUnitButton (building));
			}
		}
	}
	private Sprite getSprite(GameObject go) {
		if(currentMenu.Equals(Menu.COLLECTOR_BUILDING))
			return go.GetComponent<Building>().icon;
		else 
			return go.GetComponent<Unit>().icon;
	}
	public void clickUnitButton(GameObject build){
		GameObject selected = user.uInteraction.activeInteractable [0].gameObject;
		if(currentMenu.Equals(Menu.FACTORY_BUILDING)){
			selected.GetComponent<FactoryBuilding>().addToList(build.GetComponent<Unit>().getType());
		}
		else{
			selected.GetComponent<MainBuilding>().addToList(build.GetComponent<Unit>().getType());
		}


		Debug.Log("Build: "+ build.GetComponent<Unit>().name);
	}
	public void clickBuildingButton(GameObject build){
		//Display "Ghost Building" to place it
		//Debug.Log("Build: "+ build.GetComponent<Building>().name);
        user.uInteraction.StartBuilding(build);
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
