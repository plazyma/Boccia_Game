using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDistance : MonoBehaviour {

    //Declare list of balls
    public List<GameObject> ballList = new List<GameObject>();

    // Use this for initialization
    void Start () {
        //Loop through all objects with tag "Ball" and add them to the list
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            ballList.Add(ball);
        }
    }
   
	// Update is called once per frame
	void Update () {
        FindClosestBall();
    }

    //Find the closest ball to the jack
    void FindClosestBall()
    {
        //Declaring variables
        float shortest = 10;
        GameObject closest = gameObject;

        //Iterate through list
        foreach (GameObject ball in ballList)
        {
            //Calculate distance between current ball and the jack
            float distance = Vector3.Distance(ball.transform.position, transform.position);

            //If the current ball's distance is lower 
            if (distance < shortest)
            {
                //This is the closest ball
                shortest = distance;
                closest = ball;
            }

            //DEBUG
            Debug.Log("Shortest distance: " + closest.ToString() + shortest);
        }
    }
}


//DIFFERENTIATING BETWEEN TEAMS
//MULTIPLE TAGS IN ONE TAG
//USE PARENT/CHILD OBJECTS