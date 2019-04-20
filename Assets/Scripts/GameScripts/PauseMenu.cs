using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject howToPlayMenu;
    public GameObject howToPlayMenu2;
    public GameObject howToPlayMenu3;
    public GameObject howToPlayMenu4;
    public GameObject howToPlayMenu5;

    public List<GameObject> howToPlayList = new List<GameObject>();

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

    bool buttonPressed = false;
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
            howToPlayList.Add(howToPlayMenu);
            howToPlayMenu.SetActive(false);
        }
        if (!howToPlayMenu2)
        {
            howToPlayMenu2 = GameObject.FindGameObjectWithTag("HowToPlay2");
            howToPlayList.Add(howToPlayMenu2);
            howToPlayMenu2.SetActive(false);
        }
        if (!howToPlayMenu3)
        {
            howToPlayMenu3 = GameObject.FindGameObjectWithTag("HowToPlay3");
            howToPlayList.Add(howToPlayMenu3);
            howToPlayMenu3.SetActive(false);
        }
        if (!howToPlayMenu4)
        {
            howToPlayMenu4 = GameObject.FindGameObjectWithTag("HowToPlay4");
            howToPlayList.Add(howToPlayMenu4);
            howToPlayMenu4.SetActive(false);
        }
        if (!howToPlayMenu5)
        {
            howToPlayMenu5 = GameObject.FindGameObjectWithTag("HowToPlay5");
            howToPlayList.Add(howToPlayMenu5);
            howToPlayMenu5.SetActive(false);
        }
        if (!pauseMenuSelection)
        {
            pauseMenuSelection = GameObject.FindGameObjectWithTag("PauseMenuSelection");
            ResetSelectionPosition();
            //pauseMenuSelection.SetActive(false);

        }

        menuConfirmation.SetActive(false);
        desktopConfirmation.SetActive(false);
        
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
            if ((Input.GetAxis("DPadY") == -1 || Input.GetButtonDown("Down")) && !buttonPressed)
            {
                if (pauseMenuSelection.activeSelf)
                {
                    if (pauseSelection < numOfButtons)
                    {
                        pauseSelection += 1;
                        UpdateSelection();
                        buttonPressed = true;
                    }
                }
                else
                {
                    pauseMenuSelection.SetActive(true);
                }
            }
            if ((Input.GetAxis("DPadY") == 1 || Input.GetButtonDown("Up"))&& !buttonPressed)
            {
                if (pauseMenuSelection.activeSelf)
                {
                    if (pauseSelection > 0)
                    {
                        pauseSelection -= 1;
                        UpdateSelection();
                        buttonPressed = true;
                    }
                }
                else
                {
                    pauseMenuSelection.SetActive(true);
                }
            }
            if (Input.GetAxis("DPadY") == 0)
            {
                buttonPressed = false;
            }
            if (Input.GetButtonDown("A"))
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

            if(Input.GetButtonDown("A"))
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

            if (Input.GetButtonDown("A"))
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

        for(int i = 0; i < howToPlayList.Count; i++)
        {
            if(howToPlayList[i].activeSelf)
            {
                if(Input.GetButtonDown("Throw") || Input.GetButtonDown("LeftMouseButton"))
                {
                    if (i == howToPlayList.Count - 1)
                    {
                        howToPlayList[i].SetActive(false);
                        ShowPauseMenu();
                        ResetSelectionPosition();
                        Input.ResetInputAxes();
                    }
                    else
                    {
                        howToPlayList[i + 1].SetActive(true);
                        howToPlayList[i].SetActive(false);
                        Input.ResetInputAxes();
                    }
                }
                if(Input.GetButtonDown("Menu"))
                {
                    howToPlayList[i].SetActive(false);
                    ShowPauseMenu();
                    ResetSelectionPosition();
                    Input.ResetInputAxes();
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
