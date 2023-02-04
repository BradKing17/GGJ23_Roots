using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyController : MonoBehaviour
{
    List<int> gamepads; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputSystem.onDeviceChange +=
            (device, change) =>
            {
                switch (change)
                {
                    case InputDeviceChange.Added:
                        break;

                    case InputDeviceChange.Disconnected:
                        break;

                    case InputDeviceChange.Reconnected:
                        break;

                    case InputDeviceChange.Removed:
                        break;

                    default:

                        break;

                }
            };

    }
}
