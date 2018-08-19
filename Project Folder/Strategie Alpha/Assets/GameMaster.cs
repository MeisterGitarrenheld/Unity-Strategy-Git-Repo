using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    /// <summary>
    /// Gamemaster hat alle Referenzen die im Spiel gebraucht werden
    /// </summary>
    public static GameMaster Instance;

    [HideInInspector]
    public Terrain Terrain;
    [HideInInspector]
    public Camera MainCamera;
    [HideInInspector]
    public byte RegisteredPlayers;
    
    public List<User> Players { get; private set; }
    public Dictionary<byte, List<Transform>> PlayerInteractable { get; private set; }

	void Start ()
    {
        Instance = this;
        MainCamera = Camera.main;
        PlayerInteractable = new Dictionary<byte, List<Transform>>();
        Players = new List<User>();
    }
	
    public void RegisterInteractable(Transform newObject, byte player)
    {
        List<Transform> worker;
        PlayerInteractable.TryGetValue(player, out worker);
        worker.Add(newObject);
        PlayerInteractable[player] = worker;
    }

    public void UnRegisterInteractable(Transform remObject, byte player)
    {
        List<Transform> worker;
        PlayerInteractable.TryGetValue(player, out worker);
        worker.Remove(remObject);
        PlayerInteractable[player] = worker;
    }

    public byte RegisterPlayer(User newUser)
    {
        byte newNum = RegisteredPlayers;
        RegisteredPlayers++;
        PlayerInteractable.Add(newNum, new List<Transform>());
        Players.Add(newUser);
        return newNum;
    }
}
