﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    //public fixed float positions[5];
    public float rotationSpeed = 20.0f;
    public float keyboardRotModifier = 2.0f;
    public float powerRotModifier = 1.1f;

    public GameObject arenaBoundary;
    Controller gameController;
    Throw throwClass;
    // Use this for initialization
    void Start()
    {
        //reset forward at start

        Debug.Log(transform.forward);
        arenaBoundary = GameObject.Find("ArenaWalls");

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>();
        throwClass = GameObject.FindGameObjectWithTag("Player").GetComponent<Throw>();
    }

    // Update is called once per frame
    void Update()
    {
        //If game is unpaused
        if (gameController.GetPlayRound() && !throwClass.ballThrown)
        {
            //When right is pressed rotate ball
            if (Input.GetKey("right") || Input.GetAxis("DPadX") == 1 || Input.GetAxis("MouseX") > 0)//|| Input.GetAxis("Joystick") > 0.5)
            {
                //Restrict how far user can rotate
                if (transform.localEulerAngles.y < 160 && transform.localEulerAngles.y > 10)
                {
                    //adjust rotation for keyboard
                    if (Input.GetKey("right"))
                    {

                        transform.Rotate(0.0f, (rotationSpeed * keyboardRotModifier * powerRotModifier) * Time.deltaTime, 0.0f);
                    }
                    else
                    {
                        transform.Rotate(0.0f, (rotationSpeed * powerRotModifier) * Time.deltaTime, 0.0f);
                    }
                    //Show "arrow" to indicate where ball is pointing
                    //arrow.SetActive(true);
                }
            }
            //When left is pressed rotate in opposite direction
            if (Input.GetKey("left") || Input.GetAxis("DPadX") == -1 || Input.GetAxis("MouseX") < 0) //|| Input.GetAxis("Joystick") < -0.5)
            {
                //Restrict how far user can rotate
                if (transform.localEulerAngles.y < 170 && transform.localEulerAngles.y > 20)
                {
                    //Show arrow to indicate where ball is pointing
                    //adjust rotation for keyboard
                    if (Input.GetKey("left"))
                    {

                        transform.Rotate(0.0f, (-rotationSpeed * keyboardRotModifier * powerRotModifier) * Time.deltaTime, 0.0f);
                    }
                    else
                    {
                        transform.Rotate(0.0f, (-rotationSpeed * powerRotModifier) * Time.deltaTime, 0.0f);
                    }
                }
            }        
        }
    }
}
