using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RootController : MonoBehaviour
{
    public bool isGrowing = true;
    public RootPlayerActions playerActions;


    public float avgSpeed = 3;
    public float curSpeed = 4;

    public float curTurnSpeed = 5; 
    public float avgTurnSpeed = 5;


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

                float hAxis = input.x * curTurnSpeed * Time.deltaTime;
                transform.Rotate(0, hAxis, 0, Space.Self);

            
        }
    }

    public void OnMove(InputAction.CallbackContext context) => input = context.ReadValue<Vector2>();

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            Death();
        }
        isPoweredUp = true;
        StartCoroutine(PowerTimer());
        if (isPoweredUp)
        {
            switch (other.tag)
            {
                case "Fertilizer":
                    curSpeed = curSpeed * 2.0f;
                    curTurnSpeed = curTurnSpeed * 2.0f;
                    break;

                case "Water":
                    break;

                case "Toxic":
                    curSpeed = curSpeed * 0.5f;
                    curTurnSpeed = curTurnSpeed * 0.5f;
                    break;

            }
        }
    }

    void Death()
    {
        OnDisable();
        isGrowing = false;
    }

    IEnumerator PowerTimer()
    {

        yield return new WaitForSeconds(powerUpTimer);
        isPoweredUp = false;
        curSpeed = avgSpeed;
        curTurnSpeed = avgTurnSpeed;

    }

}
