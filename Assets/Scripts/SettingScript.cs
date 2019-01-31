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

    //game volume
    public Slider slider;
    public float gameVolume = 100.0f;
    public Text volumePercent;

 

    // Use this for initialization
    void Start () {
        volumePercent = GameObject.Find("Volume Level").GetComponent<Text>();
        volumePercent.text = (int)gameVolume + "%";
    }
	
	// Update is called once per frame
	void Update () {
    }
    void OnGUI()
    {
        if (graphicSettings.activeSelf == true)
        {
            string[] names = QualitySettings.names;
            {
                GUILayout.BeginArea(new Rect(Screen.width / 6, Screen.height / 2, 350, 50));
                GUILayout.BeginHorizontal();
                for (int i = 0; i < names.Length; i++)
                {
                    if (GUILayout.Button(names[i]))
                    {
                        QualitySettings.SetQualityLevel(i, true);
                    }
                }

                GUILayout.EndHorizontal();
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
