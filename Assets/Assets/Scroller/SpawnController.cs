using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

public class SpawnController : MonoBehaviour
{
    public int noOfPlayers = 0;
    List<Gamepad> activePads;
    public GameObject rootObj;
    public List<RootController> players;
    public List<GameObject> spawnPoints;
    public List<GameObject> objectSpawns;

    public List<GameObject> items;
    public bool gameRunning = false;
    public float objTimer = 4;

    public TMP_Text startText;
    public TMP_Text winText;

    // Start is called before the first frame update
    void Start()
    {
        gameRunning = false;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        RootController controller = playerInput.gameObject.GetComponent<RootController>();
        controller.playerIndex = noOfPlayers;
        players.Add(controller);
        controller.isGrowing = false;
        playerInput.gameObject.transform.SetPositionAndRotation(spawnPoints[noOfPlayers].transform.position, spawnPoints[noOfPlayers].transform.rotation);
        noOfPlayers++;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (noOfPlayers >= 2)
        {

            startText.text = "PRESS ANY BUTTON TO START";

            InitGame();

        }
        else
        {
            startText.text = "WAITING FOR PLAYERS";
        }

        if (gameRunning)
        {
            SpawnItems();
            for(int i = 0; i < players.Count; i++)
            {
                if(players[i].isGrowing == false)
                {
                    players.Remove(players[i]);
                }

                if(players.Count == 1)
                {
                    WinState();
                }
            }
        }
    }

    

    void InitGame()
    {
    InputSystem.onAnyButtonPress.Call(
    confirm =>
    {
        winText.enabled = false;
        StartCoroutine(StartCountDown());
    });
    }
                    
    IEnumerator StartCountDown()
    {
        float duration = 5f; // 3 seconds you can change this to
                             //to whatever you want
        float totalTime = 0;
        while (totalTime <= duration)
        {
            totalTime += Time.deltaTime;
            var integer = (int)totalTime; /* choose how to quantize this */
                                          /* convert integer to string and assign to text */
            startText.text = "STARTING IN " + (duration - integer);
            yield return null;
        }

        StartGame();
    }

    void StartGame()
    {
        startText.enabled = false;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].isGrowing = true;
        }
        gameRunning = true;
        objTimer = 4;
    }

    void SpawnItems()
    {
        
        if(objTimer <= 0.0f)
        {
            int spawnPoint = Random.Range(0, objectSpawns.Count);
            if (objectSpawns[spawnPoint].transform.childCount == 0)
            {
                int obj = Random.Range(0, items.Count);

                var clone = Instantiate(items[obj], objectSpawns[spawnPoint].transform);
                objTimer = 4.0f;
            }
        }
        else
        {
            objTimer -= Time.deltaTime;
        }
    }

    void WinState()
    {
        Debug.Log("WIN");
        winText.enabled = true;
        winText.text = "PLAYER " + players[0].playerIndex + " WINS!";
    }
}
