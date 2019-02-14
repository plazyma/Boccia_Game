using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamingScript : MonoBehaviour {

    public InputField nameInput;
    
    public string p1name;
    public string p2name;

    public GameObject cont;
    bool hideCont = true;

    // Use this for initialization
    void Start () {
        p1name = "empty";
        p2name = "empty";
        nameInput.text = "Enter Player1 Name...";
        cont = GameObject.FindGameObjectWithTag("PlayGameButton");
    }
	
	// Update is called once per frame
	void Update () {
        if (hideCont)
        {
            cont.SetActive(false);
            hideCont = false;
        }
	}

    public void checkNames(string newName)
    {
        if (p1name == "empty")
        {
            p1name = newName;
            nameInput.Select();
            nameInput.text = "";
        }
        else if (p2name == "empty")
        {
            p2name = newName;
        }
   

        if (p1name == "Steven")
        {
            Debug.Log("Welcome back Steven");
        }
        if (p2name == "Niko")
        {
            Debug.Log("Welcome back Niko");
        }

        if (p1name != "empty" && p2name != "empty")
        {
            GlobalVariables.player1 = p1name;
            GlobalVariables.player2 = p2name;
            cont.SetActive(true);
        }

    }
}
