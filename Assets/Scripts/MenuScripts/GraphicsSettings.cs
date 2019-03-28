using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsSettings : MonoBehaviour {


    public GameObject up, down, left, right;
    public GameObject back;

    public GameObject selectedObject;

    public int graphicsValue;

    public AudioSource audioSource;


    //single movement
    bool dPadPressed = false;


    // Use this for initialization
    void Start () {
        //set default position over quick play
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
            selectedObject = selectedObject.GetComponent<GraphicsSettings>().up;

            transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z - 1);

            dPadPressed = true;

        }
        else if ((Input.GetKeyDown("down") || Input.GetAxis("DPadY") == -1) && !dPadPressed)
        {
            selectedObject = selectedObject.GetComponent<GraphicsSettings>().down;

            transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z - 1);

            dPadPressed = true;

        }
        else if ((Input.GetKeyDown("left") || Input.GetAxis("DPadX") == -1) && !dPadPressed)
        {
            selectedObject = selectedObject.GetComponent<GraphicsSettings>().left;

            transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z - 1);

            dPadPressed = true;

        }
        else if ((Input.GetKeyDown("right") || Input.GetAxis("DPadX") == 1) && !dPadPressed)
        {
            selectedObject = selectedObject.GetComponent<GraphicsSettings>().right;

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
                //goto settings
                back.GetComponent<SettingScript>().gotoSettings();
            }
            else
            {
                QualitySettings.SetQualityLevel(selectedObject.GetComponent<GraphicsSettings>().graphicsValue, true);
            }

            //confirm sound
            audioSource.GetComponent<PlaySound>().playSound();
}
        else if (Input.GetButtonDown("B"))
        {
            //goto settings
            back.GetComponent<SettingScript>().gotoSettings();
        }

        if (Input.GetAxis("MouseX") != 0 || Input.GetAxis("MouseY") != 0)
        {
            //move it off screen
            transform.position = new Vector3(10000, 0, 0);
        }
    }
}
