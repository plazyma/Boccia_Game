using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSettingNavigation : MonoBehaviour {

    public GameObject up, down, left , right;
    public GameObject start, settings, back;

    public GameObject selectedObject;

    public AudioSource audioSource;

    //single movement
    bool dPadPressed = false;




    // Use this for initialization
    void Start () {
        //set default position over Back
        if (name == "Selector")
        {
            selectedObject = back;
            transform.position = back.transform.position;
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
            selectedObject = selectedObject.GetComponent<GameSettingNavigation>().up;

            transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z - 1);

            dPadPressed = true;

        }
        else if ((Input.GetKeyDown("down") || Input.GetAxis("DPadY") == -1) && !dPadPressed)
        {
            selectedObject = selectedObject.GetComponent<GameSettingNavigation>().down;

            transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z - 1);

            dPadPressed = true;

        }
        else if ((Input.GetKeyDown("left") || Input.GetAxis("DPadX") == -1) && !dPadPressed)
        {
            selectedObject = selectedObject.GetComponent<GameSettingNavigation>().left;

            transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z - 1);

            dPadPressed = true;

        }
        else if ((Input.GetKeyDown("right") || Input.GetAxis("DPadX") == 1) && !dPadPressed)
        {
            selectedObject = selectedObject.GetComponent<GameSettingNavigation>().right;

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
            
            if (selectedObject == back)
            {
                //goto player selection
                back.GetComponent<SettingScript>().gotoSettings();
            }
            else if (selectedObject.CompareTag("Walls"))
            {
                selectedObject.GetComponent<GameAidsScript>().playerWalls(selectedObject.name);
            }
            else if (selectedObject.CompareTag("Aids"))
            {
                selectedObject.GetComponent<GameAidsScript>().playerAids(selectedObject.name);
            }
            //confirm sound
            audioSource.GetComponent<PlaySound>().playSound();

        }
        if (Input.GetAxis("MouseX") != 0 || Input.GetAxis("MouseY") != 0)
        {
            //move it off screen
            transform.position = new Vector3(10000, 0, 0);
        }
    }
}
