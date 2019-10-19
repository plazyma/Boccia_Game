using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingScript : MonoBehaviour {

    //menu objects
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject soundSettings;
    public GameObject graphicSettings;
    public GameObject gameSettings;
    public GameObject creditsMenu;

    bool unloadUI = true;

    //game volume
    public Slider slider;
    public float volume = 100.0f;
    public Text volumePercent;
    public AudioSource sfxSource;
    public AudioSource musicSource;
    public GameObject selector;
    


    // Use this for initialization
    void Start () {

        //volumePercent = GameObject.Find("Volume Level").GetComponent<Text>();
        

        

        //find settings options
        mainMenu = GameObject.FindWithTag("Main Menu");
        settings = GameObject.FindWithTag("Settings");
        soundSettings = GameObject.FindWithTag("SoundSettings");
        graphicSettings = GameObject.FindWithTag("GraphicSettings");
        gameSettings = GameObject.FindWithTag("GameSettings");


    }

    private void Awake()
    {
        //find settings options
        mainMenu = GameObject.FindWithTag("Main Menu");
        settings = GameObject.FindWithTag("Settings");
        soundSettings = GameObject.FindWithTag("SoundSettings");
        graphicSettings = GameObject.FindWithTag("GraphicSettings");
        gameSettings = GameObject.FindWithTag("GameSettings");
    }
    // Update is called once per frame
    void Update () {
        if (unloadUI && mainMenu.activeInHierarchy == true)
        {
            // reset screen
            settings.SetActive(false);
            graphicSettings.SetActive(false);
            soundSettings.SetActive(false);
            gameSettings.SetActive(false);
            creditsMenu.SetActive(false);
            unloadUI = false;
        }

        if (soundSettings.activeInHierarchy == true && !CompareTag("ReturnButton"))
        {
            volumePercent.text = (int)volume + "%";
        }
        
    }
   
    public void AdjustVolume()
    {
        volume = slider.value*100;
        GlobalVariables.masterVolume = slider.value;
        AudioListener.volume = GlobalVariables.masterVolume;
        volumePercent.text = (int)volume + "%";
    }

    public void AdjustSecondaryVolume()
    {
        volume = slider.value * 100;
        if (name == "Music")
        {
            GlobalVariables.musicVolume = slider.value;
            selector.GetComponent<SoundNavigation>().musicSource.volume = slider.value;
        }
        else if (name == "SFX")
        {
            GlobalVariables.audioVolume = slider.value;
            selector.GetComponent<SoundNavigation>().sfxSource.volume = slider.value;
        }
        volumePercent.text = (int)volume + "%";
    }

    public void gotoSettings()
    {
        mainMenu.SetActive(false);
        soundSettings.SetActive(false);
        graphicSettings.SetActive(false);
        gameSettings.SetActive(false);
        settings.SetActive(true);
        creditsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void gotoSoundSettings()
    {
        soundSettings.SetActive(true);
        settings.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void gotoGraphicSettings()
    {
        graphicSettings.SetActive(true);
        settings.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void gotoMainMenu()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void gotoGameSettings()
    {
        gameSettings.SetActive(true);
        settings.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
        
 }
