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
    
    

    float powerUpTimer = 5.0f;
    bool isPoweredUp = false;

    // Start is called before the first frame update
    void Start()
    {
        
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


            
        }
    }

    

}
