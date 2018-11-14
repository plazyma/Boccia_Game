﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public int currentPlayer;
    public bool jackThrown = false;
    public bool firstThrows = true;

    public GameObject jack;
    public GameObject redBall;
    public GameObject greenBall;
    GameObject newBall;
    public GameObject player;
    public Color colour = Color.black;

    public Throw throwScript;
    public PlayerControls pControls;
    public BallDistance dist;

    public Material redMaterial; 
    public Material greenMaterial;

    public List<GameObject> ballList;

    //debug
    public int amountOfBalls = 0;
    public int greenBalls = 0;
    public int redBalls = 0;




	// Use this for initialization
	void Start () {

        //set random player at start 0-100 for larger random chances
        currentPlayer = Random.Range(0, 100);
        if (currentPlayer < 50)
        {
            currentPlayer = 1;
        }
        else
        {
            currentPlayer = 2;
        }

        //get scripts from player and throw
        throwScript = player.GetComponent<Throw>();
        pControls = player.GetComponent<PlayerControls>();
        

        //setup player for spawning balls
        Quaternion spawnRotation = Quaternion.identity;
        Vector3 playerForward = new Vector3(player.transform.forward.x * 2, player.transform.forward.y*2, player.transform.forward.z * 2);
        Vector3 playerTransform = player.transform.position;

        //spawn jack on initial run time
        Instantiate(jack, player.transform.position + playerForward ,spawnRotation);
        dist = jack.GetComponent<BallDistance>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (throwScript.jackThrown && !jackThrown)
        {
            //after 4 seconds have passed since throwing
            if (Time.time- throwScript.shotTime > 4)
            {

                //set jack as thrown on this script, create a new ball and tell throw that there is a new ball
                jackThrown = true;
                if (currentPlayer == 1)
                {
                    newBall = Instantiate(redBall, player.transform.position + (player.transform.forward * 2), player.transform.rotation);
                    redBalls ++;
                    
                }
                else if (currentPlayer == 2)
                {
                    newBall = Instantiate(greenBall, player.transform.position + (player.transform.forward * 2), player.transform.rotation);
                    greenBalls ++;
                }
                ballList.Add(newBall);
                //give throwscript the new ball
                throwScript.setBall(newBall);
               
            }          
        }
        //spawn a new ball
        spawnBall();
        

        //10 balls have been thrown
        if (amountOfBalls > 9)
        {
            checkWinner();
        }
	}
    public void spawnBall()
    {
        if (amountOfBalls > 0 && throwScript.ballThrown && Time.time - throwScript.shotTime > 4)
        {
            if (amountOfBalls > 1)
            {
                //check distance
                currentPlayer = playerSelection();
                if (redBalls > 4)
                {
                    print("all reds thrown");
                    currentPlayer = 1;
                }
                if (greenBalls > 4)
                {
                    print("all greens thrown");
                    currentPlayer = 2;
                }
            }


            if (currentPlayer == 1)
            {
                
                currentPlayer = 2;

                //create a new ball and tell throw that there is a new ball
                newBall = Instantiate(greenBall, player.transform.position + (player.transform.forward * 2), player.transform.rotation);
                greenBalls++;
            }
            else if (currentPlayer == 2)
            {
                currentPlayer = 1;

                //create a new ball and tell throw that there is a new ball
                newBall = Instantiate(redBall, player.transform.position + (player.transform.forward * 2), player.transform.rotation);
                redBalls++;
            }



            ballList.Add(newBall);


            //give throwscript the new ball
            throwScript.setBall(newBall);
        }
    }

    public int playerSelection()
    {
        //make sure there is a green and red ball on the field before checking distances
        if (dist.FindClosestBall() == 1)
        {
            print("Player 1 is closest, returning player 1");
            return 1;
        }
        else if (dist.FindClosestBall() == 2)
        {
            print("Player 2 is closest, returning player 2");
            return 2;
        }
        else
        {
            print("Something went wrong");            
        }
        
        return 0;
    }

    public void checkWinner()
    {
        //do win stuff
    }
}

