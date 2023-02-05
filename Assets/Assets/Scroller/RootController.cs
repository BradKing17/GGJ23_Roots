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
    public float pointTimer = 0.2f;
    float curPointTimer = 0.0f;

    float powerUpTimer = 5.0f;
    bool isPoweredUp = false;
    public Gamepad gamepad;

    // Start is called before the first frame update
    void Start()
    {
        playerActions = new RootPlayerActions();
        playerActions.Enable();
        curPointTimer = pointTimer;

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



            //Point Dropping
            if (curPointTimer >= 0.0f)
            {
                curPointTimer -= Time.deltaTime;
            }
            else
            {

                DropPoint();


            }
        }
    }

    public void OnMove(InputAction.CallbackContext context) => input = context.ReadValue<Vector2>();

    void DropPoint()
    {
        curPointTimer = pointTimer;

        var clone = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        clone.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        clone.transform.position = this.transform.position;
        Physics.IgnoreCollision(clone.GetComponent<Collider>(), GetComponent<Collider>());
    }

}
