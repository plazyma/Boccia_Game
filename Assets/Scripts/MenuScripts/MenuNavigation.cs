using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNavigation : MonoBehaviour {

    //neighbouring buttons
    public GameObject up, down;
    public GameObject quick, start, settings, exit;

    public GameObject selectedObject;

    public AudioSource audioSource;

    //single movement
    bool dPadPressed = false;

    // Use this for initialization
    void Start () {

        //set default position over quick play
        if (name == "Selector")
        {
            selectedObject = quick;
            transform.position = quick.transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (name == "Selector")
        {
            moveButton();
        }
	}


    void moveButton()
    {
        if ((Input.GetKeyDown("up") || Input.GetAxis("DPadY") == 1) && !dPadPressed)
        {
            selectedObject = selectedObject.GetComponent<MenuNavigation>().up;

            transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z - 1);

            dPadPressed = true;

        }
        if ((Input.GetKeyDown("down") || Input.GetAxis("DPadY") == -1) && !dPadPressed)
        {
            selectedObject = selectedObject.GetComponent<MenuNavigation>().down;

            transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z - 1);

            dPadPressed = true;

        }

        // reset one button press when no direction is held ( not the best way might look into cleaner way)
        if (Input.GetAxis("DPadY") == 0 && Input.GetAxis("DPadX") == 0)
        {
            dPadPressed = false;
        }

        if (Input.GetKeyDown("space") || Input.GetButtonDown("A"))
        {
            if (selectedObject == quick)
            {
                //jump into game
                quick.GetComponent<StartScript>().QuickPlay();
            }
            else if (selectedObject == start)
            {
                //goto player selection
                start.GetComponent<StartScript>().Onclick();
            }
            else if (selectedObject == settings)
            {
                //goto player selection
                settings.GetComponent<SettingScript>().gotoSettings();
            }
            else if (selectedObject == exit)
            {
                //goto player selection
                exit.GetComponent<QuitScript>().doExitGame();
                //confirm sound
                audioSource.GetComponent<PlaySound>().playSound();
            }
        }
        if (Input.GetAxis("MouseX") != 0 || Input.GetAxis("MouseY") != 0)
        {
            //move it off screen
            transform.position = new Vector3(10000, 0, 0);
        }
    }
}
