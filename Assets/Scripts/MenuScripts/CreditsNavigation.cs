using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsNavigation : MonoBehaviour
{

    public GameObject up, down;
    public GameObject back;

    public GameObject selectedObject;

    public AudioSource audioSource;

    bool dPadPressed = false;

    // Use this for initialization
    void Start()
    {
        //set default position over quick play
        if (name == "Selector")
        {
            selectedObject = back;
            transform.position = back.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (name == "Selector")
        {

            moveButton();
        }
    }

    void moveButton()
    {
        if ((Input.GetKeyDown("up") || Input.GetAxis("DPadY") == 1) && !dPadPressed)
        {
            selectedObject = selectedObject.GetComponent<CreditsNavigation>().up;

            transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z - 1);

            dPadPressed = true;
        }
        if ((Input.GetKeyDown("down") || Input.GetAxis("DPadY") == -1) && !dPadPressed)
        {
            selectedObject = selectedObject.GetComponent<CreditsNavigation>().down;

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
                //goto game settings
                back.GetComponent<CreditsScript>().ReturnToMainMenu();
            }

            audioSource.GetComponent<PlaySound>().playSound();
        }

        if (Input.GetAxis("MouseX") != 0 || Input.GetAxis("MouseY") != 0)
        {
            //move it off screen
            transform.position = new Vector3(10000, 0, 0);
        }
    }
}
