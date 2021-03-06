﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkUser : User
{
    public GameObject[] InitSpawn;
    private bool spawn;

    protected override void Init()
    {
        UType = UserType.Local;
        uInteraction = GetComponent<UserInteraction>();
        IncreaseResources(100);
        spawn = true;
    }

    void Update()
    {
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
