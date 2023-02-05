using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RootController : MonoBehaviour
{
    public bool isGrowing = true;
    public RootPlayerActions playerActions;
    public int playerIndex;


    public float avgSpeed = 3;
    public float curSpeed = 4;

    public float curTurnSpeed = 5; 
    public float avgTurnSpeed = 5;


    private Vector2 input;

    List<GameObject> points;
    public GameObject guide;

    float curPointTimer = 0;
    float pointTimer = 0.2f;

    float powerUpTimer = 5.0f;
    bool isPoweredUp = false;

    // Start is called before the first frame update
    void Start()
    {
        playerActions = new RootPlayerActions();
        playerActions.Enable();


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


            //Point Dropping
            if (curPointTimer >= 0.0f)
            {
                curPointTimer -= Time.deltaTime;
            }
            else
            {

                dropPoint();


            }

        }
    }

    public void OnMove(InputAction.CallbackContext context) => input = context.ReadValue<Vector2>();

    public void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Death();
        }
        else
        {
            if (!isPoweredUp)
            {
                isPoweredUp = true;
                StartCoroutine(PowerTimer());
                if (isPoweredUp)
                {
                    switch (other.gameObject.tag)
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
                Destroy(other.gameObject);
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

    void dropPoint()
    {
        curPointTimer = pointTimer;
        Debug.Log("poop");
        Debug.Log(curPointTimer);

        var clone = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        clone.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        clone.transform.position = this.transform.position;
        clone.tag = "Obstacle";
        Physics.IgnoreCollision(clone.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>());
    }

}
