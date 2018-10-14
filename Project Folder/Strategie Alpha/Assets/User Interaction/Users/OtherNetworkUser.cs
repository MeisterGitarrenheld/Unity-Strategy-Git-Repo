using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherNetworkUser : User {

    public GameObject[] InitSpawn;
    private bool spawn;

    protected override void Init()
    {
        UType = UserType.Network;
    }
    
}
