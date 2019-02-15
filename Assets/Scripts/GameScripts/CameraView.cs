using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    public Camera cameraAlt;
    public Camera cameraMain;
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
    // Use this for initialization
    void Start()
    {
        //Default camera rotation
        origCameraRot = cameraAlt.transform.eulerAngles;

        //Get access to Throw script
        throwScript = cameraMain.GetComponentInParent<Throw>();

        //Preset views
        topPos = new Vector3(0.0f, 3.0f, 0.0f);
        topRot = new Vector3(90.0f, 0.0f, 0.0f);

        leftPos = new Vector3(0.0f, 3.0f, 3.0f);
        leftRot = new Vector3(45.0f, 180.0f, 0.0f);

        rightPos = new Vector3(0.0f, 3.0f, -3.0f);
        rightRot = new Vector3(45.0f, 0.0f, 0.0f);

        //Disable second camera
        cameraAlt.enabled = false;

        //get hud2
        scoreboard2 = GameObject.FindWithTag("ScoreBoard2");
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
        if (throwScript.jackThrown && !throwScript.ballThrown)
        {
            //Go back to original view
            if (Input.GetKeyDown("0"))
            {
                //Switch cameras
                cameraAlt.enabled = false;
                cameraMain.enabled = true;

                //disable second HUD
                scoreboard2.SetActive(false);
            }

            //Top down view
            if (Input.GetKeyDown("1"))
            {
                cameraAlt.transform.eulerAngles = origCameraRot;

                //Switch cameras
                cameraAlt.enabled = true;
                cameraMain.enabled = false;

                //Adjust position and rotation based on jack
                cameraAlt.transform.position = jack.transform.position + topPos;
                cameraAlt.transform.eulerAngles = topRot;

                //enable second HUD
                scoreboard2.SetActive(true);
            }

            //Side on from the right
            if (Input.GetKeyDown("2"))
            {
                cameraAlt.transform.eulerAngles = origCameraRot;

                //Switch cameras
                cameraAlt.enabled = true;
                cameraMain.enabled = false;

                //Adjust position and rotation based on jack
                cameraAlt.transform.position = jack.transform.position + rightPos;
                cameraAlt.transform.eulerAngles = rightRot;

                //enable second HUD
                scoreboard2.SetActive(true);
            }

            //Side on from the left
            if (Input.GetKeyDown("3"))
            {
                cameraAlt.transform.eulerAngles = origCameraRot;

                //Switch cameras
                cameraAlt.enabled = true;
                cameraMain.enabled = false;

                //Adjust position and rotation based on jack
                cameraAlt.transform.position = jack.transform.position + leftPos;
                cameraAlt.transform.eulerAngles = leftRot;

                //enable second HUD
                scoreboard2.SetActive(true);
            }
        }
    }
}
