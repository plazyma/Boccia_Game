using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class NamingButtonScript : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip buttonClick;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void gotoGame()
    {
        //audioSource.clip = buttonClick;
        //audioSource.Play();
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void returnToMenu()
    {
        //audioSource.clip = buttonClick;
        //audioSource.Play();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
