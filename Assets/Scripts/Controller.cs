using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public int currentPlayer;
    public bool jackThrown = false;
    public bool firstThrows = true;

    public GameObject jack;
    public GameObject ball;
    GameObject newBall;
    public GameObject player;
    public Color colour = Color.black;

    public Throw throwScript;
    public PlayerControls pControls;

    public List<GameObject> ballList;

    //debug
    public int amountOfBalls = 0;




	// Use this for initialization
	void Start () {

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

        //setup player for spawning balls
        Quaternion spawnRotation = Quaternion.identity;
        Vector3 playerForward = new Vector3(player.transform.forward.x * 2, player.transform.forward.y*2, player.transform.forward.z * 2);
        Vector3 playerTransform = player.transform.position;

        //spawn jack on initial run time
        Instantiate(jack, player.transform.position + playerForward ,spawnRotation);

	}
	
	// Update is called once per frame
	void Update ()
    {
		if (throwScript.jackThrown && !jackThrown)
        {
            Vector3 stopped = Vector3.zero;

            //these are being triggered instantly due to initial velocity being 0
            if (jack.GetComponent<Rigidbody>().velocity.x  <0.0001f && jack.GetComponent<Rigidbody>().velocity.y < 0.0001f && Time.time- throwScript.shotTime >1)
            {
                //set jack as thrown on this script, create a new ball and tell throw that there is a new ball
                jackThrown = true;
                newBall = Instantiate(ball, player.transform.position + (player.transform.forward *2), player.transform.rotation);
                
            }
            //set the colour depending on player
            if (currentPlayer == 1)
            {
                colour = Color.red;   
            }
            else
            {
                colour = Color.green;
            }

            newBall.GetComponent<Renderer>().sharedMaterial.color = colour;
            //give throwscript the new ball
            throwScript.setBall(newBall);
        }
	}
}
