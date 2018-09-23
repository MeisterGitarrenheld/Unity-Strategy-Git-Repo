using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchUser : User {


    public GameObject[] InitSpawn;
    private bool spawn;
    
    protected override void Init()
    {
        UType = UserType.Local;
        uInteraction = GetComponent<UserInteraction>();
    }

    void Update()
    {
    }
}
