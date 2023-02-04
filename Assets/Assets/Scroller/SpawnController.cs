using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnController : MonoBehaviour
{
    public int noOfPlayers = 0;
    public GameObject rootObj;
    public List<GameObject> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        noOfPlayers = Gamepad.all.Count;
        for(int i = 0; i < noOfPlayers; i++)
        {
            var clone = Instantiate(rootObj, spawnPoints[i].transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
