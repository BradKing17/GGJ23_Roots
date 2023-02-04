using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    bool isGrowing = true;

    
    public int speed = 5;
    public int turnSpeed = 5;


    List<Vector2> points;
    public GameObject guide;

    // Start is called before the first frame update
    void Start()
    {
        
       // joystickInput = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrowing)
        {
            float hAxis = -Input.GetAxis("Horizontal") * -turnSpeed * Time.deltaTime;

            transform.Rotate(0, hAxis, 0);
            this.transform.Translate(this.transform.forward * speed * Time.deltaTime);
        }
    }

    void dropPoint()
    {

    }
}
