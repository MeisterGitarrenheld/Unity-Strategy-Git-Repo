using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public List<Transform> Resources;

    MapController mController;

    private bool gameOver;
    private float gameOverTimer = 10f;

	void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        MainCamera = Camera.main;
        PlayerInteractable = new Dictionary<byte, List<Transform>>();
        Players = new List<User>();
        Resources = new List<Transform>();
        foreach (var r in GameObject.FindGameObjectsWithTag("Resource"))
            Resources.Add(r.transform);
        mController = GetComponent<MapController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);

        if (gameOver)
        {
            gameOverTimer -= Time.deltaTime;
            if (gameOverTimer <= 0)
                SceneManager.LoadScene(0);
        }
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

        if (worker.Count == 0)
            Players[player].Defeated = true;

        int defeatedCount = 0;
        Players.ForEach(v => { if (v.Defeated) defeatedCount++; });
        if (defeatedCount >= Players.Count - 1)
        {
            gameOver = true;
            print("Game Over");
        }

    }

    public byte RegisterPlayer(User newUser)
    {
        byte newNum = RegisteredPlayers;
        RegisteredPlayers++;
        PlayerInteractable.Add(newNum, new List<Transform>());
        Players.Add(newUser);
        return newNum;
    }

    public User RegisterUI(UIHandler ui)
    {
        //Add Ui to first User without Ui
        foreach(User user in Players)
        {
            if(user.ui == null && user.UType == UserType.Local)
            {
                user.ui = ui;
                return user;
            }
        }
        //If no free user, then destroy
        Destroy(ui.gameObject);
        return null;
    }

    public void UpdateResources()
    {
        Resources = new List<Transform>();
        foreach (var r in GameObject.FindGameObjectsWithTag("Resource"))
            Resources.Add(r.transform);
    }

}
