using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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


    //jack camera
    public GameObject jcam;
    JackCamera jackCamera;
    public GameObject cameraOverlay;
	GameObject cameraOutline;


    //scoreboard
    GameObject scoreBoard;
    Scoreboard score;

    public Material redMaterial; 
    public Material greenMaterial;

    public List<GameObject> ballList;

    public GameObject winPanel;
    public Text winText;

    //debug
    public int amountOfBalls = 0;
    public int greenBalls = 0;
    public int redBalls = 0;

    public int player1Score = 0;
    public int player2Score = 0;

    public bool gameOver = false;


	// Use this for initialization
	void Start () {
        winPanel.SetActive(false);

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
    }

    void spawnJack()
    {
       
        winPanel.SetActive(false);
        //setup player for spawning balls
        Quaternion spawnRotation = Quaternion.identity;
        Vector3 playerForward = new Vector3(player.transform.forward.x * 2, player.transform.forward.y * 2, player.transform.forward.z * 2);
        Vector3 playerTransform = player.transform.position;

        jack = Instantiate(jack, player.transform.position + playerForward, spawnRotation);
        jack.GetComponent<Rigidbody>().useGravity = false;
        dist = jack.GetComponent<BallDistance>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //update jack cam
        if (Input.GetKeyDown("t"))
        {
            reset();
        }
        if(Input.GetKeyDown("j"))
        {
            jackCamera.getJack();
        }

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
                    greenBalls++;
                }
                ballList.Add(newBall);
                //give throwscript the new ball
                throwScript.setBall(newBall);
                throwScript.setPower(0.0f);
                player.transform.eulerAngles = new Vector3 (0, 90,  0);

                //update scoreboard
                score.UpdateScoreboard();

                //activate jack camera
                cameraOverlay.SetActive(true);
				cameraOutline.SetActive(true);
            }          
        }
        //spawn a new ball
        if (!gameOver)
        {
            spawnBall();
        }

        //10 balls have been thrown
        if (amountOfBalls > 11 && Time.time - throwScript.shotTime > 5)
        {
            if (!gameOver)
            {
                checkWinner();
                gameOver = true;
            }

            if (Time.time - throwScript.shotTime > 10)
            {
                //reload the level
                reset();
                //Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            }
        }
	}
    public void spawnBall()
    {
        if (amountOfBalls < 12 && throwScript.ballThrown && Time.time - throwScript.shotTime > 4)
        {
            player.transform.eulerAngles = new Vector3(0, 90, 0);
            if (amountOfBalls > 1)
            {
                //check distance
                currentPlayer = playerSelection();
                if (redBalls > 5)
                {
                    print("all reds thrown");
                    currentPlayer = 1;
                }
                if (greenBalls > 5)
                {
                    print("all greens thrown");
                    currentPlayer = 2;
                }
            }


            if (currentPlayer == 1)
            {
                throwScript.setPower(0.0f);
                currentPlayer = 2;

                //create a new ball and tell throw that there is a new ball
                newBall = Instantiate(greenBall, player.transform.position + (player.transform.forward * 2), player.transform.rotation);
                greenBalls++;
                //update scoreboard
                score.UpdateScoreboard();
            }
            else if (currentPlayer == 2)
            {
                throwScript.setPower(0.0f);
                currentPlayer = 1;

                //create a new ball and tell throw that there is a new ball
                newBall = Instantiate(redBall, player.transform.position + (player.transform.forward * 2), player.transform.rotation);
                redBalls++;
                //update scoreboard
                score.UpdateScoreboard();
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
        }
        if(dist.FindClosestBall() == 2)
        {
            //print("PLAYER 2 WINS");
            winText.text = "Player 2 Wins!";
            player2Score++;
        }

        winPanel.SetActive(true);
    }

    public void reset()
    {
        print("reset start");
        //reset the game
        //List <GameObject> ballList = new List <GameObject>();

        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            Object.Destroy(ball);
        }
        
        redBalls = 0;
        greenBalls = 0;

        amountOfBalls = 0;

        //remove balls
        Object.Destroy(jack);

        throwScript.jackThrown = false;
        throwScript.ballThrown = false;
        throwScript.setPower(0);

        jackThrown = false;
        spawnJack();
        score.UpdateScoreboard();
        //reset connections to jack
      //  jackCamera.getJack();
        print("reset stop");

        cameraOverlay.SetActive(false);
        cameraOutline.SetActive(false);
    }
}

