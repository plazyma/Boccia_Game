using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {
    //Initializing
    float power;
    float power_z;
    Rigidbody rb;
    public GameObject ball;
    Vector3 startPosition;
    // Use this for initialization
    void Start () {
        rb = ball.GetComponent<Rigidbody>();

        rb.useGravity = false;
        power = 0.0f;

        startPosition = new Vector3(-17.73f, 1.68f, 2.38f);
    }

    // Update is called once per frame
    void Update () {
        //Increase x - power by 1
        if (Input.GetKeyDown("up"))
        {
            if (power < 10)
            {
                power += 1.0f;

                print(power);
            }
        }
        //Decrease x - power by 1
        if(Input.GetKeyDown("down"))
        {
            if(power > 0)
            {
                power -= 1.0f;
                print(power);
            }
        }
        //Decrease z - power by 1
        if(Input.GetKeyDown("left"))
        {
            if (power_z > -6)
            {
                power_z -= 1.0f;
                print(power_z);
            }
        }
        //Increase z - power by 1
        if(Input.GetKeyDown("right"))
        {
            if(power_z < 6)
            {
                power_z += 1.0f;
                print(power_z);
            }
        }
        //Apply force on release
        if (Input.GetKeyDown("space"))
        {
            //Apply force to ball
            //Z - Force based on rotation
            rb.AddForce(power, 0, -power_z, ForceMode.Impulse);

            rb.useGravity = true;
        }
        //Reset position of the ball
        if(Input.GetKeyDown("r"))
        {
            //Set velocity to 0
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);

            //rest position
            ball.transform.position = startPosition;

            rb.useGravity = false;

            power = 0.0f;
        }
    }

    //Return the power
    public float getPower()
    {
        return power;
    }
}
