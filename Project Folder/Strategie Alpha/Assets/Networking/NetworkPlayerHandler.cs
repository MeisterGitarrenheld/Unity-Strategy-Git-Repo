using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerHandler : NetworkBehaviour {

    

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameMessage msg1 = new GameMessage();
            msg1.DebugMessage = "User Test";
            GameMessage msg2 = new GameMessage();
            msg2.DebugMessage = "Server Test";
            if (!isServer)
                NetworkController.UserClient.Send(GameMessage.msgNum, msg1);
            else
                NetworkServer.SendToAll(GameMessage.msgNum, msg2);
        }
	}
}
