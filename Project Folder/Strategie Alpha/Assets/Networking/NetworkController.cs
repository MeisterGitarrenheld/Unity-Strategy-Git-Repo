using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkController : NetworkManager {


    public static NetworkClient UserClient;

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);

        print("On Start Client Method");

        client.RegisterHandler(GameMessage.msgNum, OnServerMessage);
        UserClient = client;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler(GameMessage.msgNum, OnClientMessage);
    }

    public void OnClientMessage(NetworkMessage msg)
    {
        print("Client Message Recieved!");
        GameMessage gmsg = msg.ReadMessage<GameMessage>();
        print(gmsg.DebugMessage);
    }

    public void OnServerMessage(NetworkMessage msg)
    {
        print("Server Message Recieved!");
        GameMessage gmsg = msg.ReadMessage<GameMessage>();
        print(gmsg.DebugMessage);
    }

}
