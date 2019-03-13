using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NamingButtonScript : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip buttonClick;

    public Button button;
    public Sprite playSprite;
    public Sprite playPressedSprite;
    SpriteState playPressed = new SpriteState();
    public GameObject field;
    NamingScript nameScript;

    bool gameReady = false;

    
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeLogo()
    {
        button.GetComponent<Image>().sprite = playSprite;
        playPressed.pressedSprite = playPressedSprite;
        button.spriteState = playPressed;
    }
    public void setGameReady()
    {
        gameReady = true;
    }

    public void gotoGame()
    {
        //audioSource.clip = buttonClick;
        //audioSource.Play();

        if (!gameReady)
        {
            if (nameScript.p2name != "XYZ")
            {
                setGameReady();
            }
        }
        if (gameReady)
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }
    public void returnToMenu()
    {
        //audioSource.clip = buttonClick;
        //audioSource.Play();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    bool dothis()
        {

        return true;
        }
}
