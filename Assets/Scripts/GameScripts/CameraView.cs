using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    public Controller gameController;
    public Scoreboard scoreboardScript;
    public Camera cameraAlt;
    public Camera cameraMain;
    public Camera cameraMove;
    GameObject jack;
    GameObject scoreboard2;

    Vector3 origCameraRot;

    Vector3 topPos;
    Vector3 topRot;

    Vector3 leftPos;
    Vector3 leftRot;

    Vector3 rightPos;
    Vector3 rightRot;

    Throw throwScript;

    int currentCameraView = 0;
    const int numCameraViews = 3;
    // Use this for initialization
    void Start()
    {
        //Default camera rotation
        origCameraRot = cameraAlt.transform.eulerAngles;

        //Get access to Throw script
        throwScript = cameraMain.GetComponentInParent<Throw>();

        //Preset views
        topPos = new Vector3(0.0f, 5.0f, 0.0f);
        topRot = new Vector3(90.0f, 0.0f, 0.0f);

        leftPos = new Vector3(0.0f, 4.0f, 3.0f);
        leftRot = new Vector3(45.0f, 180.0f, 0.0f);

        rightPos = new Vector3(0.0f, 4.0f, -3.0f);
        rightRot = new Vector3(45.0f, 0.0f, 0.0f);

        //Disable second camera
        cameraAlt.enabled = false;

        //get hud2
        scoreboard2 = GameObject.FindWithTag("ScoreBoard2");

        if (!gameController)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>();
        }
        if(!scoreboardScript)
        {
            scoreboardScript = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<Scoreboard>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If not initialised
        if (!jack)
        {
            jack = GameObject.FindGameObjectWithTag("Jack");
        }
        //Only if jack has been thrown
        if (throwScript.jackThrown && !throwScript.ballThrown && gameController.GetPlayRound())
        {
            //Go back to original view
            if (Input.GetButtonDown("Camera Angles"))
            {
                if(currentCameraView < numCameraViews)
                {
                    currentCameraView += 1;
                    ChangeCameraView();
                    Input.ResetInputAxes();
                }
                else if(currentCameraView == numCameraViews)
                {
                    currentCameraView = 0;
                    ChangeCameraView();
                    Input.ResetInputAxes();
                }
            }       
        }

        if (Input.GetKeyDown("c"))
        {
            if (cameraMain.enabled == true)
            {
                cameraMove.enabled = true;
                cameraMain.enabled = false;
            }
            else if(cameraMain.enabled == false)
            {
                cameraMove.enabled = false;
                cameraMain.enabled = true;
            }

        }
    }

    //reset camera after throw
    public void cameraReset()
    {
        //Switch cameras
        cameraAlt.enabled = false;
        cameraMain.enabled = true;

        currentCameraView = 0;

        //disable second HUD
        scoreboard2.SetActive(false);
    }

    private void ChangeCameraView()
    {
        if(currentCameraView == 0)
        {
            cameraReset();
        }
        else if(currentCameraView == 1)
        {
            cameraAlt.transform.eulerAngles = origCameraRot;

            //Switch cameras
            cameraAlt.enabled = true;
            cameraMain.enabled = false;

            //Adjust position and rotation based on jack
            cameraAlt.transform.position = jack.transform.position + topPos;
            cameraAlt.transform.eulerAngles = topRot;

            scoreboardScript.UpdateCameraViewHUD("Above");
            //enable second HUD
            scoreboard2.SetActive(true);
        }
        else if(currentCameraView == 2)
        {
            cameraAlt.transform.eulerAngles = origCameraRot;

            //Switch cameras
            cameraAlt.enabled = true;
            cameraMain.enabled = false;

            //Adjust position and rotation based on jack
            cameraAlt.transform.position = jack.transform.position + rightPos;
            cameraAlt.transform.eulerAngles = rightRot;

            scoreboardScript.UpdateCameraViewHUD("Right");
            //enable second HUD
            scoreboard2.SetActive(true);
        }
        else if(currentCameraView == 3)
        {
            cameraAlt.transform.eulerAngles = origCameraRot;

            //Switch cameras
            cameraAlt.enabled = true;
            cameraMain.enabled = false;

            //Adjust position and rotation based on jack
            cameraAlt.transform.position = jack.transform.position + leftPos;
            cameraAlt.transform.eulerAngles = leftRot;

            scoreboardScript.UpdateCameraViewHUD("Left");
            //enable second HUD
            scoreboard2.SetActive(true);
        }
    }
}
