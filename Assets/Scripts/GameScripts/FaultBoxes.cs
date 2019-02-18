using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaultBoxes : MonoBehaviour
{

    bool jackFault;
    bool ballFault;

    Controller gameController;
    public GameObject centreBox;
    public List<GameObject> faultyBalls;
    void Start()
    {
        jackFault = false;
        ballFault = false;

        faultyBalls = new List<GameObject>();

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>();

        if (!centreBox)
        {
            centreBox = GameObject.FindGameObjectWithTag("CentreBox");
        }
    }

    //Object has entered a faulty area
    private void OnTriggerEnter(Collider other)
    {
        //Check if the object is the jack
        if (other.CompareTag("Jack"))
        {
            jackFault = true;
        }
        if (other.CompareTag("Ball"))
        {
            ballFault = true;
            if (!faultyBalls.Contains(other.gameObject))
            {
                faultyBalls.Add(other.gameObject);
                print("OBJECT ADDED");
            }
        }
    }

    //Object has exited faulty area
    private void OnTriggerExit(Collider other)
    {
        //Check if the object is the jack
        if (other.CompareTag("Jack"))
        {
            jackFault = false;
        }
        if (other.CompareTag("Ball"))
        {
            ballFault = false;
        }

        if (faultyBalls.Contains(other.gameObject))
        {
            faultyBalls.Remove(other.gameObject);
            print("OBJECT REMOVED");
        }
    }

    public void deleteBalls()
    {
        if (faultyBalls.Count > 0)
        {
            foreach (GameObject ball in faultyBalls.ToArray())
            {
                faultyBalls.Remove(ball);
                Destroy(ball);


                if (ball.GetComponent<BallStats>().playerThrown == 1)
                {
                    gameController.redBallsFaulty++;
                }
                else if (ball.GetComponent<BallStats>().playerThrown == 2)
                {
                    gameController.greenBallsFaulty++;
                }
            }
        }
    }

    //Return whether the jack is in a faulty area or not
    public bool GetJackFault()
    {
        return jackFault;
    }

    //Force jack has left the area
    public void SetJackFaultFalse()
    {
        jackFault = false;
    }

    public bool GetBallFault()
    {
        return ballFault;
    }

    public void SetBallFaultFalse()
    {
        ballFault = false;
    }

    public void ResetJack(GameObject jack)
    {
        if (jackFault)
        {
            jack.transform.position = centreBox.transform.position;
        }
    }

    public void clearCollisionList()
    {
        faultyBalls.Clear();
    }
}
