using UnityEngine;

public class ComputerUser : User
{

    public GameObject[] InitSpawn;

    private bool spawn;

    // Use this for initialization
    protected override void Init () {
        UType = UserType.Computer;
        spawn = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (spawn)
        {
            for (int i = 0; i < InitSpawn.Length; i++)
            {
                var obj = Instantiate(InitSpawn[i], transform.position + new Vector3(i * 4 / 3, 3, i * 4 % 3), transform.rotation).transform;
                gm.RegisterInteractable(obj, PlayerNum);
                var unit = obj.GetComponent<Unit>();
                var build = obj.GetComponent<Building>();
                if (unit != null)
                    unit.setOwner(PlayerNum);
                else if (build != null)
                {
                    build.setOwner(PlayerNum);
                    build.Place();
                }
            }
            spawn = false;
        }
    }
}
