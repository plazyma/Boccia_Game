using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAidsScript : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playerWalls()
    {
        if (name == "P1Walls")
        {
            if (GlobalVariables.walls1 == true)
            {
                GlobalVariables.walls1 = false;
                GetComponentInChildren<Text>().text = "OFF";
            }
            else
            {
                GlobalVariables.walls1 = true;
                GetComponentInChildren<Text>().text = "ON";
            }
            
        }
        else if (name == "P2Walls")
        {
            if (GlobalVariables.walls2 == true)
            {
                GlobalVariables.walls2 = false;
                GetComponentInChildren<Text>().text = "OFF";
            }
            else
            {
                GlobalVariables.walls2 = true;
                GetComponentInChildren<Text>().text = "ON";
            }

        }

    }

    public void playerAids()
    {
        if (name == "P1Aim")
        {
            if (GlobalVariables.aim1 == true)
            {
                GlobalVariables.aim1 = false;
                GetComponentInChildren<Text>().text = "OFF";
            }
            else
            {
                GlobalVariables.aim1 = true;
                GetComponentInChildren<Text>().text = "ON";
            }

        }
        else if (name == "P2Aim")
        {
            if (GlobalVariables.aim2 == true)
            {
                GlobalVariables.aim2 = false;
                GetComponentInChildren<Text>().text = "OFF";
            }
            else
            {
                GlobalVariables.aim2 = true;
                GetComponentInChildren<Text>().text = "ON";
            }

        }

    }
}
