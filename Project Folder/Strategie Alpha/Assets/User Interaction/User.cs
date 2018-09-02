using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UserType
{
    Local,
    Network,
    Computer
}

public class User : MonoBehaviour {

    public byte PlayerNum;
    [HideInInspector]
    public UIHandler ui;

    protected GameMaster gm;
    public UserInteraction uInteraction { get; protected set; }
    public UserType UType { get; protected set; }

	void Start ()
    {
        gm = GameMaster.Instance;
        PlayerNum = gm.RegisterPlayer(this);
        Init();
    }

    protected virtual void Init() { }
}
