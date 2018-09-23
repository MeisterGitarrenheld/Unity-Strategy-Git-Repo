using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    private GameMaster gm;

    public GameObject[] ResourceFields;
    public Transform ResourcePositions;
    public float ResourceRespawnTimer;

    private float resTimer;
    private List<int> freeResourcePositions;
    private Transform[] resPositions;

	// Use this for initialization
	void Start () {
        gm = GameMaster.Instance;
        resPositions = ResourcePositions.GetComponentsInChildren<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        resTimer += Time.deltaTime;
        if(resTimer > ResourceRespawnTimer)
        {
            resTimer = 0;
            freeResourcePositions = new List<int>();
            for (int i = 0; i < resPositions.Length; i++)
            {
                if (resPositions[i].transform.childCount <= 0)
                    freeResourcePositions.Add(i);
            }
            if(freeResourcePositions.Count > 0)
                Instantiate(ResourceFields[Random.Range(0, ResourceFields.Length)], resPositions[freeResourcePositions[Random.Range(0, freeResourcePositions.Count)]]);
        }
	}
}
