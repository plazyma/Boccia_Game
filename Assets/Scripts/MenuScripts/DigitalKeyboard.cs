using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DigitalKeyboard : MonoBehaviour {

    public List <GameObject> letters;
    public List<GameObject> playerName;
    public GameObject letter1, letter2, letter3;
    public GameObject selectedLetter;
    public GameObject up, down, left, right;
    public GameObject back, cont;

    public Sprite defaultSprite;

    bool loadButtons = true;

    // Use this for initialization
    void Start () {

      
    }
	
	// Update is called once per frame
	void Update ()
    {
        //load buttons
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
                        transform.position = new Vector3(letter.transform.position.x, letter.transform.position.y, letter.transform.position.z - 1);
                        selectedLetter = letter;
                    }
                }
                defaultSprite = letter1.GetComponent<Image>().sprite;
            }
            
            loadButtons = false;

            //move buttons
            moveButtons();


            
        }

	}

    void moveButtons()
    {
        if (Input.GetKeyDown("right"))
        {
            selectedLetter = selectedLetter.GetComponent<DigitalKeyboard>().right;

            transform.position = new Vector3(selectedLetter.transform.position.x, selectedLetter.transform.position.y, selectedLetter.transform.position.z - 1);
        }
        if (Input.GetKeyDown("left"))
        {
            selectedLetter = selectedLetter.GetComponent<DigitalKeyboard>().left;

            transform.position = new Vector3(selectedLetter.transform.position.x, selectedLetter.transform.position.y, selectedLetter.transform.position.z - 1);
        }

        if (Input.GetKeyDown("up"))
        {
            selectedLetter = selectedLetter.GetComponent<DigitalKeyboard>().up;

            transform.position = new Vector3(selectedLetter.transform.position.x, selectedLetter.transform.position.y, selectedLetter.transform.position.z - 1);
        }
        if (Input.GetKeyDown("down"))
        {
            selectedLetter = selectedLetter.GetComponent<DigitalKeyboard>().down;

            transform.position = new Vector3(selectedLetter.transform.position.x, selectedLetter.transform.position.y, selectedLetter.transform.position.z - 1);
        }

        if (Input.GetKeyDown("o"))
        {
            if (selectedLetter == cont)
            {
                cont.GetComponent<NamingButtonScript>().gotoGame();
            }
            else if (selectedLetter == back)
            {
                back.GetComponent<NamingButtonScript>().returnToMenu();
            }
            else if (playerName.Count < 3 && selectedLetter != cont)
            {
                playerName.Add(selectedLetter);
                //update name
                updateName();
            }
            
            
        }
        if (Input.GetKeyDown("p"))
        {
            //delete last letter
            if (playerName.Count != 0)
            {
                playerName.RemoveAt(playerName.Count - 1);
                //update name
                updateName();
            }
        }
    }

    void updateName()
    {
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
