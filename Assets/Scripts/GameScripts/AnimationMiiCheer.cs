using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMiiCheer : MonoBehaviour {

    public Animator miiAnimator;
    public Throw throwScript;
    public Controller gameController;
    bool isPlaying = false;

    bool playFaultAnim = false;

    public List<FaultBoxes> faultBoxList = new List<FaultBoxes>();
    // Use this for initialization
    void Start () {
        miiAnimator = GetComponent<Animator>();
        throwScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Throw>();

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>();

        foreach (FaultBoxes fault in GameObject.FindGameObjectWithTag("FaultBoxes").GetComponentsInChildren<FaultBoxes>())
        {
            faultBoxList.Add(fault);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Check if ball has been thrown and 3 secs have passed
        if (throwScript.ballThrown && Time.time - throwScript.shotTime > 3)
        {
            //If animation isnt playing
            if (!isPlaying)
            {
                //Check fault boxes starting at 2 - dont want to check below the V
                for (int i = 2; i < faultBoxList.Count - 1; i++)
                {
                    if (faultBoxList[i].ballFault)
                    {
                        playFaultAnim = true;
                    }
                }
                //Play shake
                if (playFaultAnim)
                {
                    miiAnimator.SetBool("shake", true);
                }
                else
                {
                    miiAnimator.SetBool("cheer", true);
                }

                isPlaying = true;
            }
        }
        else
        {
            //If animation is playing, reset
            if (isPlaying)
            {
                miiAnimator.SetBool("cheer", false);

                miiAnimator.SetBool("shake", false);

                gameController.faultyBall = false;
                playFaultAnim = false;
                isPlaying = false;
            }
        }
    }
}

