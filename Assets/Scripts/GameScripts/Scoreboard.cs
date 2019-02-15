using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Scoreboard : MonoBehaviour {

    public Text player1Count;
    public Text player2Count;
    public Text player1Score;
    public Text player2Score;

    //Display faulty throws
    public Text player1Fault;
    public Text player2Fault;

    //Display current player
    public Text currentPlayer;
    public Text currentPlayer2;

    //Scoreboard for second camera
    public Text player1Count2;
    public Text player2Count2;
    public Text player1Score2;
    public Text player2Score2;

    public Text player1Fault2;
    public Text player2Fault2;

    public int totalBalls = 6;
    int p1BallsLeft;
    int p2BallsLeft;

    public GameObject cont;
    Controller gameController;


    // Use this for initialization
    void Start () {

        player1Count = GameObject.Find("p1Balls").GetComponent<Text>();
        player2Count = GameObject.Find("p2Balls").GetComponent<Text>();
        player1Score = GameObject.Find("p1Score").GetComponent<Text>();
        player2Score = GameObject.Find("p2Score").GetComponent<Text>();

        player1Fault = GameObject.Find("p1Fault").GetComponent<Text>();
        player2Fault = GameObject.Find("p2Fault").GetComponent<Text>();

        //Scoreboard for second camera
        player1Count2 = GameObject.Find("p1Balls2").GetComponent<Text>();
        player2Count2 = GameObject.Find("p2Balls2").GetComponent<Text>();
        player1Score2 = GameObject.Find("p1Score2").GetComponent<Text>();
        player2Score2 = GameObject.Find("p2Score2").GetComponent<Text>();

        player1Fault2 = GameObject.Find("p1Fault2").GetComponent<Text>();
        player2Fault2 = GameObject.Find("p2Fault2").GetComponent<Text>();

        currentPlayer = GameObject.Find("currentPlayer").GetComponent<Text>();
        currentPlayer2 = GameObject.Find("currentPlayer2").GetComponent<Text>();

        //get the controller object
        cont = GameObject.FindGameObjectWithTag("GameController");
        gameController = cont.GetComponent<Controller>();
        UpdateScoreboard();
	}
	
    public void UpdateScoreboard()
    {
        //update balls left
        p1BallsLeft = totalBalls - gameController.redBalls;
        p2BallsLeft = totalBalls - gameController.greenBalls;

        // update the scores and balls thrown
        player1Count.text = GlobalVariables.player1 + " Balls Left: " + p1BallsLeft;
        player2Count.text = GlobalVariables.player2 + " Balls Left: " + p2BallsLeft;

        player1Score.text = GlobalVariables.player1 + " Score: " + gameController.player1Score;
        player2Score.text = GlobalVariables.player2 + " Score: " + gameController.player2Score;

        player1Fault.text = GlobalVariables.player1 + " Fault: " + gameController.redBallsFaulty;
        player2Fault.text = GlobalVariables.player2 + " Fault: " + gameController.greenBallsFaulty;

        //Second scoreboard
        player1Count2.text = GlobalVariables.player1 + " Balls Left: " + p1BallsLeft;
        player2Count2.text = GlobalVariables.player2 + " Balls Left: " + p2BallsLeft;

        player1Score2.text = GlobalVariables.player1 + " Score: " + gameController.player1Score;
        player2Score2.text = GlobalVariables.player2 + " Score: " + gameController.player2Score;

        player1Fault2.text = GlobalVariables.player1 + " Fault: " + gameController.redBallsFaulty;
        player2Fault2.text = GlobalVariables.player2 + " Fault: " + gameController.greenBallsFaulty;

        

        //Move text indicating the current player
        if (gameController.currentPlayer == 1)
        {
            //currentPlayer.rectTransform.anchoredPosition = new Vector3(-290, 0 , 0);
            //currentPlayer2.rectTransform.anchoredPosition = new Vector3(-290, 0, 0);
            currentPlayer.text = "Current Player: \n" + GlobalVariables.player1;
            currentPlayer2.text = "Current Player: \n" + GlobalVariables.player1;
        }
        else if(gameController.currentPlayer == 2)
        {
            //currentPlayer.rectTransform.anchoredPosition = new Vector3(290, 0, 0);
            //currentPlayer2.rectTransform.anchoredPosition = new Vector3(290, 0, 0);
            currentPlayer.text = "Current Player: \n" + GlobalVariables.player2;
            currentPlayer2.text = "Current Player: \n" + GlobalVariables.player2;
        }
    }
}
