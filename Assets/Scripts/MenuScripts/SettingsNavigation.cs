using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsNavigation : MonoBehaviour {

    //neighbouring letters
    public GameObject up, down;
    public GameObject game, sound, graphics, back;

    public GameObject selectedObject;


    //single movement
    bool dPadPressed = false;


    // Use this for initialization
    void Start () {
        //set default position over quick play
        if (name == "Selector")
        {
            selectedObject = game;
            transform.position = game.transform.position;
        }
    }

    private void Awake()
    {
        //find settings options
        game = GameObject.FindWithTag("Game");
        sound = GameObject.FindWithTag("Sound");
        graphics = GameObject.FindWithTag("Graphics");
        back = GameObject.FindWithTag("Back");
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
            selectedObject = selectedObject.GetComponent<SettingsNavigation>().up;

            transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, selectedObject.transform.position.z - 1);

            dPadPressed = true;
        }
        if ((Input.GetKeyDown("down") || Input.GetAxis("DPadY") == -1) && !dPadPressed)
        {
            selectedObject = selectedObject.GetComponent<SettingsNavigation>().down;

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
            if (selectedObject == game)
            {
                //goto game settings
                game.GetComponent<SettingScript>().gotoGameSettings();
            }
            else if (selectedObject == sound)
            {
                //goto player selection
                sound.GetComponent<SettingScript>().gotoSoundSettings();
            }
            else if (selectedObject == graphics)
            {
                //goto player selection
                graphics.GetComponent<SettingScript>().gotoGraphicSettings();
            }
            else if (selectedObject == back)
            {
                //goto player selection
                back.GetComponent<SettingScript>().gotoMainMenu();
            }
        }


        if (Input.GetAxis("MouseX") != 0 || Input.GetAxis("MouseY") != 0)
        {
            //move it off screen
            transform.position = new Vector3(10000, 0, 0);
        }
    }
}
