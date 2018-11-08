using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    //public fixed float positions[5];


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //When right is pressed rotate ball
        if (Input.GetKeyDown("right"))
        {
            //Restrict how far user can rotate
            if (transform.rotation.eulerAngles.y < 90.0f || transform.rotation.eulerAngles.y > 250.0f)
            {
                //Show "arrow" to indicate where ball is pointing
                //arrow.SetActive(true);
                transform.Rotate(0.0f, 10.0f, 0.0f);
                //DEBUG
                print(transform.eulerAngles.y);
            }
        }
        //When left is pressed rotate in opposite direction
        if (Input.GetKeyDown("left"))
        {
            //Restrict how far user can rotate
            if (transform.rotation.eulerAngles.y < 120.0f || transform.rotation.eulerAngles.y > 270.0f)
            {
                //Show arrow to indicate where ball is pointing
                //arrow.SetActive(true);
                transform.Rotate(0.0f, -10.0f, 0.0f);
                //DEBUG
                print(transform.eulerAngles.y);
            }
        }

    }
}
