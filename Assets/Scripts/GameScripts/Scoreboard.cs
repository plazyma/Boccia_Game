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

    //Orange ball counter UI components
    public List<Image> orangeBalls;
    public List<Image> orangeBalls2;

    //Blue ball counter UI components
    public List<Image> blueBalls;
    public List<Image> blueBalls2;

    //Object representing team logos
    public Image player1Logo;
    public Image player2Logo;

    //Sprites of balls for counter
    public Sprite orangeBallSprite;
    public Sprite blueBallSprite;
    public Sprite blankBallSprite;
    public Sprite faultBallSprite;

    //Keep track of previous value of faulty throws
    int prevRedballsFaulty = 0;
    int prevGreenBallsFaulty = 0;

    // Use this for initialization
    void Start () {
        //Find score UI object
        player1Score = GameObject.Find("p1Score").GetComponent<Text>();
        player2Score = GameObject.Find("p2Score").GetComponent<Text>();

        ////Scoreboard for second camera
        player1Score2 = GameObject.Find("p1Score2").GetComponent<Text>();
        player2Score2 = GameObject.Find("p2Score2").GetComponent<Text>();

        currentPlayer = GameObject.Find("currentPlayer").GetComponent<Text>();
        //currentPlayer2 = GameObject.Find("currentPlayer2").GetComponent<Text>();

        //get the controller object
        cont = GameObject.FindGameObjectWithTag("GameController");
        gameController = cont.GetComponent<Controller>();

        //Find all the orange ball counter components
         foreach(Transform child in GameObject.FindGameObjectWithTag("OrangeCounter").GetComponentsInChildren<Transform>())
         {
            if (child.CompareTag("Orange"))
            {
                orangeBalls.Add(child.GetComponent<Image>());
            }
         }

        foreach (Transform child in GameObject.FindGameObjectWithTag("OrangeCounter2").GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("Orange"))
            {
                orangeBalls2.Add(child.GetComponent<Image>());
            }
        }

        //Find all the blue ball counter components
        foreach (Transform child in GameObject.FindGameObjectWithTag("BlueCounter").GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("Blue"))
            {
                blueBalls.Add(child.GetComponent<Image>());
            }
        }

        foreach (Transform child in GameObject.FindGameObjectWithTag("BlueCounter2").GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("Blue"))
            {
                blueBalls2.Add(child.GetComponent<Image>());
            }
        }

        if(!player1Logo)
        {
            player1Logo = GameObject.FindGameObjectWithTag("ScoreboardPlayer1Logo").GetComponent<Image>();

            player1Logo.sprite = GlobalVariables.teamLogos[GlobalVariables.team1];
        }
        if(!player2Logo)
        {
            player2Logo = GameObject.FindGameObjectWithTag("ScoreboardPlayer2Logo").GetComponent<Image>();

            player2Logo.sprite = GlobalVariables.teamLogos[GlobalVariables.team2];
        }
        UpdateScoreboard();
    }
	
    public void UpdateScoreboard()
    {
        //update balls left
        p1BallsLeft = totalBalls - gameController.redBalls;
        p2BallsLeft = totalBalls - gameController.greenBalls;

        //// update the scores and balls thrown
        //player1Count.text = GlobalVariables.player1 + " Balls Left: " + p1BallsLeft;
        //player2Count.text = GlobalVariables.player2 + " Balls Left: " + p2BallsLeft;
        //player1Score.text = GlobalVariables.player1 + " Score: " + gameController.player1Score;
       // player2Score.text = GlobalVariables.player2 + " Score: " + gameController.player2Score;
        player1Score.text = gameController.player1Score.ToString();
        player2Score.text = gameController.player2Score.ToString();

        player1Score2.text = gameController.player1Score.ToString();
        player2Score2.text = gameController.player2Score.ToString();

        //Loop
        for (int i = 0; i < 6; i++)
        {
            if (gameController.currentPlayer == 1)
            {
                if (p1BallsLeft == i)
                {
                    //If the faulty ball counter has changed since the last time
                    if (prevRedballsFaulty != gameController.redBallsFaulty)
                    {
                        //Cross sprite
                        orangeBalls[i].sprite = faultBallSprite;
                        orangeBalls2[i].sprite = faultBallSprite;
                        //Update previous fault counter
                        prevRedballsFaulty = gameController.redBallsFaulty;
                    }
                    else
                    {
                        //Blank sprite
                        orangeBalls[i].sprite = blankBallSprite;
                        orangeBalls2[i].sprite = blankBallSprite;
                    }
                }
            }
            else if (gameController.currentPlayer == 2)
            {
                if (p2BallsLeft == i)
                { 
                    //If the faulty ball counter has changed since the last time
                    if (prevGreenBallsFaulty != gameController.greenBallsFaulty)
                    {
                        //Cross sprite
                        blueBalls[i].sprite = faultBallSprite;
                        blueBalls2[i].sprite = faultBallSprite;
                        //Update previous fault counter
                        prevGreenBallsFaulty = gameController.greenBallsFaulty;
                    }
                    else
                    {
                        //Blank sprite
                        blueBalls[i].sprite = blankBallSprite;
                        blueBalls2[i].sprite = blankBallSprite;
                    }
                }
            }
        }
        ////Second scoreboard
        //player1Count2.text = GlobalVariables.player1 + " Balls Left: " + p1BallsLeft;
        //player2Count2.text = GlobalVariables.player2 + " Balls Left: " + p2BallsLeft;

        //player1Score2.text = GlobalVariables.player1 + " Score: " + gameController.player1Score;
        //player2Score2.text = GlobalVariables.player2 + " Score: " + gameController.player2Score;

        //orangeBallCounter.sprite = orangeBallCounterList[gameController.redBalls];
        //blueBallCounter.sprite = blueBallCounterList[gameController.greenBalls];
        ////Move text indicating the current player
        if (gameController.currentPlayer == 1)
        {
            //currentPlayer.rectTransform.anchoredPosition = new Vector3(-290, 0 , 0);
            //currentPlayer2.rectTransform.anchoredPosition = new Vector3(-290, 0, 0);
            currentPlayer.text = "Current Player: \n" + GlobalVariables.player1;
            //currentPlayer2.text = "Current Player: \n" + GlobalVariables.player1;
        }
        else if (gameController.currentPlayer == 2)
        {
            //currentPlayer.rectTransform.anchoredPosition = new Vector3(290, 0, 0);
            //currentPlayer2.rectTransform.anchoredPosition = new Vector3(290, 0, 0);
            currentPlayer.text = "Current Player: \n" + GlobalVariables.player2;
            //currentPlayer2.text = "Current Player: \n" + GlobalVariables.player2;
        }
    }

    //Reset the scoreboard
    public void resetScoreboard()
    {
        for(int i = 0; i < 6; i++)
        {
            orangeBalls[i].sprite = orangeBallSprite;
            orangeBalls2[i].sprite = orangeBallSprite;

            blueBalls[i].sprite = blueBallSprite;
            blueBalls2[i].sprite = blueBallSprite;
			
			prevGreenBallsFaulty = 0;
			prevRedballsFaulty = 0;
        }
    }
}
