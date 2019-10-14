using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Sprite playAgainSprite;
    public Sprite playAgainSpritePressed;

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

    public List<Button> buttonList = new List<Button>();

    int pauseSelection = 0;

    int numOfButtons = 3;

    int confirmationSelection = 0;

    bool buttonPressed = false;
    // Use this for initialization
    void Awake()
    {
        if (!pauseMenu)
        {
            pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        }
        if (!menuConfirmation)
        {
            menuConfirmation = GameObject.FindGameObjectWithTag("MenuConfirmation");
        }
        if (!desktopConfirmation)
        {
            desktopConfirmation = GameObject.FindGameObjectWithTag("DesktopConfirmation");
        }
        if (!gameController)
        {
            gameController = GetComponent<Controller>();
        }
        if (!cameraViewScript)
        {
            cameraViewScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraView>();
        }
        if (!returnButton)
        {
            returnButton = GameObject.FindGameObjectWithTag("PauseMenuReturnButton").GetComponent<Button>();

            buttonList.Add(returnButton);
        }
        if (!howToPlayButton)
        {
            howToPlayButton = GameObject.FindGameObjectWithTag("PauseMenuHowToPlayButton").GetComponent<Button>();

            buttonList.Add(howToPlayButton);
        }
        if (!quitToMenuButton)
        {
            quitToMenuButton = GameObject.FindGameObjectWithTag("PauseMenuQuitToMenuButton").GetComponent<Button>();

            buttonList.Add(quitToMenuButton);
        }
        if (!quitToDesktopButton)
        {
            quitToDesktopButton = GameObject.FindGameObjectWithTag("PauseMenuQuitToDesktopButton").GetComponent<Button>();

            buttonList.Add(quitToDesktopButton);
        }
        if (!quitToDesktopButtonNo)
        {
            quitToDesktopButtonNo = GameObject.FindGameObjectWithTag("PauseMenuQuitToDesktopButtonNo").GetComponent<Button>();

            buttonList.Add(quitToDesktopButtonNo);
        }
        if (!quitToDesktopButtonYes)
        {
            quitToDesktopButtonYes = GameObject.FindGameObjectWithTag("PauseMenuQuitToDesktopButtonYes").GetComponent<Button>();

            buttonList.Add(quitToDesktopButtonYes);
        }
        if (!quitToMenuButtonNo)
        {
            quitToMenuButtonNo = GameObject.FindGameObjectWithTag("PauseMenuQuitToMenuButtonNo").GetComponent<Button>();

            buttonList.Add(quitToMenuButtonNo);
        }
        if (!quitToMenuButtonYes)
        {
            quitToMenuButtonYes = GameObject.FindGameObjectWithTag("PauseMenuQuitToMenuButtonYes").GetComponent<Button>();

            buttonList.Add(quitToMenuButtonYes);
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
                else if (menuConfirmation.activeSelf)
                {
                    HideMenuConfirmation();
                    ResetSelectionPosition();
                }
                else if (desktopConfirmation.activeSelf)
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
            if ((Input.GetAxis("DPadY") == 1 || Input.GetButtonDown("Up")) && !buttonPressed)
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
            if (Input.GetButtonDown("A") || Input.GetKeyDown("space"))
            {
                switch (pauseSelection)
                {
                    case 0:
                        if (howToPlayButton.IsActive())
                        {
                            ShowHowToPlayMenu();
                        }

                        Input.ResetInputAxes();
                        break;
                    case 1:
                        if (returnButton.IsActive())
                        {
                            if (gameController.gameOver && gameController.GetCurrentRound() >= 3)
                            {
                                gameController.reloadScene();
                            }
                            else
                            {
                                HidePauseMenu();
                            }
                        }

                        Input.ResetInputAxes();
                        break;
                    case 2:
                        if (quitToMenuButton.IsActive())
                        {
                            ShowMenuConfirmation();
                        }
                        pauseMenuSelection.transform.position = quitToMenuButtonNo.transform.position;

                        Input.ResetInputAxes();
                        break;
                    case 3:
                        if (quitToDesktopButton.IsActive())
                        {
                            ShowDesktopConfirmation();
                        }
                        pauseMenuSelection.transform.position = quitToDesktopButtonNo.transform.position;

                        Input.ResetInputAxes();
                        break;
                }
            }
        }

        //Input for menu confirmation
        if (menuConfirmation.activeSelf)
        {
            if (Input.GetButtonDown("Turn Left") || Input.GetAxis("DPadX") < 0)
            {
                if (confirmationSelection == 1)
                {
                    confirmationSelection = 0;
                    pauseMenuSelection.transform.position = quitToMenuButtonNo.transform.position;
                }
            }
            if (Input.GetButtonDown("Turn Right") || Input.GetAxis("DPadX") > 0)
            {
                if (confirmationSelection == 0)
                {
                    confirmationSelection = 1;
                    pauseMenuSelection.transform.position = quitToMenuButtonYes.transform.position;
                }
            }

            if (Input.GetButtonDown("A") || Input.GetKeyDown("space"))
            {
                if (confirmationSelection == 0)
                {
                    pauseSelection = 0;
                    UpdateSelection();

                    HideMenuConfirmation();
                }
                if (confirmationSelection == 1)
                {
                    QuitToMenu();
                }
            }
        }

        //Input for desktop confirmation
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

            if (Input.GetButtonDown("A") || Input.GetKeyDown("space"))
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

        //Cycle through how to play screens
        for (int i = 0; i < howToPlayList.Count; i++)
        {
            if (howToPlayList[i].activeSelf)
            {
                if (Input.GetButtonDown("Throw") || Input.GetButtonDown("LeftMouseButton"))
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
                if (Input.GetButtonDown("Menu"))
                {
                    howToPlayList[i].SetActive(false);
                    ShowPauseMenu();
                    ResetSelectionPosition();
                    Input.ResetInputAxes();
                }
            }
        }
    }

    //Reset the selector's position to the first button
    void ResetSelectionPosition()
    {
        pauseMenuSelection.transform.position = howToPlayButton.transform.position;
        pauseSelection = 0;
    }

    //Update the rendering of the selector
    void UpdateSelection()
    {
        switch (pauseSelection)
        {
            case 0:
                if (howToPlayButton.IsActive())
                {
                    pauseMenuSelection.transform.position = howToPlayButton.transform.position;
                }
                break;
            case 1:
                if (returnButton.IsActive())
                {
                    pauseMenuSelection.transform.position = returnButton.transform.position;
                }
                break;
            case 2:
                if (quitToMenuButton.IsActive())
                {
                    pauseMenuSelection.transform.position = quitToMenuButton.transform.position;
                }
                break;
            case 3:
                if (quitToDesktopButton.IsActive())
                {
                    pauseMenuSelection.transform.position = quitToDesktopButton.transform.position;
                }
                break;
        }

    }

    // Hide pause menu
    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1.0f;

        gameController.SetPlayRound(true);
    }

    //Show pause menu
    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);

        //Hide cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //Stop game from running
        Time.timeScale = 0.0f;

        gameController.SetPlayRound(false);
    }

    //Show how to play
    public void ShowHowToPlayMenu()
    {
        howToPlayMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    //Hide how to play 
    public void HideHowToPlayMenu()

    {
        howToPlayMenu.SetActive(false);
    }

    //Show yes/no for menu confirmation
    public void ShowMenuConfirmation()
    {
        menuConfirmation.SetActive(true);

        foreach (Button butt in buttonList)
        {
            if (butt == quitToMenuButtonNo || butt == quitToMenuButtonYes)
            {
                butt.interactable = true;
            }
            else
            {
                butt.interactable = false;
            }
        }
    }

    //Hide yes/no for going to menu
    public void HideMenuConfirmation()
    {
        menuConfirmation.SetActive(false);

        foreach (Button butt in buttonList)
        {
            butt.interactable = true;
        }
    }

    //show yes/no for quitting to desktop
    public void ShowDesktopConfirmation()
    {
        desktopConfirmation.SetActive(true);

        confirmationSelection = 0;

        foreach (Button butt in buttonList)
        {
            if (butt == quitToDesktopButtonNo || butt == quitToDesktopButtonYes)
            {
                butt.interactable = true;
            }
            else
            {
                butt.interactable = false;
            }
        }
    }

    //Hide yes/no for quititng to desktop
    public void HideDesktopConfirmation()
    {
        desktopConfirmation.SetActive(false);

        foreach (Button butt in buttonList)
        {
            butt.interactable = true;
        }
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
        //returnButton.GetComponentInChildren<Text>().text = "Play Again?";
        howToPlayButton.gameObject.SetActive(false);
        howToPlayButton.interactable = false;


        pauseMenuSelection.transform.position = returnButton.transform.position;

        //Change sprite
        returnButton.image.sprite = playAgainSprite;

        //Sprite state for changing highlighted sprite
        SpriteState playAgainSpriteState = new SpriteState();

        //Get button's state
        playAgainSpriteState = returnButton.spriteState;

        //Change sprites
        playAgainSpriteState.highlightedSprite = playAgainSpritePressed;
        playAgainSpriteState.pressedSprite = playAgainSprite;

        //Set state
        returnButton.spriteState = playAgainSpriteState;

        ShowPauseMenu();

        returnButton.onClick.AddListener(gameController.reloadScene);
    }
}
