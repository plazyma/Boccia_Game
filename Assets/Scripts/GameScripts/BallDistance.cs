using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BallDistance : MonoBehaviour {

    public Material redMaterial;
    public Material greenMaterial;
    GameObject jack;
    public List<GameObject> ballList = new List<GameObject>();
    GameObject closestBall;

    // Use this for initialization
    void Start () {
        
    }

    //Find the closest ball to the jack
    public int FindClosestBall()
    {
        jack = GameObject.FindWithTag("Jack");
        ballList.Clear();
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            ballList.Add(ball);
        }

        //Declaring variables
        float shortest = 200;
        closestBall = gameObject;
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
                closestBall = ball;

                //get the script for the closest ball
                BallStats stats = closestBall.GetComponent<BallStats>();

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
            Debug.Log("Shortest distance: " + closestBall.ToString() + shortest);

        }
        return player;
    }

    //Struct to hold the ball and its distance to the jack
    struct BallData
    {
        public GameObject ball;
        public float distanceToJack;
    }

    //Calculate the score
    public int CalculateScore()
    {
        int score = 0;

        BallData ballData = new BallData();
        List<BallData> balls = new List<BallData>();

        //Loop through list of balls
        foreach(GameObject ball in ballList)
        {
            //Calculate distance to jack
            float distance = Vector3.Distance(ball.transform.position, jack.transform.position);

            //Add to list of struct to hold the ball and its distance
            ballData.ball = ball;
            ballData.distanceToJack = distance;

            balls.Add(ballData);
        }

        //Order the list by distance
        balls = balls.OrderBy(e => e.distanceToJack).ToList();

        //Loop list
        foreach(BallData ball in balls)
        {
            //If the current ball was thrown by the same player as the closest ball
            if(ball.ball.GetComponent<BallStats>().getPlayer() == closestBall.GetComponent<BallStats>().getPlayer())
            {
                //Increase score
                score++;
            }
            else
            {
                //Quit loop
                break;
            }
        }

        return score;
    }

}
