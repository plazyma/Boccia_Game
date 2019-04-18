using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class DigitalKeyboard : MonoBehaviour {

    //list of letters for selection
    public List <GameObject> letters;

    //player name (ooo) / blank default
    public List <GameObject> playerName;

    //current player
    public int playerNumber = 1;

    //player names
    public string p1Name, p2Name;

    //chosen letters
    public GameObject letter1, letter2, letter3;

    //currently selected letter
    public GameObject selectedLetter;

    //neighbouring letters
    public GameObject up, down, left, right;

    //UI buttons
    public GameObject back, cont, del, tLeft, tRight;

    //default sprite for name entre
    public Sprite defaultSprite;

    //default letter for resetting position and choices
    GameObject defaultLetter;

    //selector
    public GameObject selectorObject;

    //UI
    public GameObject banner;
    public Button button;
    public Sprite playSprite;
    public Sprite playPressedSprite;
    SpriteState playPressed = new SpriteState();

    //single movement
    bool dPadPressed = false;

    //store the buttons variable
    bool loadButtons = true;


    // team selection
    public GameObject teamLogo;
    public int currentTeam = 0;


    // Use this for initialization
    void Start () {

        //empty names
        p1Name = "";
        p2Name = "";

        

        //find the text on screen
        banner = GameObject.FindGameObjectWithTag("NameBanner");
        selectorObject.GetComponent<AudioVolume>().SetVolumeLevels();
    }

    // Update is called once per frame
    void Update ()
    {
        //position the selector and load buttons and defaults
        if (name == "Selector")
        {
            if (loadButtons)
            {
            

                foreach (GameObject letter in GameObject.FindGameObjectsWithTag("Letter"))
                {
                    letters.Add(letter);
                    if (letter.name == "Q")
                    {
                        //transform.position = letter.transform.position;
                        transform.position = new Vector3(letter.transform.position.x, letter.transform.position.y, letter.transform.position.z - 7);
                        defaultLetter = letter;
                        selectedLetter = letter;
                    }
                    if (letter.name == "Delete")
                    {
                        del = letter;
                    }
                }
                defaultSprite = letter1.GetComponent<Image>().sprite;

                //hide the button 
                cont.SetActive(false);
            }
            
            loadButtons = false;

            //move buttons
            moveButtons();
            
        }
        if (Input.GetAxis("MouseX") != 0 || Input.GetAxis("MouseY")!= 0 && selectorObject.GetComponent<Renderer>().enabled == true)
        {
            selectorObject.GetComponent<Renderer>().enabled = false;
        }
    }

    public void changeTeamLogo(int direction)
    {
        //if the current team value gets to the end of the array then
        if (direction == 1)
        {  
            //if the current team is less than total teams
            if (selectorObject.GetComponent<DigitalKeyboard>().currentTeam < (GlobalVariables.TOTALTEAMS - 1))
            {
                //if player 2
                if(selectorObject.GetComponent<DigitalKeyboard>().playerNumber == 2)
                {
                    //increment the team
                    selectorObject.GetComponent<DigitalKeyboard>().currentTeam++;

                    //if this new number matches player 1s chosen team
                    if (selectorObject.GetComponent<DigitalKeyboard>().currentTeam == GlobalVariables.team1)
                    {
                        //if we are at the end of the array
                        if (selectorObject.GetComponent<DigitalKeyboard>().currentTeam == GlobalVariables.TOTALTEAMS -1)
                        {
                            //goto the start
                            selectorObject.GetComponent<DigitalKeyboard>().currentTeam = 0;
                        }
                        else
                        {
                            //otherwise move an extra space
                            selectorObject.GetComponent<DigitalKeyboard>().currentTeam++;
                        }
                        
                    }
                    //if this does not match player ones team
                    else
                    {
                        //
                        if (selectorObject.GetComponent<DigitalKeyboard>().currentTeam == GlobalVariables.TOTALTEAMS)
                        {
                            if (GlobalVariables.team1 == 0)
                            {
                                selectorObject.GetComponent<DigitalKeyboard>().currentTeam = 1;
                            }
                            else
                            {
                                selectorObject.GetComponent<DigitalKeyboard>().currentTeam = 0;
                            }
                        }

                    }
                }
                else
                {
                    selectorObject.GetComponent<DigitalKeyboard>().currentTeam++;
                }
                
            }
            else
            {
                if (GlobalVariables.team1 == 0)
                {
                    selectorObject.GetComponent<DigitalKeyboard>().currentTeam = 1;
                }
                else
                {
                    selectorObject.GetComponent<DigitalKeyboard>().currentTeam = 0;
                }
                
            }
        }
        //if the current team value gets to the start of the array then
        else if (direction == 0)
        {
            if (selectorObject.GetComponent<DigitalKeyboard>().currentTeam > 0 )
            {
                if (selectorObject.GetComponent<DigitalKeyboard>().playerNumber == 2)
                {
                    selectorObject.GetComponent<DigitalKeyboard>().currentTeam--;
                    if (selectorObject.GetComponent<DigitalKeyboard>().currentTeam == GlobalVariables.team1)
                    {                    
                        if (selectorObject.GetComponent<DigitalKeyboard>().currentTeam == 0)
                        {
                            selectorObject.GetComponent<DigitalKeyboard>().currentTeam = GlobalVariables.TOTALTEAMS - 1;
                        }
                        else
                        {
                            selectorObject.GetComponent<DigitalKeyboard>().currentTeam--;
                        }
                    }
                }
                else
                {
                    selectorObject.GetComponent<DigitalKeyboard>().currentTeam--;
                }
            }
            else
            {
                if (GlobalVariables.team1 == GlobalVariables.TOTALTEAMS - 1)
                {
                    selectorObject.GetComponent<DigitalKeyboard>().currentTeam = GlobalVariables.TOTALTEAMS - 2;
                }
                else
                {
                    selectorObject.GetComponent<DigitalKeyboard>().currentTeam = GlobalVariables.TOTALTEAMS - 1;
                }
                
            }
        }

        //update logo back to first team
        selectorObject.GetComponent<DigitalKeyboard>().teamLogo.GetComponent<Image>().sprite = GlobalVariables.teamLogos[selectorObject.GetComponent<DigitalKeyboard>().currentTeam];

    }

    public void confirmAndResetTeamLogo()
    {
        if (selectorObject.GetComponent<DigitalKeyboard>().playerNumber == 1)
        {
            GlobalVariables.team1 = selectorObject.GetComponent<DigitalKeyboard>().currentTeam;
        }
        else
        {
            GlobalVariables.team2 = selectorObject.GetComponent<DigitalKeyboard>().currentTeam;
        }

        if (playerNumber == 1)
        {
            //reset logo back to first team
            if (GlobalVariables.team1 != 0)
            {
                selectorObject.GetComponent<DigitalKeyboard>().currentTeam = 0;
                selectorObject.GetComponent<DigitalKeyboard>().teamLogo.GetComponent<Image>().sprite = GlobalVariables.teamLogos[0];
            }
            else
            {
                selectorObject.GetComponent<DigitalKeyboard>().currentTeam = 1;
                selectorObject.GetComponent<DigitalKeyboard>().teamLogo.GetComponent<Image>().sprite = GlobalVariables.teamLogos[1];
            }
            
        }
    }


    public void changeLogo()
    {
        //change from continue to play sprites for button
        button.GetComponent<Image>().sprite = playSprite;
        playPressed.pressedSprite = playSprite;
        playPressed.highlightedSprite = playPressedSprite;
        button.spriteState = playPressed;
    }

    void moveButtons()
    {
        // find the neighbouring letter and move the selector to it
        if (selectedLetter.GetComponent<DigitalKeyboard>().right.activeInHierarchy == true && (Input.GetKeyDown("right") || Input.GetAxis("DPadX") == 1) && !dPadPressed)
        {
            selectedLetter = selectedLetter.GetComponent<DigitalKeyboard>().right;

            transform.position = new Vector3(selectedLetter.transform.position.x, selectedLetter.transform.position.y, selectedLetter.transform.position.z - 7);

            dPadPressed = true;
            selectorObject.GetComponent<Renderer>().enabled = true;
        }
        if (selectedLetter.GetComponent<DigitalKeyboard>().left.activeInHierarchy == true && (Input.GetKeyDown("left") || Input.GetAxis("DPadX") == -1) && !dPadPressed )
        {
            selectedLetter = selectedLetter.GetComponent<DigitalKeyboard>().left;

            transform.position = new Vector3(selectedLetter.transform.position.x, selectedLetter.transform.position.y, selectedLetter.transform.position.z - 7 );

            dPadPressed = true;
            selectorObject.GetComponent<Renderer>().enabled = true;
        }

        if (selectedLetter.GetComponent<DigitalKeyboard>().up.activeInHierarchy == true && (Input.GetKeyDown("up") || Input.GetAxis("DPadY") == 1) && !dPadPressed)
        {
            selectedLetter = selectedLetter.GetComponent<DigitalKeyboard>().up;

            transform.position = new Vector3(selectedLetter.transform.position.x, selectedLetter.transform.position.y, selectedLetter.transform.position.z - 7);

            dPadPressed = true;
            selectorObject.GetComponent<Renderer>().enabled = true;
        }
        if (selectedLetter.GetComponent<DigitalKeyboard>().down.activeInHierarchy == true && (Input.GetKeyDown("down") || Input.GetAxis("DPadY") == -1) && !dPadPressed)
        {
            selectedLetter = selectedLetter.GetComponent<DigitalKeyboard>().down;

            transform.position = new Vector3(selectedLetter.transform.position.x, selectedLetter.transform.position.y, selectedLetter.transform.position.z - 7);

            dPadPressed = true;
            selectorObject.GetComponent<Renderer>().enabled = true;
        }
        
        // reset one button press when no direction is held ( not the best way might look into cleaner way)
        if (Input.GetAxis("DPadY") == 0 && Input.GetAxis("DPadX") == 0)
        {
            dPadPressed = false;
        }

        //confirmation
        if (Input.GetKeyDown("space") || Input.GetButtonDown("A"))
        {
            //if the selected object is the continue button and there are 3 chosen letters
            if (selectedLetter == cont && playerName.Count >= 3)
            {
                //if it is player one
                if (playerNumber == 1)
                {
                    //add characters to string
                    foreach (GameObject letter in playerName)
                    {
                        p1Name = p1Name + letter.name;
                        
                    }
                    //update global name
                    GlobalVariables.player1 = p1Name;

                    //move to next player
                    playerNumber++;

                    //clear the name array
                    playerName.Clear();

                    //update name screen
                    updateName();

                    //move selector back to original position
                    transform.position = new Vector3(defaultLetter.transform.position.x, defaultLetter.transform.position.y, defaultLetter.transform.position.z - 7);

                    //update selected letter
                    selectedLetter = defaultLetter;
                
                    //update name banner
                    banner.GetComponent<Text>().text = "Player 2 \n Enter Name \n Space / A Button : Confirm \n Backspace / B Button : Cancel";

                    //update continue button
                    changeLogo();

                    //reset and confirm the team
                    confirmAndResetTeamLogo();
                }
                else
                {
                    //add characters to string
                    foreach (GameObject letter in playerName)
                    {
                        p2Name = p2Name + letter.name;

                    }
                    //update global name
                    GlobalVariables.player2 = p2Name;

                    //load next scene
                    SceneManager.LoadScene("Game", LoadSceneMode.Single);
                }
                
            }
            else if (selectedLetter == tLeft)
            {    
            //if the current team value gets to the start of the array then
                if (currentTeam > 0)
                {
                    currentTeam--;
                }
                else
                {
                    currentTeam = GlobalVariables.TOTALTEAMS - 1;
                }
                

                //update logo back to first team
                teamLogo.GetComponent<Image>().sprite = GlobalVariables.teamLogos[selectorObject.GetComponent<DigitalKeyboard>().currentTeam];
            }
            else if (selectedLetter == tRight)
            {
                if (currentTeam < (GlobalVariables.TOTALTEAMS - 1))
                {
                    currentTeam++;
                }
                else
                {
                    currentTeam = 0;
                }
                //update logo back to first team
                teamLogo.GetComponent<Image>().sprite = GlobalVariables.teamLogos[selectorObject.GetComponent<DigitalKeyboard>().currentTeam];
            }
            
            //if the selected object is the back button
            else if (selectedLetter == back)
            {
                //return to the main menu
                back.GetComponent<NamingButtonScript>().returnToMenu();
            }
            else if (selectedLetter == del)
            {
                //delete last letter
                if (playerName.Count != 0)
                {
                    deleteLetter();
                }
            }
            else if (playerName.Count < 3 && selectedLetter != cont)
            {
                //add letter to name list
                playerName.Add(selectedLetter);

                //update name
                updateName();

                
            }
            
            
        }
        if (Input.GetKeyDown("backspace") || Input.GetButtonDown("B"))
        {
            deleteLetter();
        }
        if (playerName.Count == 3)
        {
            //show the button 
            cont.SetActive(true);
        }
        if (playerName.Count < 3 && cont.activeInHierarchy == true)
        {
            cont.SetActive(false);
        }
    }
    public void deleteLetter()
    {
        //delete last letter
        if (playerName.Count != 0)
        {
            //remove the newest letter
            playerName.RemoveAt(playerName.Count - 1);
            //update name
            updateName();
        }
    }

    public void onClick(GameObject let)
    {
        
        if (name == "Back")
        {
            //return to the main menu
            back.GetComponent<NamingButtonScript>().returnToMenu();

        }
        else if (name == "Continue")
        {
          
            //update player
            confirmAndResetTeamLogo();

            if (selectorObject.GetComponent<DigitalKeyboard>().playerName.Count >= 3)
            {
                //if it is player one
                if (selectorObject.GetComponent<DigitalKeyboard>().playerNumber == 1)
                {
                    //add characters to string
                    foreach (GameObject letter in selectorObject.GetComponent<DigitalKeyboard>().playerName)
                    {
                        p1Name = p1Name + letter.name;

                    }
                    //update global name
                    GlobalVariables.player1 = p1Name;

                    //move to next player
                    selectorObject.GetComponent<DigitalKeyboard>().playerNumber++;

                    //clear the name array
                    selectorObject.GetComponent<DigitalKeyboard>().playerName.Clear();

                    //update name
                    selectorObject.GetComponent<DigitalKeyboard>().updateName();

                    //update name banner
                    banner.GetComponent<Text>().text = "Player 2 \n Enter Name \n Space / A Button : Confirm \n Backspace / B Button : Cancel";

                    //update continue button
                    selectorObject.GetComponent<DigitalKeyboard>().changeLogo();
                }
                else
                {
                    //add characters to string
                    foreach (GameObject letter in selectorObject.GetComponent<DigitalKeyboard>().playerName)
                    {
                        p2Name = p2Name + letter.name;

                    }
                    //update global name
                    GlobalVariables.player2 = p2Name;

                    //load next scene
                    //SceneManager.LoadScene("Game", LoadSceneMode.Single);
                    GetComponent<StartScript>().QuickPlay();
                }

            }
        }
        else if (name == "Delete")
        {
            //delete last letter
            if (selectorObject.GetComponent<DigitalKeyboard>().playerName.Count != 0)
            {
                //remove the newest letter
                selectorObject.GetComponent<DigitalKeyboard>().playerName.RemoveAt(selectorObject.GetComponent<DigitalKeyboard>().playerName.Count - 1);
                //update name
                selectorObject.GetComponent<DigitalKeyboard>().updateName();
            }
        }
        else
        {
            if (selectorObject.GetComponent<DigitalKeyboard>().playerName.Count < 3)
            {
                //add letter to name list
                selectorObject.GetComponent<DigitalKeyboard>().playerName.Add(let);
                //update name
                selectorObject.GetComponent<DigitalKeyboard>().updateName();

                
            }
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void updateName()
    {
        //update the player name objects
        if (playerName.Count > 2)
        {
            letter1.GetComponent<Image>().sprite = playerName[0].GetComponent<Image>().sprite;
            letter2.GetComponent<Image>().sprite = playerName[1].GetComponent<Image>().sprite;
            letter3.GetComponent<Image>().sprite = playerName[2].GetComponent<Image>().sprite;

        }
        else if (playerName.Count > 1)
        {

            letter1.GetComponent<Image>().sprite = playerName[0].GetComponent<Image>().sprite;
            letter2.GetComponent<Image>().sprite = playerName[1].GetComponent<Image>().sprite;
            letter3.GetComponent<Image>().sprite = defaultSprite;
        }
        else if (playerName.Count > 0)
        {
            letter1.GetComponent<Image>().sprite = playerName[0].GetComponent<Image>().sprite;
            letter2.GetComponent<Image>().sprite = defaultSprite;
            letter3.GetComponent<Image>().sprite = defaultSprite;
        }
        else
        {
            letter1.GetComponent<Image>().sprite = defaultSprite;
            letter2.GetComponent<Image>().sprite = defaultSprite;
            letter3.GetComponent<Image>().sprite = defaultSprite;
        }

    }

}
