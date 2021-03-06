﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamingScript : MonoBehaviour {

    public InputField nameInput;
    
    public string p1name;
    public string p2name;

    public GameObject cont;
    NamingButtonScript button;
    bool hideCont = true;

    public GameObject banner;


    // Use this for initialization
    void Start () {
        p1name = "Player 1";
        p2name = "Player 2";
        nameInput.text = "Enter Player 1 Name...";
        cont = GameObject.FindGameObjectWithTag("PlayGameButton");
        button = cont.GetComponent<NamingButtonScript>();
        banner = GameObject.FindGameObjectWithTag("NameBanner");
    }
	
	// Update is called once per frame
	void Update () {
        if (hideCont)
        {
            //cont.SetActive(false);
            hideCont = false;
        }
	}

    public void checkNames()
    {

        if (p1name == "Player 1")
        {
            p1name = nameInput.text;
            nameInput.Select();
            nameInput.text = "";
            nameInput.placeholder.GetComponent<Text>().text = "Enter Player 2 Name...";
            banner.GetComponent<Text>().text = "Player 2 \n Enter Name";
        }
        else if (p2name == "Player 2")
        {
            p2name = nameInput.text;
        }

        if (p1name != "Player 1")
        {
            button.changeLogo();
            if (p2name != "Player 2")
            {
                GlobalVariables.player1 = p1name;
                GlobalVariables.player2 = p2name;
            }
        }

    }
}
