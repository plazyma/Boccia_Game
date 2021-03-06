﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SplashScreens : MonoBehaviour
{
    public Controller gameController;
    public AimAssist aimAssistScript;
    public PauseMenu pauseMenuScript;
    public CrowdCheerAudio crowdCheerAudioScript;

    public GameObject playerChangePanel;
    public List<Image> playerChangePanelImages = new List<Image>();

    public List<Sprite> chasLetterSprites = new List<Sprite>();

    public GameObject gameStartPanel;
    public List<Image> gameStartPanelPlayer1Images = new List<Image>();
    public List<Image> gameStartPanelPlayer2Images = new List<Image>();

    public GameObject roundOverPanel;
    public List<Image> roundOverPanelImages = new List<Image>();

    public GameObject gameOverPanel;
    public List<Image> gameOverPanelImages = new List<Image>();
	
	public GameObject playerTurnPrompt;
	public List<Image> playerTurnPromptPlayerNameImages = new List<Image>();
	
	public Sprite bluePrompt;
	public Sprite orangePrompt;

    public Sprite blueJackPrompt;
    public Sprite orangeJackPrompt;

    public Image gameStartPanelPlayer1Logo;
    public Image gameStartPanelPlayer2Logo;

    public VideoClip blueWinRound;
    public VideoClip blueWinGame;

    public VideoClip orangeWinRound;
    public VideoClip orangeWinGame;

    public GameObject videoObject;

    float timer;

    // Use this for initialization
    private void Awake()
    {
        if (!gameController)
        {
            gameController = GetComponent<Controller>();
        }
        if(!crowdCheerAudioScript)
        {
            crowdCheerAudioScript = GameObject.FindGameObjectWithTag("CrowdAudioPlayer").GetComponent<CrowdCheerAudio>();
        }

        if (!aimAssistScript)
        {
            aimAssistScript = GameObject.FindGameObjectWithTag("Player").GetComponent<AimAssist>();
        }

        if(!pauseMenuScript)
        {
            pauseMenuScript = GetComponent<PauseMenu>();
        }

        //Load in player change panel if it isnt already
        if (!playerChangePanel)
        {
            playerChangePanel = GameObject.FindGameObjectWithTag("PlayerChangePanel");
        }

        //Load in images 
        foreach (Image im in playerChangePanel.GetComponentsInChildren<Image>())
        {
            if (!im.CompareTag("PlayerChangePanel"))
            {
                playerChangePanelImages.Add(im);
            }
        }

        //Load in sprites
        foreach (Object sprite in Resources.LoadAll("Letters", typeof(Sprite)))
        {
            chasLetterSprites.Add((Sprite)sprite);
        }

        //Load panels
        if (!gameStartPanel)
        {
            gameStartPanel = GameObject.FindGameObjectWithTag("GameStartPanel");
        }

        //Load images
        foreach (Image im in GameObject.FindGameObjectWithTag("GameStartPanelP1").GetComponentsInChildren<Image>())
        {
            if (!im.CompareTag("GameStartPanelP1"))
            {
                gameStartPanelPlayer1Images.Add(im);
            }
        }

        //Load images
        foreach (Image im in GameObject.FindGameObjectWithTag("GameStartPanelP2").GetComponentsInChildren<Image>())
        {
            if (!im.CompareTag("GameStartPanelP2"))
            {
                gameStartPanelPlayer2Images.Add(im);
            }
        }

        if(!gameStartPanelPlayer1Logo)
        {
            gameStartPanelPlayer1Logo = GameObject.FindGameObjectWithTag("Player1Logo").GetComponent<Image>();
        }

        if(!gameStartPanelPlayer2Logo)
        {
            gameStartPanelPlayer2Logo = GameObject.FindGameObjectWithTag("Player2Logo").GetComponent<Image>();
        }

        if(!roundOverPanel)
        {
            roundOverPanel = GameObject.FindGameObjectWithTag("RoundOverPanel");
        }

        foreach(Image im in roundOverPanel.GetComponentsInChildren<Image>())
        {
            if(!im.CompareTag("RoundOverPanel"))
            {
                roundOverPanelImages.Add(im);
            }
        }

        if(!gameOverPanel)
        {
            gameOverPanel = GameObject.FindGameObjectWithTag("GameOverPanel");
        }

        foreach(Image im in gameOverPanel.GetComponentsInChildren<Image>())
        {
            if(!im.CompareTag("GameOverPanel"))
            {
                gameOverPanelImages.Add(im);
            }
        }
		
		foreach(Image im in playerTurnPrompt.GetComponentsInChildren<Image>())
		{
			if(!im.CompareTag("PlayerTurnPrompt"))
			{
			playerTurnPromptPlayerNameImages.Add(im);
			}
		}

        //Disable all panels
        playerChangePanel.SetActive(false);
        roundOverPanel.SetActive(false);
        gameOverPanel.SetActive(false);
		playerTurnPrompt.SetActive(false);

        videoObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((playerChangePanel.activeSelf || gameStartPanel.activeSelf || roundOverPanel.activeSelf || gameOverPanel.activeSelf) && Input.anyKeyDown)
        {
            if (!gameController.GetPlayRound())
            {
                gameController.SetPlayRound(true);
            }

            //Deactivate panels
            if (playerChangePanel.activeSelf)
            {
                playerChangePanel.SetActive(false);
            }
            else if (gameStartPanel.activeSelf)
            {
                gameStartPanel.SetActive(false);
            }
            else if (roundOverPanel.activeSelf)
            {
                roundOverPanel.SetActive(false);
                videoObject.SetActive(false);
            }
            else if (gameOverPanel.activeSelf)
            {
                gameOverPanel.SetActive(false);
                videoObject.SetActive(false);
                if (gameController.gameOver)
                {
                    pauseMenuScript.GameOver();
                }
            }
			
			
			if (!playerTurnPrompt.activeSelf)
			{
				playerTurnPrompt.SetActive(true);
			}


            aimAssistScript.EnableHardAimAssist();
            aimAssistScript.EnableArenaBoundary();

            Input.ResetInputAxes();

            crowdCheerAudioScript.SetCrowdLoop();
        }
    }

    //Display player 1 name vs player 2 name and the logos
    public void GameStartPanel()
    {
		playerTurnPrompt.SetActive(false);
		
        //Enable panel
        gameStartPanel.SetActive(true);
        gameController.SetPlayRound(false);

        //Get in players names into an array
        char[] player1Name = GlobalVariables.player1.ToCharArray();
        char[] player2Name = GlobalVariables.player2.ToCharArray();

        //Loop through images to change sprites
        for (int i = 0; i < gameStartPanelPlayer1Images.Count; i++)
        {
            //Set sprite to the value at array position - 65 (based on unicode decimal)
            gameStartPanelPlayer1Images[i].sprite = chasLetterSprites[player1Name[i] - 65];
        }

        //Loop through images to change sprites
        for (int i = 0; i < gameStartPanelPlayer2Images.Count; i++)
        {
            //Set sprite to the value at array position - 65 (based on unicode decimal)
            gameStartPanelPlayer2Images[i].sprite = chasLetterSprites[player2Name[i] - 65];
        }

        gameStartPanelPlayer1Logo.sprite = GlobalVariables.teamLogos[GlobalVariables.team1];
        gameStartPanelPlayer2Logo.sprite = GlobalVariables.teamLogos[GlobalVariables.team2];

        //Text component to display current round
        Text roundNumber = GameObject.FindGameObjectWithTag("RoundNumber").GetComponent<Text>();

        if (roundNumber != null)
        {
            print(gameController.GetCurrentRound() + 1);
            //Display current round 
            roundNumber.text = (gameController.GetCurrentRound() + 1).ToString();
        }

    }

    //Activate panel indicating players have changed
    public void PlayerChangePanel(int currPlayer)
    {
		playerTurnPrompt.SetActive(false);
		
        //Turn player names into a char array
        char[] playerName;
        if (currPlayer == 1)
        {
            playerName = GlobalVariables.player1.ToCharArray();
        }
        else
        {
            playerName = GlobalVariables.player2.ToCharArray();
        }

        //Loop through all panel images to change
        for (int i = 0; i < playerChangePanelImages.Count; i++)
        {
            //Set image sprite to the value at char array position - 65 (based on unicode decimal)
            playerChangePanelImages[i].sprite = chasLetterSprites[playerName[i] - 65];
        }

        playerChangePanel.SetActive(true);
        gameController.SetPlayRound(false);

        //Disable aiming line
        aimAssistScript.DisableHardAimAssist();
    }

    public void RoundOverPanel(int currPlayer)
    {
		playerTurnPrompt.SetActive(false);
		
        char[] playerName;

        if(currPlayer == 1)
        {
            playerName = GlobalVariables.player1.ToCharArray();

            roundOverPanel.GetComponent<VideoPlayer>().clip = orangeWinRound;

        }
        else
        {
            playerName = GlobalVariables.player2.ToCharArray();

            roundOverPanel.GetComponent<VideoPlayer>().clip = blueWinRound;
        }

        for(int i = 0; i < roundOverPanelImages.Count; i++)
        {
            roundOverPanelImages[i].sprite = chasLetterSprites[playerName[i] - 65];
        }

        roundOverPanel.SetActive(true);
        videoObject.SetActive(true);

        gameController.SetPlayRound(false);

        aimAssistScript.DisableHardAimAssist();
        crowdCheerAudioScript.SetCrowdHigh();
    }

    public void GameOverPanel(int currPlayer)
    {
		playerTurnPrompt.SetActive(false);
		
        char[] playerName;

        if(currPlayer == 1)
        {
            playerName = GlobalVariables.player1.ToCharArray();

            gameOverPanel.GetComponent<VideoPlayer>().clip = orangeWinGame;

        }
        else
        {
            playerName = GlobalVariables.player2.ToCharArray();

            gameOverPanel.GetComponent<VideoPlayer>().clip = blueWinGame;

        }

        for(int i = 0; i < gameOverPanelImages.Count; i++)
        {
            gameOverPanelImages[i].sprite = chasLetterSprites[playerName[i] - 65];
        }

        gameOverPanel.SetActive(true);
        videoObject.SetActive(true);

        gameController.SetPlayRound(false);

        aimAssistScript.DisableHardAimAssist();
        crowdCheerAudioScript.SetCrowdHigh();
    }
	
	public void PlayerTurnPrompt(int currPlayer)
	{
		char[] playerName;
		
		if(currPlayer == 1)
		{
			playerName = GlobalVariables.player1.ToCharArray();
			
            if(!gameController.jackThrown)
            {
                playerTurnPrompt.GetComponent<Image>().sprite = orangeJackPrompt;

            }
            else
            {
                playerTurnPrompt.GetComponent<Image>().sprite = orangePrompt;
            }

        }
		else
		{
			playerName = GlobalVariables.player2.ToCharArray();

            if (!gameController.jackThrown)
            {
                playerTurnPrompt.GetComponent<Image>().sprite = blueJackPrompt;
            }
            else
            {
                playerTurnPrompt.GetComponent<Image>().sprite = bluePrompt;
            }

        }
		
		for(int i = 0; i < playerTurnPromptPlayerNameImages.Count; i++)
		{
			playerTurnPromptPlayerNameImages[i].sprite = chasLetterSprites[playerName[i] - 65];

            if(!gameController.jackThrown)
            {
                playerTurnPromptPlayerNameImages[i].gameObject.SetActive(false);
            }
            else
            {
                playerTurnPromptPlayerNameImages[i].gameObject.SetActive(true);
            }
		}
		
		//playerTurnPrompt.SetActive(true);
	}

}
