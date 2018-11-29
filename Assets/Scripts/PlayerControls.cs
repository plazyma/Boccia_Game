using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    //public fixed float positions[5];
    public float rotationSpeed = 20.0f;
    public float keyboardRotModifier = 8.0f;
    

    // Use this for initialization
    void Start () {
        //reset forward at start
        
        Debug.Log(transform.forward);

    }
	
	// Update is called once per frame
	void Update () {
        //When right is pressed rotate ball
        if (Input.GetKeyDown("right") || Input.GetAxis("DPadX") == 1 || Input.GetAxis("MouseX") > 0)
        {
            //Restrict how far user can rotate
            if (transform.localEulerAngles.y < 160 && transform.localEulerAngles.y > 10)
            {
                //adjust rotation for keyboard
                if (Input.GetKeyDown("right"))
                {

                    transform.Rotate(0.0f, rotationSpeed* keyboardRotModifier * Time.deltaTime, 0.0f);
                }
                else
                {
                    transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f);
                }
                //Show "arrow" to indicate where ball is pointing
                //arrow.SetActive(true);
               
                //DEBUG
                print(transform.eulerAngles.y);
            }
        }
        //When left is pressed rotate in opposite direction
        if (Input.GetKeyDown("left") || Input.GetAxis("DPadX") == -1 || Input.GetAxis("MouseX") < 0)
        {
            //Restrict how far user can rotate
            if (transform.localEulerAngles.y < 170 && transform.localEulerAngles.y > 20)
            {
                //Show arrow to indicate where ball is pointing
                //arrow.SetActive(true);
                //adjust rotation for keyboard
                if (Input.GetKeyDown("left"))
                {

                    transform.Rotate(0.0f, -rotationSpeed * keyboardRotModifier * Time.deltaTime, 0.0f);
                }
                else
                {
                    transform.Rotate(0.0f, -rotationSpeed * Time.deltaTime, 0.0f);
                }
                //DEBUG
                print(transform.eulerAngles.y);
                
            }
        }



        //reset mouse position to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }
}
