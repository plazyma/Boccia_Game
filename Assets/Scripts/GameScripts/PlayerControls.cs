using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    //public fixed float positions[5];
    public float rotationSpeed = 20.0f;
    public float keyboardRotModifier = 2.0f;
    public float powerRotModifier = 1.1f;

    public GameObject arenaBoundary;
    Controller gameController;
    Throw throwClass;
    // Use this for initialization
    void Start () {
        //reset forward at start
        
        Debug.Log(transform.forward);
        arenaBoundary = GameObject.Find("ArenaWalls");

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>();
        throwClass = GameObject.FindGameObjectWithTag("Player").GetComponent<Throw>();
    }
	
	// Update is called once per frame
	void Update () {
        //If game is unpaused
        if (gameController.GetPlayRound() && !throwClass.ballThrown)
        {
            //When right is pressed rotate ball
            if (Input.GetAxis("Turn") > 0.8)
            {
                //Restrict how far user can rotate
                if (transform.localEulerAngles.y < 160 && transform.localEulerAngles.y > 10)
                {
                       transform.Rotate(0.0f, (rotationSpeed * powerRotModifier) * Time.deltaTime , 0.0f);
                }
            }
            if(Input.GetAxis("Turn (Keyboard)") > 0)
            {
                if(transform.localEulerAngles.y < 160 && transform.localEulerAngles.y > 10)
                {
                    transform.Rotate(0.0f, (rotationSpeed * keyboardRotModifier * powerRotModifier) * Time.deltaTime, 0.0f);
                }    
            }
            //When left is pressed rotate in opposite direction
            if (Input.GetAxis("Turn") < -0.8)
            {
                //Restrict how far user can rotate
                if (transform.localEulerAngles.y < 170 && transform.localEulerAngles.y > 20)
                {
                    transform.Rotate(0.0f, (-rotationSpeed * powerRotModifier) * Time.deltaTime, 0.0f);
                }
            }
            if(Input.GetAxis("Turn (Keyboard)") < 0)
            {
                if (transform.localEulerAngles.y < 170 && transform.localEulerAngles.y > 20)
                {
                    transform.Rotate(0.0f, (-rotationSpeed * keyboardRotModifier * powerRotModifier) * Time.deltaTime, 0.0f);
                }
            }

            if (Input.GetKeyDown("v"))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetKeyDown("/"))
            {
                if (arenaBoundary.activeSelf)
                {
                    arenaBoundary.SetActive(false);
                }
                else
                {
                    arenaBoundary.SetActive(true);
                }
            }


        }



        //reset mouse position to the center of the screen
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
}
