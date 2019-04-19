using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject howToPlayMenu;
    Controller gameController;
    CameraView cameraViewScript;

    public Button returnButton;
    public Button howToPlayButton;
    public Button quitToMenuButton;
    public Button quitToDesktopButton;

    public Button quitToMenuButtonNo;
    public Button quitToMenuButtonYes;

    public Button quitToDesktopButtonNo;
    public Button quitToDesktopButtonYes;

    public GameObject menuConfirmation;
    public GameObject desktopConfirmation;
    public GameObject pauseMenuSelection;
    
    int pauseSelection = 0;

    int numOfButtons = 3;

    int confirmationSelection = 0;
	// Use this for initialization
	void Awake () {
		if(!pauseMenu)
        {
            pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        }
        if(!menuConfirmation)
        {
            menuConfirmation = GameObject.FindGameObjectWithTag("MenuConfirmation");
        }
        if(!desktopConfirmation)
        {
            desktopConfirmation = GameObject.FindGameObjectWithTag("DesktopConfirmation");
        }
        if(!gameController)
        {
            gameController = GetComponent<Controller>();
        }
        if(!cameraViewScript)
        {
            cameraViewScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraView>();
        }
        if(!returnButton)
        {
            returnButton = GameObject.FindGameObjectWithTag("PauseMenuReturnButton").GetComponent<Button>();
        }
        if(!howToPlayButton)
        {
            howToPlayButton = GameObject.FindGameObjectWithTag("PauseMenuHowToPlayButton").GetComponent<Button>();
        }
        if(!quitToMenuButton)
        {
            quitToMenuButton = GameObject.FindGameObjectWithTag("PauseMenuQuitToMenuButton").GetComponent<Button>();
        }
        if(!quitToDesktopButton)
        {
            quitToDesktopButton = GameObject.FindGameObjectWithTag("PauseMenuQuitToDesktopButton").GetComponent<Button>();
        }
        if(!quitToDesktopButtonNo)
        {
            quitToDesktopButtonNo = GameObject.FindGameObjectWithTag("PauseMenuQuitToDesktopButtonNo").GetComponent<Button>();
        }
        if (!quitToDesktopButtonYes)
        {
            quitToDesktopButtonYes = GameObject.FindGameObjectWithTag("PauseMenuQuitToDesktopButtonYes").GetComponent<Button>();
        }
        if (!quitToMenuButtonNo)
        {
            quitToMenuButtonNo = GameObject.FindGameObjectWithTag("PauseMenuQuitToMenuButtonNo").GetComponent<Button>();
        }
        if(!quitToMenuButtonYes)
        {
            quitToMenuButtonYes = GameObject.FindGameObjectWithTag("PauseMenuQuitToMenuButtonYes").GetComponent<Button>();
        }

        if (!howToPlayMenu)
        {
            howToPlayMenu = GameObject.FindGameObjectWithTag("HowToPlay");
        }
        if(!pauseMenuSelection)
        {
            pauseMenuSelection = GameObject.FindGameObjectWithTag("PauseMenuSelection");
            ResetSelectionPosition();
            //pauseMenuSelection.SetActive(false);

        }

        menuConfirmation.SetActive(false);
        desktopConfirmation.SetActive(false);
        howToPlayMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (Input.GetButtonDown("Menu") && !gameController.gameOver)
        {
            if (gameController.GetPlayRound())
            {
                if (!pauseMenu.activeSelf)
                {
                    ShowPauseMenu();
                    cameraViewScript.cameraReset();
                }
            }
            else
            {
                if (pauseMenu.activeSelf && (!menuConfirmation.activeSelf && !desktopConfirmation.activeSelf))
                {
                    HidePauseMenu();
                    ResetSelectionPosition();
                }
                else if (howToPlayMenu.activeSelf)
                {
                    ShowPauseMenu();
                    HideHowToPlayMenu();
                    ResetSelectionPosition();
                }
                else if(menuConfirmation.activeSelf)
                {
                    HideMenuConfirmation();
                    ResetSelectionPosition();
                }
                else if(desktopConfirmation.activeSelf)
                {
                    HideDesktopConfirmation();
                    ResetSelectionPosition();
                }
            }
        }
        if (pauseMenu.activeSelf == true && (menuConfirmation.activeSelf == false && desktopConfirmation.activeSelf == false))
        {
            if (Input.GetButtonDown("Power Down"))
            {
                if (pauseMenuSelection.activeSelf)
                {
                    if (pauseSelection < numOfButtons)
                    {
                        pauseSelection += 1;
                        UpdateSelection();
                    }
                }
                else
                {
                    pauseMenuSelection.SetActive(true);
                }
            }
            if (Input.GetButtonDown("Power Up"))
            {
                if (pauseMenuSelection.activeSelf)
                {
                    if (pauseSelection > 0)
                    {
                        pauseSelection -= 1;
                        UpdateSelection();
                    }
                }
                else
                {
                    pauseMenuSelection.SetActive(true);
                }
            }
            if (Input.GetButtonDown("Throw"))
            {
                switch (pauseSelection)
                {
                    case 0:
                        ShowHowToPlayMenu();

                        Input.ResetInputAxes();
                        break;
                    case 1:
                        HidePauseMenu();

                        Input.ResetInputAxes();
                        break;
                    case 2:
                        ShowMenuConfirmation();
                        pauseMenuSelection.transform.position = quitToMenuButtonNo.transform.position;

                        Input.ResetInputAxes();
                        break;
                    case 3:
                        ShowDesktopConfirmation();
                        pauseMenuSelection.transform.position = quitToDesktopButtonNo.transform.position;

                        Input.ResetInputAxes();
                        break;
                }
            }
        }

        if(menuConfirmation.activeSelf)
        {
            if(Input.GetButtonDown("Turn Left") || Input.GetAxis("DPadX") < 0)
            {
                if(confirmationSelection == 1)
                {
                    confirmationSelection = 0;
                    pauseMenuSelection.transform.position = quitToMenuButtonNo.transform.position;
                }              
            }
            if(Input.GetButtonDown("Turn Right") || Input.GetAxis("DPadX") > 0)
            {
                if (confirmationSelection == 0)
                {
                    confirmationSelection = 1;
                    pauseMenuSelection.transform.position = quitToMenuButtonYes.transform.position;
                }
            }

            if(Input.GetButtonDown("Throw"))
            {
                if(confirmationSelection == 0)
                {
                    pauseSelection = 0;
                    UpdateSelection();

                    HideMenuConfirmation();
                }
                if(confirmationSelection == 1)
                {
                    QuitToMenu();
                }
            }
        }

        if (desktopConfirmation.activeSelf)
        {
            if (Input.GetButtonDown("Turn Left") || Input.GetAxis("DPadX") < 0)
            {
                if (confirmationSelection == 1)
                {
                    confirmationSelection = 0;
                    pauseMenuSelection.transform.position = quitToDesktopButtonNo.transform.position;

                }
            }
            if (Input.GetButtonDown("Turn Right") || Input.GetAxis("DPadX") > 0)
            {
                if (confirmationSelection == 0)
                {
                    confirmationSelection = 1;
                    pauseMenuSelection.transform.position = quitToDesktopButtonYes.transform.position;

                }
            }

            if (Input.GetButtonDown("Throw"))
            {
                if (confirmationSelection == 0)
                {
                    HideDesktopConfirmation();

                    pauseSelection = 0;
                    UpdateSelection();                    
                }
                if (confirmationSelection == 1)
                {
                    QuitToDesktop();
                }
            }
        }
    }

    void ResetSelectionPosition()
    {
        pauseMenuSelection.transform.position = howToPlayButton.transform.position;
        pauseSelection = 0;
    }

    void UpdateSelection()
    {
        switch (pauseSelection)
        {
            case 0:
                pauseMenuSelection.transform.position = howToPlayButton.transform.position;
                break;
            case 1:
                pauseMenuSelection.transform.position = returnButton.transform.position;
                break;
            case 2:
                pauseMenuSelection.transform.position = quitToMenuButton.transform.position;
                break;
            case 3:
                pauseMenuSelection.transform.position = quitToDesktopButton.transform.position;
                break;
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

    public void ShowHowToPlayMenu()
    {
        howToPlayMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void HideHowToPlayMenu()

    {
        howToPlayMenu.SetActive(false);
    }

    public void ShowMenuConfirmation()
    {
        menuConfirmation.SetActive(true);
    }

    public void HideMenuConfirmation()
    {
        menuConfirmation.SetActive(false);
    }

    public void ShowDesktopConfirmation()
    {
        desktopConfirmation.SetActive(true);

        confirmationSelection = 0;
    }

    public void HideDesktopConfirmation()
    {
        desktopConfirmation.SetActive(false);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        returnButton.GetComponentInChildren<Text>().text = "Play Again?";

        ShowPauseMenu();

        returnButton.onClick.AddListener(gameController.reloadScene);
    }
}
