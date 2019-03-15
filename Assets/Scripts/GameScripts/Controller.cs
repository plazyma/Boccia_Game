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

    public bool faultyBall = false;
    public bool successBall = false;

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
    public AimAssist aimAssistScript;

    //jack camera
    public GameObject jcam;
    JackCamera jackCamera;
    public GameObject cameraOverlay;
    public GameObject cameraOverlay2;
    public Camera cameraAlt;
    CameraView camAlt;

    //Audio
    public AudioSource audioSource;
    public AudioClip ballChangeSound;
    public AudioClip player1WinSound;
    public AudioClip player2WinSound;
    public AudioClip winSound;
    public GameObject musicSource;
    bool paused = false;

    //scoreboard
    Scoreboard scoreboardScript;

    public Material redMaterial; 
    public Material greenMaterial;

    public List<GameObject> ballList;

    public GameObject playerChangePanel;
    public List<Image> playerChangePanelImages = new List<Image>();

    public List<Sprite> playerChangePanelSprites = new List<Sprite>();

    public GameObject gameStartPanel;
    public GameObject gameStartPanelPlayer1;
    public GameObject gameStartPanelPlayer2;
    public List<Image> gameStartPanelPlayer1Images = new List<Image>();
    public List<Image> gameStartPanelPlayer2Images = new List<Image>();

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

    //Rounds
    int currentRound = 0;
    const int maximumRounds = 4;
    

    //Play Round bool
    bool playRound = false;

    public AudioVolume audioVolumeScript;

    // Use this for initialization
    void Start() {
        gameOver = false;
        //setup scoreboard
        scoreboardScript = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<Scoreboard>();

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
        cameraOverlay = GameObject.FindGameObjectWithTag("Screen");
        cameraOverlay2 = GameObject.FindGameObjectWithTag("Screen2");


        camAlt = player.GetComponent<CameraView>();

        //Aim Assist
        aimAssistScript = player.GetComponent<AimAssist>();

        //Faultboxes
        faultBoxes = GameObject.FindGameObjectWithTag("FaultBoxes");

        //Populate list with FaultBoxes scripts
        foreach (FaultBoxes fault in faultBoxes.GetComponentsInChildren<FaultBoxes>())
        {
            faultBoxList.Add(fault);
        }
        scoreboardScript.UpdateScoreboard();

        //get the music player
        musicSource = GameObject.FindGameObjectWithTag("MusicPlayer");

        //Load in player change panel if it isnt already
        if (!playerChangePanel)
        {
            playerChangePanel = GameObject.FindGameObjectWithTag("PlayerChangePanel");
        }

        //Load in images 
        foreach (Image im in playerChangePanel.GetComponentsInChildren<Image>())
        {
            if (!im.CompareTag("PlayerChangePanel"))
            {
                playerChangePanelImages.Add(im);
            }
        }

        //Load panels
        gameStartPanel = GameObject.FindGameObjectWithTag("GameStartPanel");
        gameStartPanelPlayer1 = GameObject.FindGameObjectWithTag("GameStartPanelP1");
        gameStartPanelPlayer2 = GameObject.FindGameObjectWithTag("GameStartPanelP2");

        //Load in sprites
        foreach(Object sprite in Resources.LoadAll("Letters", typeof(Sprite)))
        {
            playerChangePanelSprites.Add((Sprite)sprite);
        }

        //Load images
        foreach(Image im in gameStartPanelPlayer1.GetComponentsInChildren<Image>())
        {
            if(!im.CompareTag("GameStartPanelP1"))
            {
                gameStartPanelPlayer1Images.Add(im);
            }
        }

        //Load images
        foreach (Image im in gameStartPanelPlayer2.GetComponentsInChildren<Image>())
        {
            if (!im.CompareTag("GameStartPanelP2"))
            {
                gameStartPanelPlayer2Images.Add(im);
            }
        }

        if(!audioVolumeScript)
        {
            audioVolumeScript = GetComponent<AudioVolume>();

            audioVolumeScript.SetVolumeLevels();
        }

        //Disable all panels
        playerChangePanel.SetActive(false);


        //Display panel
        GameStartPanel();
    }

    void spawnJack()
    { 
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
        //If any of the panels are active, press return to confirm playing
        if((playerChangePanel.activeSelf || gameStartPanel.activeSelf) && Input.GetKeyDown("return"))
        {
            if(!playRound)
            {
                playRound = true;
            }

            //Deactivate panels
            if(playerChangePanel.activeSelf)
            {
                playerChangePanel.SetActive(false);
            }
            if(gameStartPanel.activeSelf)
            {
                gameStartPanel.SetActive(false);
            }

            aimAssistScript.EnableHardAimAssist();
            aimAssistScript.EnableArenaBoundary();
        }

        //If game is unpaused
        if (playRound)
        {
            //Debug
            if (Input.GetKeyDown("'"))
            {
                foreach (FaultBoxes fault in faultBoxList)
                {
                    print(fault.GetJackFault());
                }
            }

            //pause and unpause the music
            if (Input.GetKeyDown("m") && paused == false)
            {
                musicSource.GetComponent<AudioSource>().Pause();
                paused = true;
            }
            else if (Input.GetKeyDown("m") && paused == true)
            {
                musicSource.GetComponent<AudioSource>().Play();
                paused = false;
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
                char a = 'a';

                print(a);
            }
            if (Input.GetKeyDown("[5]"))
            {
                amountOfBalls = amountOfBalls + 2;
                greenBalls++;
                redBalls++;
            }

            //quit the game
            if (Input.GetKeyDown("q") || Input.GetButtonDown("Start Button"))
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

                                aimAssistScript.ResetAim();
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
                        //score.UpdateScoreboard();

                        //activate jack camera
                        cameraOverlay.SetActive(false);
                        cameraOverlay2.SetActive(false);
                    }

                }
            }
            //spawn a new ball
            if (!gameOver)
            {
                //If faulty jack throw
                if (faultyJack)
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

                if (currentRound < maximumRounds)
                {
                    if (Time.time - throwScript.shotTime > 10)
                    {
                        //reload the level
                        reset();
                    }
                }
                else
                {
                    if (gameOver && Time.time - throwScript.shotTime > 12)
                    {
                        reloadScene();
                    }
                }
            }
        }
    }

    public void spawnBall()
    {
        if (amountOfBalls < 12 && throwScript.ballThrown && Time.time - throwScript.shotTime > 4)
        {
            //Loop through list of faults, starting with 2 - don't want to check the boxes before the "v line"
            for (int i = 2; i < faultBoxList.Count - 1; i++)
            {
                if(faultBoxList[i].deleteBalls())
                {
                    faultyBall = true;
                }
                faultBoxList[i].ResetJack(jack);
            }

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
                //score.UpdateScoreboard();

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
                //score.UpdateScoreboard();

                audioSource.clip = ballChangeSound;
                audioSource.Play();
            }
            ballList.Add(newBall);

            //give throwscript the new ball
            throwScript.setBall(newBall);

            SetPlayerChangePanel(currentPlayer);
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
        if (currentRound < maximumRounds)
        {
            //do win stuff
            if (dist.FindClosestBall() == 1)
            {
                // print("PLAYER 1 WINS");
                player1Score = player1Score + dist.CalculateScore();

                //Make winner panel display for longer
                SetPlayerChangePanel(1);

                audioSource.clip = player1WinSound;
                audioSource.Play();
            }
            else if (dist.FindClosestBall() == 2)
            {
                //print("PLAYER 2 WINS");
                player2Score = player2Score + dist.CalculateScore();
                //Make winner panel display for longer
                SetPlayerChangePanel(2);

                audioSource.clip = player2WinSound;
                audioSource.Play();
            }
            currentRound++;
        }
        else
        {
            if (player1Score > player2Score)
            {
                //Make winner panel display for longer
                SetPlayerChangePanel(1);
                gameOver = true;

                audioSource.clip = winSound;
                audioSource.Play();
            }
            else
            {
                //Make winner panel display for longer
                SetPlayerChangePanel(1);
                gameOver = true;

                audioSource.clip = winSound;
                audioSource.Play();
            }
        }
        scoreboardScript.UpdateScoreboard();
    }

    //Activate panel indicating players have changed
    private void SetPlayerChangePanel(int currPlayer)
    {
        //Turn player names into a char array
        char[] playerName;
        if(currPlayer == 1)
        {
            playerName = GlobalVariables.player1.ToCharArray();
        }
        else 
        {
            playerName = GlobalVariables.player2.ToCharArray();
        }

        //Loop through all panel images to change
        for(int i = 0; i < playerChangePanelImages.Count; i++)
        {
            //Set image sprite to the value at char array position - 65 (based on unicode decimal)
            playerChangePanelImages[i].sprite = playerChangePanelSprites[playerName[i] - 65];
        }

        playerChangePanel.SetActive(true);
        playRound = false;

        //Disable aiming line
        aimAssistScript.DisableHardAimAssist();
    }

    //Display player 1 name vs player 2 name and the logos
    private void GameStartPanel()
    {
        //Enable panel
        gameStartPanel.SetActive(true);
        playRound = false;

        //Get in players names into an array
        char[] player1Name = GlobalVariables.player1.ToCharArray();
        char[] player2Name = GlobalVariables.player2.ToCharArray();

        //Loop through images to change sprites
        for(int i = 0; i < gameStartPanelPlayer1Images.Count; i++)
        {
            //Set sprite to the value at array position - 65 (based on unicode decimal)
            gameStartPanelPlayer1Images[i].sprite = playerChangePanelSprites[player1Name[i] - 65];
        }

        //Loop through images to change sprites
        for (int i = 0; i < gameStartPanelPlayer2Images.Count; i++)
        {
            //Set sprite to the value at array position - 65 (based on unicode decimal)
            gameStartPanelPlayer2Images[i].sprite = playerChangePanelSprites[player2Name[i] - 65];
        }

        //Text component to display current round
        Text roundNumber = GameObject.FindGameObjectWithTag("RoundNumber").GetComponent<Text>();

        if(roundNumber != null)
        {
            //Display current round 
            roundNumber.text = (currentRound + 1).ToString();
        }

    }

    //Return pause status
    public bool GetPlayRound()
    {
        return playRound;
    }

    //Set pause status
    public void SetPlayRound(bool play)
    {
        playRound = play;
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

        //Reset scoreboard
        scoreboardScript.UpdateScoreboard();
        scoreboardScript.resetScoreboard();


        cameraOverlay.SetActive(true);
        cameraOverlay2.SetActive(true);

        //Clear fault box lists
        foreach (FaultBoxes fault in faultBoxList)
        {
            fault.clearCollisionList();
        }

        //Clear fault count
        redBallsFaulty = 0;
        greenBallsFaulty = 0;

        //Clear aim assist
        aimAssistScript.ResetAim();
        aimAssistScript.DisableHardAimAssist();
        //Reset player position
        player.transform.eulerAngles = new Vector3(0, 90, 0);

        gameOver = false;

        GameStartPanel();
    }
}

