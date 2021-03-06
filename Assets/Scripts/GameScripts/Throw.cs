﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Throw : MonoBehaviour
{
    //Initializing
    float power;
    Rigidbody rb;
    public GameObject ball;
    //Vector3 startPosition;

    public GameObject gameController;
    Controller cont;
    public GameObject player;

    public AudioSource audioSource;
    public AudioClip ballThrowSound;

    public float shotTime;
    public bool ballFound = false;
    public bool ballThrown = false;
    public bool jackThrown = false;

    public List<AudioClip> powerBarAudioClips = new List<AudioClip>();

    public AimAssist aimAssistScript;

    public Powerbar powerBar;
    public Powerbar powerBar2;

    Scoreboard scoreBoardScript;
    Scoreboard scoreBoardScript2;

    // Use this for initialization
    void Start()
    {
        //get controller, player and some scripts
        gameController = GameObject.FindWithTag("GameController");
        cont = gameController.gameObject.GetComponent<Controller>();
        player = GameObject.FindGameObjectWithTag("Player");

        //rb.useGravity = false;
        power = 0.0f;

        // startPosition = new Vector3(-17.73f, 1.68f, 2.38f);
        loadSound();

        if (!aimAssistScript)
        {
            aimAssistScript = GetComponent<AimAssist>();
        }

        if (!powerBar)
        {
            powerBar = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<Powerbar>();
        }
        scoreBoardScript = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<Scoreboard>();
    }

    void loadSound()
    {
        //Load all sound clips
        Object[] powerBarSounds = Resources.LoadAll("Power", typeof(AudioClip));

        //Populate list
        foreach (Object sound in powerBarSounds)
        {
            powerBarAudioClips.Add((AudioClip)sound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If list of sounds is empty, populate it
        if (powerBarAudioClips.Count == 0)
        {
            loadSound();
        }

        //if there is no ball yet
        if (ballFound == false)
        {
            if (jackThrown == false)
            {
                ball = GameObject.FindGameObjectWithTag("Jack");
                rb = ball.GetComponent<Rigidbody>();
            }
            //Update powerbar
            powerBar.updatePowerBar();
        }
        //if ball isn't thrown
        if (ballThrown == false)
        {

            //update balls position until thrown
            ball.transform.position = transform.position + (transform.forward * 2);
            ball.transform.rotation = transform.rotation;

            //If game is unpaused
            if (cont.GetPlayRound())
            {
                //Increase x - power by 1
                if (Input.GetKeyDown("up") || Input.GetButtonDown("B") || Input.GetAxis("MouseScrollWheel") > 0)
                {
                    if (power < 10)
                    {
                        //Convert power from float to int
                        float current = power * 2;
                        int currentB = (int)current;

                        //Set clip based on power
                        audioSource.clip = powerBarAudioClips[currentB];
                        //Play
                        audioSource.Play();

                        //Increase power
                        power += 1.0f;

                        //Update aim assist
                        aimAssistScript.CalculateAimIncreased();

                        //reduce player turning speed based on power
                        player.GetComponent<PlayerControls>().powerRotModifier -= 0.1f;
                    }
                    //Update powerbar
                    powerBar.updatePowerBar();
                }
                //Decrease x - power by 1
                if (Input.GetKeyDown("down") || Input.GetButtonDown("A") || Input.GetAxis("MouseScrollWheel") < 0)
                {
                    if (power > 0)
                    {
                        //Decrease power
                        power -= 1.0f;

                        //Convert power from float to int
                        float current = power * 2;
                        int currentB = (int)current;

                        //Set clip based on power
                        audioSource.clip = powerBarAudioClips[currentB];
                        //Play
                        audioSource.Play();

                        //Update aim assist
                        aimAssistScript.CalculateAimReduced();

                        //increase player turning speed based on power
                        player.GetComponent<PlayerControls>().powerRotModifier += 0.1f;
                    }
                    //Update powerbar
                    powerBar.updatePowerBar();
                }

                //Apply force on release
                if (Input.GetKeyDown("space") || Input.GetButtonDown("X") || Input.GetButtonDown("LeftMouseButton"))
                {
                    //rb.useGravity = true;
                    //Apply force to ball
                    //Z - Force based on rotation
                    //Steve - added more force due to mass changes for phsyics
                    //rb.AddForce(power*4, 0, -power_z*4, ForceMode.Impulse);
                    rb.AddForce(transform.forward * power * 400);
                    rb.useGravity = true;
                    if (ball.tag == "Jack")
                    {
                        jackThrown = true;
                    }
                    else
                    {
                        cont.amountOfBalls++;
                    }
                    //ball is thrown and can't be touched
                    ballThrown = true;
                    shotTime = Time.time;
                    audioSource.clip = ballThrowSound;
                    audioSource.Play();

                    //Update scoreboard
                    scoreBoardScript.UpdateScoreboard();

                    //reset player power modifier
                    player.GetComponent<PlayerControls>().powerRotModifier = 1.1f;
                }
            }

        }
    }

    //Return the power
    public float getPower()
    {
        return power;
    }

    public void setPower(float pow)
    {
        power = pow;
    }

    //set the new ball to throw
    public void setBall(GameObject instBall)
    {
        ball = instBall;

        rb = ball.GetComponent<Rigidbody>();

        ballThrown = false;

        //Reset aim assist
        aimAssistScript.ResetAim();

        //Update powerbar
        powerBar.updatePowerBar();


    }
}
