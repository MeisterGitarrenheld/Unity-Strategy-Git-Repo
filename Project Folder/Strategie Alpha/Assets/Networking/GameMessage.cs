using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameMessage : MessageBase {

    public static short msgNum = MsgType.Highest + 1;

    public int SentPlayerNum;
    public int SentMessageType;
    public string DebugMessage;

}
