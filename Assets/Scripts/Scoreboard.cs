using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Scoreboard : MonoBehaviour {

    public Text player1Count;
    public Text player2Count;
    public Text player1Score;
    public Text player2Score;

    public GameObject cont;
    Controller gameContoller;


    // Use this for initialization
    void Start () {

    player1Count = GameObject.Find("p1Balls").GetComponent<Text>();
    player2Count = GameObject.Find("p2Balls").GetComponent<Text>();
    player1Score = GameObject.Find("p1Score").GetComponent<Text>();
    player2Score = GameObject.Find("p2Score").GetComponent<Text>();
    

    //get the controller object
    cont = GameObject.FindGameObjectWithTag("GameController");
    gameContoller = cont.GetComponent<Controller>();
    UpdateScoreboard();
	}
	
    public void UpdateScoreboard()
    {
        // update the scores and balls thrown
        player1Count.text = "Player1 Balls Thrown: " + gameContoller.redBalls;
        player2Count.text = "Player2 Balls Thrown: " + gameContoller.greenBalls;

        player1Score.text = "Player1 Score: " + gameContoller.player1Score;
        player2Score.text = "Player2 Score: " + gameContoller.player2Score;
    }
}
