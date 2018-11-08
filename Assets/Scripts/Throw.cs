using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {
    //Initializing
    public float power;
    public Rigidbody rb;
    public GameObject player;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        power = 0;

        transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update () {
        //while space is pressed increase power
        //TODO - Change how fast power is increased
        if (Input.GetKey("space"))
        {
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
            rb.AddForce(power, 0, -power * (player.transform.rotation.y), ForceMode.Impulse);
            rb.useGravity = true;
        }
    }
}
