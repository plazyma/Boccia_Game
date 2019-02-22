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
        field = GameObject.FindWithTag("InputField");
        nameScript = field.GetComponent<NamingScript>();
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
            nameScript.checkNames();
            if (nameScript.p2name != "empty")
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
}
