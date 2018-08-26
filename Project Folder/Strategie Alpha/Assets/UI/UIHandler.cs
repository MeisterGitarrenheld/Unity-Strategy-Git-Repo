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
		//Menü bereits sichtbar?
		if (currentMenu.Equals (Menu.COLLECTOR_BUILDING)) {
			return;
		}
		foreach(GameObject building in buildings){
			Sprite icon = building.GetComponent<Building>().icon;
			GameObject button = Instantiate (buildingButton) as GameObject;
			button.transform.SetParent (buildingList.transform);
			button.GetComponent<Image> ().sprite = icon;

			button.GetComponent<Button> ().onClick.AddListener (() => clickBuildingButton(building));
		}
		//buildingButton.GetComponent<Button>().

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
		bool jumpFirst = false;
		Debug.Log ("Delete!");
		foreach (Transform t in buildingList.transform) {
			if (!jumpFirst) {
				jumpFirst = true;
				continue;
			}
			Destroy (t.gameObject);
		}

	}
}
