using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RootController : MonoBehaviour
{
    public bool isGrowing = true;
    public RootPlayerActions playerActions;

    public int minSpeed = 2;
    public int maxSpeed = 5;
    public int turnSpeed = 5;

    public int curSpeed = 4;

    private Vector2 input;

    List<GameObject> points;
    public GameObject guide;
    
    

    float powerUpTimer = 5.0f;
    bool isPoweredUp = false;
    public Gamepad gamepad;

    // Start is called before the first frame update
    void Start()
    {
        playerActions = new RootPlayerActions();
        playerActions.Enable();
        curPointTimer = pointTimer;
        var gamepad = Gamepad.current;

        // joystickInput = new Vector2(0, 0);
    }

    private void OnEnable()
    {
       // playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrowing)
        {

            this.transform.Translate(this.gameObject.transform.forward * curSpeed * Time.deltaTime);

                float hAxis = input.x * turnSpeed * Time.deltaTime;
                transform.Rotate(0, hAxis, 0);

            
        }
    }

    public void OnMove(InputAction.CallbackContext context) => input = context.ReadValue<Vector2>();

}
