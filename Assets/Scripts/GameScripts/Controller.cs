﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Controller : MonoBehaviour {

    public int currentPlayer;
    public bool jackThrown = false;
    public bool firstThrows = true;
    public bool faultyJack = false;

    public GameObject jackPrefab;
    GameObject jack;

    public GameObject redBall;
    public GameObject greenBall;
    GameObject newBall;
    public GameObject player;
    public Color colour = Color.black;

    public Throw throwScript;
    public PlayerControls pControls;
    public BallDistance dist;


    //jack camera
    public GameObject jcam;
    JackCamera jackCamera;
    public GameObject cameraOverlay;
	GameObject cameraOutline;
    public Camera cameraAlt;
    CameraView camAlt;

    //Audio
    public AudioSource audioSource;
    public AudioClip ballChangeSound;
    public AudioClip player1WinSound;
    public AudioClip player2WinSound;
    public AudioClip winSound;

    //scoreboard
    GameObject scoreBoard;
    Scoreboard score;

    public Material redMaterial; 
    public Material greenMaterial;

    public List<GameObject> ballList;

    public GameObject winPanel;
    public Text winText;

    public GameObject gameWinPanel;
    public Text gameWinText;

    //Faultboxes
    public GameObject faultBoxes;
    FaultBoxes faultBoxesScript;
    public List<FaultBoxes> faultBoxList = new List<FaultBoxes>();
    public GameObject faultBoxCross;

    //debug
    public int amountOfBalls = 0;
    public int greenBalls = 0;
    public int redBalls = 0;

    public int greenBallsFaulty = 0;
    public int redBallsFaulty = 0;

    public int player1Score = 0;
    public int player2Score = 0;

    public bool gameOver;
    public float gameOverTime = 0.0f;

    // Use this for initialization
    void Start() {
        winPanel.SetActive(false);
        gameWinPanel.SetActive(false);

        gameOver = false;
        //setup scoreboard
        scoreBoard = GameObject.FindGameObjectWithTag("ScoreBoard");
        score = scoreBoard.GetComponent<Scoreboard>();

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

        //spawn jack on initial run time
        spawnJack();

        //setup jack camera
        jcam = GameObject.FindGameObjectWithTag("JackCamera");
        jackCamera = jcam.GetComponent<JackCamera>();
        jackCamera.getJack();
        cameraOverlay = GameObject.FindGameObjectWithTag("CameraOverlay");
        cameraOverlay.SetActive(false);
        cameraOutline = GameObject.FindGameObjectWithTag("CameraOutline");
        cameraOutline.SetActive(false);

        camAlt = player.GetComponent<CameraView>();

        //Faultboxes
        faultBoxes = GameObject.FindGameObjectWithTag("FaultBoxes");

        //Populate list with FaultBoxes scripts
        foreach (FaultBoxes fault in faultBoxes.GetComponentsInChildren<FaultBoxes>())
        {
            faultBoxList.Add(fault);
        }
        score.UpdateScoreboard();
    }

    void spawnJack()
    { 
        winPanel.SetActive(false);
        //setup player for spawning balls
        Quaternion spawnRotation = Quaternion.identity;
        Vector3 playerForward = new Vector3(player.transform.forward.x * 2, player.transform.forward.y * 2, player.transform.forward.z * 2);
        Vector3 playerTransform = player.transform.position;

        jack = Instantiate(jackPrefab, player.transform.position + playerForward, spawnRotation);
        jack.GetComponent<Rigidbody>().useGravity = false;
        dist = jack.GetComponent<BallDistance>();

        throwScript.jackThrown = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug
        if(Input.GetKeyDown("'"))
        {
            foreach(FaultBoxes fault in faultBoxList)
            {
                print(fault.GetJackFault());
            }
        }
       
        //update jack cam
        if (Input.GetKeyDown("t"))
        {
            reset();
        }
        if (Input.GetKeyDown("j"))
        {
            jackCamera.getJack();
        }

        if (Input.GetKeyDown("p"))
        {
            player1Score++;
            score.UpdateScoreboard();
            // jack.transform.position = faultBoxCross.transform.position;
            
            //print(jack.transform.localScale.z);
            //jack.transform.position = new Vector3(jack.transform.position.x, jack.transform.position.y, jack.transform.position.z + (jack.GetComponent<SphereCollider>().radius * jack.transform.localScale.z));
        }

        //quit the game
        if (Input.GetKeyDown("q")|| Input.GetButtonDown("Start Button"))
        {
            Application.Quit();
        }

        if (throwScript.jackThrown && !jackThrown)
        {

            //after 4 seconds have passed since throwing
            if (Time.time - throwScript.shotTime > 4)
            {
                jackThrown = true;
                //Check all faulty areas for jack
                foreach (FaultBoxes fault in faultBoxList)
                {
                    if (fault != faultBoxList[faultBoxList.Count - 1])
                    {
                        if (fault.GetJackFault())
                        {
                            print(" Jack In Area");
                            //Faulty throw
                            faultyJack = true;

                            //Reset fault check
                            fault.SetJackFaultFalse();
                        }
                    }
                }
                //set jack as thrown on this script, create a new ball and tell throw that there is a new ball

                if (!faultyJack)
                {
                    if (currentPlayer == 1)
                    {
                        newBall = Instantiate(redBall, player.transform.position + (player.transform.forward * 2), player.transform.rotation);
                        redBalls++;
                    }
                    else if (currentPlayer == 2)
                    {
                        newBall = Instantiate(greenBall, player.transform.position + (player.transform.forward * 2), player.transform.rotation);
                        greenBalls++;
                    }
                    ballList.Add(newBall);
                    //give throwscript the new ball
                    throwScript.setPower(0.0f);
                    throwScript.setBall(newBall);

                    player.transform.eulerAngles = new Vector3(0, 90, 0);

                    //update scoreboard
                    score.UpdateScoreboard();

                    //activate jack camera
                    cameraOverlay.SetActive(true);
                    cameraOutline.SetActive(true);
                }
              
            }
        }
        //spawn a new ball
        if (!gameOver)
        {
            //If faulty jack throw
            if(faultyJack)
            {
                //Destroy jack
                Object.Destroy(jack);

                //Reset bools
                throwScript.jackThrown = false;
                throwScript.ballThrown = false;

                jackThrown = false;

                //Reset power
                throwScript.setPower(0);

                //Reset player rotation               
                player.transform.eulerAngles = new Vector3(0, 90, 0);

                //Spawn new jack
                spawnJack();

                //Script gets disabled on jack creation - force enable
                //jack.GetComponent<Collision>().enabled = true;

                //Reset bool
                faultyJack = false;

            }
            else
            {
                spawnBall(); 
            }
            
        }

        //10 balls have been thrown
        if (amountOfBalls > 11 && Time.time - throwScript.shotTime > 5)
        {
            if (!gameOver)
            {
                checkWinner();
                gameOver = true;
            }

            if (player1Score < 2 && player2Score < 2)
            {
                if (Time.time - throwScript.shotTime > 10)
                {
                    //reload the level
                    reset();
                }
            }
        }
        if (gameOver && Time.time - throwScript.shotTime > 12)
        {
            reloadScene();
        }
    }
    public void spawnBall()
    {
        if (amountOfBalls < 12 && throwScript.ballThrown && Time.time - throwScript.shotTime > 4)
        {
            //Loop through list of faults, starting with 2 - don't want to check the boxes before the "v line"
            for (int i = 2; i < faultBoxList.Count - 1; i++)
            {
                faultBoxList[i].deleteBalls();
                faultBoxList[i].ResetJack(jack);
            }
            //faultBoxList[6].ResetJack(jack);

            player.transform.eulerAngles = new Vector3(0, 90, 0);
            if (amountOfBalls > 1)
            {
                //Store current player
                int currentPlayerTemp = currentPlayer;
                //check distance
                currentPlayer = playerSelection();

                //Closest player returned - need to swap players - further player throws
                if (currentPlayer == 1)
                {
                    currentPlayer = 2;
                }
                else if (currentPlayer == 2)
                {
                    currentPlayer = 1;
                }
                //If 0 returned == no balls on field - keep same player
                else if (currentPlayer == 0)
                {
                    currentPlayer = currentPlayerTemp;
                }

                if (redBalls > 5)
                {
                    print("all reds thrown");
                    currentPlayer = 2;
                }
                if (greenBalls > 5)
                {
                    print("all greens thrown");
                    currentPlayer = 1;
                }
            }
            //Swap on the first throw only if no faulty balls
            if (amountOfBalls == 1 && (redBallsFaulty == 0 && greenBallsFaulty == 0))
            {
                if (currentPlayer == 1)
                {
                    currentPlayer = 2;
                }
                else if (currentPlayer == 2)
                {
                    currentPlayer = 1;
                }
            }

            if (currentPlayer == 2)
            {
                //reset camera
                camAlt.cameraReset();

                throwScript.setPower(0.0f);

                //create a new ball and tell throw that there is a new ball
                newBall = Instantiate(greenBall, player.transform.position + (player.transform.forward * 2), player.transform.rotation);
                greenBalls++;
                //update scoreboard
                score.UpdateScoreboard();

                audioSource.clip = ballChangeSound;
                audioSource.Play();
            }
            else if (currentPlayer == 1)
            {
                //reset camera
                camAlt.cameraReset();
                throwScript.setPower(0.0f);

                //create a new ball and tell throw that there is a new ball
                newBall = Instantiate(redBall, player.transform.position + (player.transform.forward * 2), player.transform.rotation);
                redBalls++;
                //update scoreboard
                score.UpdateScoreboard();

                audioSource.clip = ballChangeSound;
                audioSource.Play();
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
            //throwScript.setPower(0.0f);
            print("Player 1 is closest, returning player 1");
            
            return 1;
        }
        else if (dist.FindClosestBall() == 2)
        {
            //throwScript.setPower(0.0f);
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
        if (dist.FindClosestBall() == 1)
        {
           // print("PLAYER 1 WINS");
            winText.text = "Player 1 Wins!";
            player1Score++;

            audioSource.clip = player1WinSound;
            audioSource.Play();
        }
        if(dist.FindClosestBall() == 2)
        {
            //print("PLAYER 2 WINS");
            winText.text = "Player 2 Wins!";
            player2Score++;

            audioSource.clip = player2WinSound;
            audioSource.Play();
        }

        if(player1Score == 2)
        {
            gameWinText.text = "Player 1 Wins The Game!!!!!!!!!!";
            gameWinPanel.SetActive(true);
            gameOver = true;

            audioSource.clip = winSound;
            audioSource.Play();
        }
        if(player2Score == 2)
        {
            gameWinText.text = "Player 2 Wins The Game!!!!!!!!!";
            gameWinPanel.SetActive(true);
            gameOver = true;

            audioSource.clip = winSound;
            audioSource.Play();
        }
        if(player1Score < 2 && player2Score < 2)
        {
            winPanel.SetActive(true);
        }

        //winPanel.SetActive(true);
    }

    public void reloadScene()
    {
        
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            print("after scene load");
       
    }
    public void reset()
    {
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            Object.Destroy(ball);
        }
        
        redBalls = 0;
        greenBalls = 0;

        amountOfBalls = 0;

        //remove jack
        Object.Destroy(jack);

        throwScript.jackThrown = false;
        throwScript.ballThrown = false;
        throwScript.setPower(0);

        jackThrown = false;
        spawnJack();
        score.UpdateScoreboard();


        cameraOverlay.SetActive(false);
        cameraOutline.SetActive(false);

        gameOver = false;
    }
}
