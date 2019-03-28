using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundNavigation : MonoBehaviour {


    public GameObject back;
    public GameObject selectedObject;
    public GameObject handleMaster, handleMusic, handleSFX;

    public Slider master,sfx,music;

    public Sprite selectedSprite;
    public Sprite defaultSprite;
    public int selection = 0;

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

    private void Awake()
    {
        //find settings options
        master = GameObject.FindWithTag("MasterSound").GetComponentInChildren<Slider>();
        sfx = GameObject.FindWithTag("SFXSound").GetComponentInChildren<Slider>();
        music = GameObject.FindWithTag("MusicSound").GetComponentInChildren<Slider>();
        back = GameObject.FindWithTag("ReturnButton");
    }

    // Update is called once per frame
    void Update () {
        if (name == "Selector")
        {

            moveButton();
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    void moveButton()
    {
        if (Input.GetKeyDown("left") || Input.GetAxis("DPadX") == -1 && !dPadPressed)
        {
            if (selection == 1)
            {
                sfx.value -=  0.1f;
            }
            if (selection == 2)
            {
                music.value -= 0.1f;
            }
            if (selection == 3)
            {
                master.value -= 0.1f;
            }
        }

        if (Input.GetKeyDown("right") || Input.GetAxis("DPadX") == 1 && !dPadPressed)
        {
            if (selection == 1)
            {
                sfx.value += 0.1f;
            }
            if (selection == 2)
            {
                music.value += 0.1f;
            }
            if (selection == 3)
            {
                master.value += 0.1f;
            }
        }

        if ((Input.GetKeyDown("up") || Input.GetAxis("DPadY") == 1) && !dPadPressed)
        {
            if (selection < 3)
            {
                selection++;
                transform.position = new Vector3(10000, 0, 0);
            }
            if(selection == 1)
            {
                handleSFX.GetComponent<Image>().sprite = selectedSprite;
            }
            else if (selection == 2)
            {
                handleSFX.GetComponent<Image>().sprite = defaultSprite;
                handleMusic.GetComponent<Image>().sprite = selectedSprite;
            }
            else if (selection == 3)
            {
                handleMusic.GetComponent<Image>().sprite = defaultSprite;
                handleMaster.GetComponent<Image>().sprite = selectedSprite;
            }

            dPadPressed = true;

        }
        if ((Input.GetKeyDown("down") || Input.GetAxis("DPadY") == -1) && !dPadPressed)
        {
            if (selection > 0)
            {
                selection--;
                
            }

            if (selection == 0)
            {
                transform.position = selectedObject.transform.position;
                handleSFX.GetComponent<Image>().sprite = defaultSprite;
            }

            else if (selection == 1)
            {
                handleSFX.GetComponent<Image>().sprite = selectedSprite;
                handleMusic.GetComponent<Image>().sprite = defaultSprite;
                
            }
            else if (selection == 2)
            {
                handleMusic.GetComponent<Image>().sprite = selectedSprite;
                handleMaster.GetComponent<Image>().sprite = defaultSprite;
            }
            else if (selection == 3)
            {
                handleMusic.GetComponent<Image>().sprite = defaultSprite;
                handleMaster.GetComponent<Image>().sprite = selectedSprite;
            }

            dPadPressed = true;

        }

        // reset one button press when no direction is held ( not the best way might look into cleaner way)
        if (Input.GetAxis("DPadY") == 0 && Input.GetAxis("DPadX") == 0)
        {
            dPadPressed = false;
        }

        if (Input.GetKeyDown("space") || Input.GetButtonDown("A") && selection == 0)
        {
            if (selectedObject == back)
            {
                //go back to settings
                back.GetComponent<SettingScript>().gotoSettings();
            }

            //confirm sound
            audioSource.GetComponent<PlaySound>().playSound();

        }
        if (Input.GetAxis("MouseX") != 0 || Input.GetAxis("MouseY") != 0)
        {
            //move it off screen
            transform.position = new Vector3(10000, 0, transform.position.z);
        }
    }
}
