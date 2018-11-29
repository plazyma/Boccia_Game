using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    //public fixed float positions[5];

    
	// Use this for initialization
	void Start () {
        //reset forward at start
        
        Debug.Log(transform.forward);

    }
	
	// Update is called once per frame
	void Update () {
        print(Input.GetAxis("MouseX"));
        //When right is pressed rotate ball
        if (Input.GetKeyDown("right") || Input.GetAxis("DPadX") == 1 || Input.GetAxis("MouseX") > 0)
        {
            //Restrict how far user can rotate
            if (transform.rotation.eulerAngles.y < 160.0f || transform.rotation.eulerAngles.y > 10.0f)
            {
                //Show "arrow" to indicate where ball is pointing
                //arrow.SetActive(true);
                transform.Rotate(0.0f, 2.0f, 0.0f);
                //DEBUG
                //print(transform.eulerAngles.y);
                print(Input.GetAxis("MouseX"));
            }
        }
        //When left is pressed rotate in opposite direction
        if (Input.GetKeyDown("left") || Input.GetAxis("DPadX") == -1 || Input.GetAxis("MouseX") < 0)
        {
            //Restrict how far user can rotate
            if (transform.rotation.eulerAngles.y < 170.0f || transform.rotation.eulerAngles.y > 20.0f)
            {
                //Show arrow to indicate where ball is pointing
                //arrow.SetActive(true);
                transform.Rotate(0.0f, -2.0f, 0.0f);
                //DEBUG
                //print(transform.eulerAngles.y);
                print(Input.GetAxis("MouseX"));
            }
        }
    }
}
