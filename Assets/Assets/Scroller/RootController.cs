using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RootController : MonoBehaviour
{
    bool isGrowing = true;
    public RootPlayerActions playerActions;

    public int minSpeed = 2;
    public int maxSpeed = 5;
    public int turnSpeed = 5;

    public int curSpeed = 4;

    List<GameObject> points;
    public GameObject guide;
    
    

    float powerUpTimer = 5.0f;
    bool isPoweredUp = false;

    // Start is called before the first frame update
    void Start()
    {
        playerActions = new RootPlayerActions();
        playerActions.Enable();
        var gamepad = Gamepad.current;

        // joystickInput = new Vector2(0, 0);
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGrowing)
        {
            //Movement
            Vector2 input = playerActions.Player.Movement.ReadValue<Vector2>();
            Debug.Log(input);
            float hAxis = input.x  * turnSpeed * Time.deltaTime;

            transform.Rotate(0, hAxis, 0);
            this.transform.Translate(this.transform.forward * curSpeed * Time.deltaTime);


            
        }
    }

    

}
