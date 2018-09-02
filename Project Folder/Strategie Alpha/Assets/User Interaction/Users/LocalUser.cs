using UnityEngine;

public class LocalUser : User {


	protected override void Init () {
        UType = UserType.Local;
        uInteraction = GetComponent<UserInteraction>();

        foreach (Unit un in FindObjectsOfType<Unit>())
        {
            gm.RegisterInteractable(un.transform, PlayerNum);
        }

        foreach (Building un in FindObjectsOfType<Building>())
        {
            gm.RegisterInteractable(un.transform, PlayerNum);
            un.Place();
        }
	}
	
	void Update () {

    }
}
