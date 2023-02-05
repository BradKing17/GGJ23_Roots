using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnController : MonoBehaviour
{
    public int noOfPlayers = 0;
    public GameObject rootObj;
    public List<RootController> players;
    public List<GameObject> spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        noOfPlayers = Gamepad.all.Count;
        for(int i = 0; i < noOfPlayers; i++)
        {
            var clone = PlayerInput.Instantiate(rootObj, playerIndex: i, pairWithDevice: Gamepad.all[i]);
            players.Add(clone.GetComponent<RootController>());
            players[i].isGrowing = false;
            Debug.Log(i);
            clone.gameObject.transform.SetPositionAndRotation(spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);

        }

        StartCoroutine(StartCountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(5);
        StartGame();
    }

    void StartGame()
    {
        for(int i= 0; i <players.Count; i++)
        {
            players[i].isGrowing = true;
        }
    }
     
}
