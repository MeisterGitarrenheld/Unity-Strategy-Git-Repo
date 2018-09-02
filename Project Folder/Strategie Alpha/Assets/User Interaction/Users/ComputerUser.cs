using UnityEngine;

public class ComputerUser : User
{

    public GameObject[] InitSpawn;

    // Use this for initialization
    protected override void Init () {
        UType = UserType.Computer;
		for(int i = 0; i < InitSpawn.Length; i++)
        {
            var obj = Instantiate(InitSpawn[i], transform.position + new Vector3(i*4 / 3, 3, i*4 % 3), transform.rotation).transform;
            gm.RegisterInteractable(obj, PlayerNum);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
