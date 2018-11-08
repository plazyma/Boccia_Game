using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {
    //Initializing
    public float power;
    public Rigidbody rb;
    public GameObject arrow;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        power = 0;

        transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        //while space is pressed increase power
        //TODO - Change how fast power is increased
        if (Input.GetKey("space"))
        {
            arrow.SetActive(false);
            while (power < 10)
            {
                power += 0.1f;

                print(power);
            }
        }
        //Apply force on release
        if(Input.GetKeyUp("space"))
        {
            //Apply force to ball
            //Z - Force based on rotation
            //TODO - Update how z force is calculated
            rb.AddForce(power, 0, -power * (transform.rotation.y), ForceMode.Impulse);
            rb.useGravity = true;
        }

        //When right is pressed rotate ball
        if(Input.GetKeyDown("right"))
        {
            //Restrict how far user can rotate
           if (transform.rotation.eulerAngles.y < 90.0f || transform.rotation.eulerAngles.y > 250.0f)
            {
                //Show "arrow" to indicate where ball is pointing
                arrow.SetActive(true);
                transform.Rotate(0.0f, 10.0f, 0.0f);
                //DEBUG
                print(transform.eulerAngles.y);
            }
        }
        //When left is pressed rotate in opposite direction
        if(Input.GetKeyDown("left"))
        {
            //Restrict how far user can rotate
            if (transform.rotation.eulerAngles.y < 120.0f || transform.rotation.eulerAngles.y > 270.0f)
            {
                //Show arrow to indicate where ball is pointing
                arrow.SetActive(true);
                transform.Rotate(0.0f, -10.0f, 0.0f);
                //DEBUG
                print(transform.eulerAngles.y);
            }
        }
    }
}
