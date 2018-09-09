using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NWGM : NetworkManager {


    public GameObject NetworkPlayer;
    public GameObject NetworkOpponent;

    private Transform[] StartPositions;
    

    private void Start()
    {
        StartPositions = transform.GetComponentsInChildren<Transform>();
    }

    public override void OnStartHost()
    {
        base.OnStartHost();
        print("Host!!");
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        print("Server!!!");
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
        print("Client!!!");
        //Instantiate(NetworkPlayer, StartPositions[connectedPlayers - 1].position, Quaternion.identity);
        //connectedPlayers++;
    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
    }

    public override void OnStopHost()
    {
        base.OnStopHost();
    }
    public override void OnStopServer()
    {
        base.OnStopServer();
    }
}
