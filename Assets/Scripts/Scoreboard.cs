using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Scoreboard : MonoBehaviour {

    public Text player1Count;
    public Text player2Count;
    public Text player1Score;
    public Text player2Score;

    //Scoreboard for second camera
    public Text player1Count2;
    public Text player2Count2;
    public Text player1Score2;
    public Text player2Score2;

    public int totalBalls = 6;
    int p1BallsLeft;
    int p2BallsLeft;

    public GameObject cont;
    Controller gameContoller;


    // Use this for initialization
    void Start () {

        player1Count = GameObject.Find("p1Balls").GetComponent<Text>();
        player2Count = GameObject.Find("p2Balls").GetComponent<Text>();
        player1Score = GameObject.Find("p1Score").GetComponent<Text>();
        player2Score = GameObject.Find("p2Score").GetComponent<Text>();

        //Scoreboard for second camera
        player1Count2 = GameObject.Find("p1Balls2").GetComponent<Text>();
        player2Count2 = GameObject.Find("p2Balls2").GetComponent<Text>();
        player1Score2 = GameObject.Find("p1Score2").GetComponent<Text>();
        player2Score2 = GameObject.Find("p2Score2").GetComponent<Text>();

        //get the controller object
        cont = GameObject.FindGameObjectWithTag("GameController");
        gameContoller = cont.GetComponent<Controller>();
        UpdateScoreboard();
	}
	
    public void UpdateScoreboard()
    {
        //update balls left
        p1BallsLeft = totalBalls - gameContoller.redBalls;
        p2BallsLeft = totalBalls - gameContoller.greenBalls;

        // update the scores and balls thrown
        player1Count.text = "Player1 Balls Left: " + p1BallsLeft;
        player2Count.text = "Player2 Balls Left: " + p2BallsLeft;

        player1Score.text = "Player1 Score: " + gameContoller.player1Score;
        player2Score.text = "Player2 Score: " + gameContoller.player2Score;

        //Second scoreboard
        player1Count2.text = "Player1 Balls Left: " + p1BallsLeft;
        player2Count2.text = "Player2 Balls Left: " + p2BallsLeft;

        player1Score2.text = "Player1 Score: " + gameContoller.player1Score;
        player2Score2.text = "Player2 Score: " + gameContoller.player2Score;
    }
}
