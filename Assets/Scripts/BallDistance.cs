using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallDistance : MonoBehaviour {

    public Material redMaterial;
    public Material greenMaterial;
    GameObject jack;

    //Declare list of balls
    //public List<GameObject> ballList = new List<GameObject>();

    // Use this for initialization
    void Start () {
    }
   

    //Find the closest ball to the jack
    public int FindClosestBall()
    {
        jack = GameObject.FindWithTag("Jack");
        List<GameObject> ballList = new List<GameObject>();
        ballList.Clear();
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            ballList.Add(ball);
        }

        //Declaring variables
        float shortest = 20;
        GameObject closest = gameObject;
        int player = 0;

        //Iterate through list
        foreach (GameObject ball in ballList)
        {
            //Calculate distance between current ball and the jack
            float distance = Vector3.Distance(ball.transform.position, jack.transform.position);
            //print(ball.ToString() + distance);
            //If the current ball's distance is lower 
            if (distance < shortest)
            {
                //This is the closest ball
                shortest = distance;
                closest = ball;

                //get the script for the closest ball
                BallStats stats = closest.GetComponent<BallStats>();

                //if the ball was thrown by X player then return that value
                if (stats.getPlayer() == 1)
                {
                   player = 1;
                }
                else if (stats.getPlayer() == 2)
                {
                    player = 2;
                }
            }

            //DEBUG
            Debug.Log("Shortest distance: " + closest.ToString() + shortest);

        }
        return player;
    }

}
