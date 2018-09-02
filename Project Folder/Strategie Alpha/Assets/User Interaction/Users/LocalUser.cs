using UnityEngine;

public class LocalUser : User {


	protected override void Init () {
        UType = UserType.Local;
        uInteraction = GetComponent<UserInteraction>();

        foreach (Unit un in FindObjectsOfType<Unit>())
        {
            gm.RegisterInteractable(un.transform, PlayerNum);
            un.setOwner(PlayerNum);
        }

        foreach (Building un in FindObjectsOfType<Building>())
        {
            gm.RegisterInteractable(un.transform, PlayerNum);
            un.setOwner(PlayerNum);
            un.Place();
        }
	}
	
	void Update () {

    }
}
