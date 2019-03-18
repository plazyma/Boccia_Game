using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;
    Controller gameController;

    float timer = 0;
	// Use this for initialization
	void Awake () {
		if(!pauseMenu)
        {
            pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        }
        if(!gameController)
        {
            gameController = GetComponent<Controller>();
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if(gameController.GetPlayRound())
            {
                if(!pauseMenu.activeSelf)
                {
                    ShowPauseMenu();
                }
            }
            else
            {
                if(pauseMenu.activeSelf)
                {
                    HidePauseMenu();
                }
            }
        }
	}

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1.0f;

        gameController.SetPlayRound(true);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0.0f;

        gameController.SetPlayRound(false);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
