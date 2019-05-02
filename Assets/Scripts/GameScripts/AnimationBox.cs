using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBox : MonoBehaviour {

    public Animator boxAnimator;
    public Throw throwScript;
    public FaultBoxes faultBoxScript;
    public Controller gameController;
    bool isPlaying = false;
    bool playFaultAnim = false;

    public List<FaultBoxes> faultBoxList = new List<FaultBoxes>();
	// Use this for initialization
	void Start () {
        boxAnimator = GetComponent<Animator>();
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
        //Check if ball has been thrown and 3 seconds have passed since throwing
		if(throwScript.ballThrown && Time.time - throwScript.shotTime > 3)
        {
            //Check if an animation is not playing
            if (!isPlaying)
            {
                //Loop through fault boxes - start at 2, dont want boxes below V
                for (int i = 0; i < faultBoxList.Count - 1; i++)
                {
                    if(i > 1)
                    {
                        if (faultBoxList[i].ballFault || faultBoxList[i].GetJackFault())
                        {
                            playFaultAnim = true;
                        }
                    }
                    else
                    {
                        if(faultBoxList[i].GetJackFault())
                        {
                            playFaultAnim = true;
                        }
                    }
                }
                //Play shake animation
                if(playFaultAnim)
                {
                    boxAnimator.SetBool("shake", true);
                }
                else
                {
                    //Choose randomly between cheer or clap
                    if (Random.Range(0, 100) > 50)
                    {
                        boxAnimator.SetBool("cheer", true);
                    }
                    else
                    {
                        boxAnimator.SetBool("clap", true);
                    }
                }

                isPlaying = true;
            }
        }
        else
        {
            //If animation is playing
            if (isPlaying)
            {
                //Reset to idle
                boxAnimator.SetBool("cheer", false);
                boxAnimator.SetBool("clap", false);
                boxAnimator.SetBool("shake", false);

                gameController.faultyBall = false;
                playFaultAnim = false;
                isPlaying = false;
            }
        }
	}
}
