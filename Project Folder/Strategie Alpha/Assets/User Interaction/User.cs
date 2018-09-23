using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public enum UserType
{
    Local,
    Network,
    Computer
}

public class User : MonoBehaviour {

    public byte PlayerNum { get; protected set; }
    [HideInInspector]
    public UIHandler ui;

    protected GameMaster gm;
    public UserInteraction uInteraction { get; protected set; }
    public UserType UType { get; protected set; }

	public int Resources { get; protected set; }
	public GameObject resourceUI;

    public bool Defeated;

	void Awake ()
    {
        gm = GameMaster.Instance;
        PlayerNum = gm.RegisterPlayer(this);
    }
    private void Start()
    {
        Init();
    }
    protected virtual void Init() { }


    public void IncreaseResources(int resInc)
    {
        Resources += resInc;
        if (resourceUI != null)
            resourceUI.GetComponent<Text> ().text = Resources.ToString();
    }
    public bool DecreaseResources(int resDec)
    {
        if (resDec > Resources)
            return false;
        Resources -= resDec;
        if (resourceUI != null)
            resourceUI.GetComponent<Text>().text = Resources.ToString();
        return true;
    }
}
