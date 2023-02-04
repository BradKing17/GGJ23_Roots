using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class InputManager : MonoBehaviour
{
    public string action;
    public int directionX = 0;
    public int directionY = 0;
    public Text playersText;

    public GameObject rootObj;
    public GameObject[] roots;


    // Start is called before the first frame update
    void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
        AirConsole.instance.onConnect += OnConnect;
        AirConsole.instance.onDisconnect += OnDisconnect;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playersText.text = "PLAYERS: " + AirConsole.instance.GetActivePlayerDeviceIds.Count;
        this.gameObject.transform.Translate(new Vector3(directionX, directionY));
    }

    void OnMessage(int device_id, JToken data)
    {
        int player_id = AirConsole.instance.ConvertDeviceIdToPlayerNumber(device_id);
        if(player_id != -1)
        {

        }
    }

    void OnConnect(int device_id)
    {
        if (AirConsole.instance.GetActivePlayerDeviceIds.Count == 0)
        {
            if (AirConsole.instance.GetControllerDeviceIds().Count >= 2)
            {
                StartGame();
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
        Debug.Log("STARTING");
        for(int i = 0; i < AirConsole.instance.GetControllerDeviceIds().Count; i++)
        {
            var clone = Instantiate(rootObj);
        }
    
    }
    void ResetGame()
    {

    }
}
