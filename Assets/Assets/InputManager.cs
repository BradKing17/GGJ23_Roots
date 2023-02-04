using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class InputManager : MonoBehaviour
{
    public bool gameStarted = false;

    public Text playersText;
    public GameObject rootObj;
    public List<GameObject> roots;

    

    // Start is called before the first frame update
    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMessage(int device_id, JToken data)
    {
        Debug.Log(device_id + ", " + (float)data["move"]);
        int player_id = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);
        if(player_id != -1)
        {
            roots[player_id].transform.position += new Vector3(0, (float)data["move"], 0);
        }
    }

    void OnConnect(int device_id)
    {
        if (!gameStarted)
        {
            if (AirConsole.instance.GetActivePlayerDeviceIds.Count == 0)
            {
                playersText.text = "PLAYERS: " + AirConsole.instance.GetControllerDeviceIds().Count.ToString();
                if (AirConsole.instance.GetControllerDeviceIds().Count >= 3)
                {
                    StartGame();
                }
            }
        }
    }

    void OnDisconnect(int device_id)
    {
        int active_player = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);
        if (active_player != -1)
        {
            if (AirConsole.instance.GetControllerDeviceIds().Count >= 2)
            {
                
            }
            else
            {
                AirConsole.instance.SetActivePlayers(0);
                ResetGame();
                playersText.text = "PLAYER LEFT - NEED MORE PLAYERS";
            }
        }
    }
    void StartGame()
    {
        gameStarted = true;
        Debug.Log("STARTING");
        for(int i = 0; i < AirConsole.instance.GetControllerDeviceIds().Count; i++)
        {
            var clone = Instantiate(rootObj);
            roots.Add(clone);
        }
    
    }
    void ResetGame()
    {

    }
}
