using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour {

    //menu objects
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject soundSettings;
    public GameObject graphicSettings;
    bool unloadUI = true;

    //game volume
    public Slider slider;
    public float gameVolume = 100.0f;
    public Text volumePercent;

 

    // Use this for initialization
    void Start () {
        volumePercent = GameObject.Find("Volume Level").GetComponent<Text>();
        volumePercent.text = (int)gameVolume + "%";

        //find settings options
        mainMenu = GameObject.FindWithTag("Main Menu");
        settings = GameObject.FindWithTag("Settings");
        soundSettings = GameObject.FindWithTag("SoundSettings");
        graphicSettings = GameObject.FindWithTag("GraphicSettings");

       
    }

    private void Awake()
    {
        //find settings options
        mainMenu = GameObject.FindWithTag("Main Menu");
        settings = GameObject.FindWithTag("Settings");
        soundSettings = GameObject.FindWithTag("SoundSettings");
        graphicSettings = GameObject.FindWithTag("GraphicSettings");
    }
    // Update is called once per frame
    void Update () {
        if (unloadUI && mainMenu.activeInHierarchy == true)
        {
            // reset screen
            settings.SetActive(false);
            graphicSettings.SetActive(false);
            soundSettings.SetActive(false);
            unloadUI = false;
        }
        
    }
    void OnGUI()
    {
        if (graphicSettings.activeSelf == true)
        {
            string[] names = QualitySettings.names;
            {
                //GUILayout.BeginArea(new Rect(Screen.width/2 - (Screen.width / 3.5f), Screen.height / 2 + Screen.height / 12, 350, 50));
                GUILayout.BeginArea(new Rect(Screen.width / 2 - 50 , Screen.height / 2 + 10, 100, 350));
                //GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                for (int i = 0; i < names.Length; i++)
                {
                    if (GUILayout.Button(names[i]))
                    {
                        QualitySettings.SetQualityLevel(i, true);
                    }
                }
                GUILayout.EndVertical();
                //GUILayout.EndHorizontal();
                GUILayout.EndArea();
            }
        }
    }
    public void AdjustVolume()
    {
        gameVolume = slider.value*100;
        AudioListener.volume = slider.value;
        volumePercent.text = (int)gameVolume + "%";
    }

    public void gotoSettings()
    {
        mainMenu.SetActive(false);
        soundSettings.SetActive(false);
        graphicSettings.SetActive(false);
        settings.SetActive(true);
    }

    public void gotoSoundSettings()
    {
        soundSettings.SetActive(true);
        settings.SetActive(false);
    }

    public void gotoGrpahicSettings()
    {
        graphicSettings.SetActive(true);
        settings.SetActive(false);
    }

    public void gotoMainMenu()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
    }
        
 }
