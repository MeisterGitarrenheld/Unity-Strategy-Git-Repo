using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum UserType
{
    Local,
    Network,
    Computer
}

public class User : NetworkBehaviour {

    public byte PlayerNum { get; protected set; }
    [HideInInspector]
    public UIHandler ui;

    protected GameMaster gm;
    public UserInteraction uInteraction { get; protected set; }
    public UserType UType { get; protected set; }

    public int Resources { get; protected set; }

	void Start ()
    {
        gm = GameMaster.Instance;
        PlayerNum = gm.RegisterPlayer(this);
        Init();
    }

    protected virtual void Init() { }


    public void IncreaseResources(int resInc)
    {
        Resources += resInc;
    }
    public bool DecreaseResources(int resDec)
    {
        if (resDec > Resources)
            return false;
        Resources -= resDec;
        return true;
    }
}
