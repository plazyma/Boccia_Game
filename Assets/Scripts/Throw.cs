using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Throw : MonoBehaviour {
    //Initializing
    float power;
    float power_z;
    Rigidbody rb;
    public GameObject ball;
    Vector3 startPosition;

    public GameObject gameController;
    Controller cont;
    public GameObject player;

    public float shotTime;
    public bool ballFound = false;
    public bool ballThrown = false;
    public bool jackThrown = false;

    // Use this for initialization
    void Start () {
        //get controller, player and some scripts
        gameController = GameObject.FindWithTag("GameController");
        cont = gameController.gameObject.GetComponent<Controller>();
        player = GameObject.FindGameObjectWithTag("Player");

        rb.useGravity = false;
        power = 0.0f;

        startPosition = new Vector3(-17.73f, 1.68f, 2.38f);
    }

    // Update is called once per frame
    void Update () {

        //if there is no ball yet
        if (ballFound == false)
        {
            if (jackThrown == false)
            {
                ball = GameObject.FindGameObjectWithTag("Jack");
                rb = ball.GetComponent<Rigidbody>();
            }

        }
        //if ball isn't thrown
        if (ballThrown == false)
        { 

            //update balls position until thrown
            ball.transform.position = transform.position + (transform.forward * 2);



            //Increase x - power by 1
            if (Input.GetKeyDown("up"))
            {
                if (power < 10)
                {
                    power += 1.0f;

                    print(power);
                }
            }
            //Decrease x - power by 1
            if (Input.GetKeyDown("down"))
            {
                if (power > 0)
                {
                    power -= 1.0f;
                    print(power);
                }
            }
            //Decrease z - power by 1
            if (Input.GetKeyDown("left"))
            {
                if (power_z > -6)
                {
                    power_z -= 1.0f;
                    print(power_z);
                }
            }
            //Increase z - power by 1
            if (Input.GetKeyDown("right"))
            {
                if (power_z < 6)
                {
                    power_z += 1.0f;
                    print(power_z);
                }
            }
            //Apply force on release
            if (Input.GetKeyDown("space"))
            {
                //rb.useGravity = true;
                //Apply force to ball
                //Z - Force based on rotation
                //Steve - added more force due to mass changes for phsyics
                //rb.AddForce(power*4, 0, -power_z*4, ForceMode.Impulse);
                rb.AddForce(transform.forward * power * 400);
                rb.useGravity = true;
                if (ball.tag == "Jack")
                {
                    jackThrown = true;
                }
                else
                {
                    cont.amountOfBalls++;
                }
                //ball is thrown and can't be touched
                ballThrown = true;
                shotTime = Time.time;
            }
            
        }
        //Reset position of the ball and scene
        if (Input.GetKeyDown("r"))
        {

            //reload the level
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            //commented out for now
            ////Set velocity to 0
            //rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            //rb.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);

            ////rest position
            //ball.transform.position = startPosition;

            //rb.useGravity = false;

            //power = 0.0f;

        }
    }

    //Return the power
    public float getPower()
    {
        return power;
    }

    //set the new ball to throw
    public void setBall(GameObject instBall)
    {
        ball = instBall;

        rb = ball.GetComponent<Rigidbody>();

        ballThrown = false;
    }
}
