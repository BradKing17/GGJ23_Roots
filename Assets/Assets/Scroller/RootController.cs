using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    bool isGrowing = true;

    public int minSpeed = 2;
    public int maxSpeed = 5;
    public int turnSpeed = 5;

    public int curSpeed = 4;

    List<GameObject> points;
    public GameObject guide;
    public float pointTimer = 0.2f;
    float curPointTimer = 0.0f;

    float powerUpTimer = 5.0f;
    bool isPoweredUp = false;

    // Start is called before the first frame update
    void Start()
    {
        curPointTimer = pointTimer;
        // joystickInput = new Vector2(0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGrowing)
        {
            //Movement

            float hAxis = -Input.GetAxis("Horizontal") * -turnSpeed * Time.deltaTime;

            transform.Rotate(0, hAxis, 0);
            this.transform.Translate(this.transform.forward * curSpeed * Time.deltaTime);


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

    void DropPoint()
    {
        curPointTimer = pointTimer;
        Debug.Log("poop");
        Debug.Log(curPointTimer);

        var clone = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        clone.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        clone.transform.position = this.transform.position;
        Physics.IgnoreCollision(clone.GetComponent<Collider>(), GetComponent<Collider>());
    }

}
