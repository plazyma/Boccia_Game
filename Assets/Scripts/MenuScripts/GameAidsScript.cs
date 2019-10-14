using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameAidsScript : MonoBehaviour {

    public Sprite on, off,onH,offH;
    SpriteState bOn = new SpriteState();
    SpriteState bOff = new SpriteState();

    // Use this for initialization
    void Start () {

       
        bOn.pressedSprite = on;
        bOn.highlightedSprite = onH;
        bOff.pressedSprite = off;
        bOff.highlightedSprite = offH;
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playerWalls(string s)
    {
        if (s == "P1Walls")
        {
            if (GlobalVariables.walls1 == true)
            {
                GlobalVariables.walls1 = false;
                GetComponentInChildren<Image>().sprite = off;
                GetComponent<Button>().spriteState = bOff;
            }
            else
            {
                GlobalVariables.walls1 = true;              
                GetComponentInChildren<Image>().sprite = on;
                GetComponent<Button>().spriteState = bOn;
            }
            
        }
        else if (s == "P2Walls")
        {
            if (GlobalVariables.walls2 == true)
            {
                GlobalVariables.walls2 = false;                
                GetComponentInChildren<Image>().sprite = off;
                GetComponent<Button>().spriteState = bOff;
            }
            else
            {
                GlobalVariables.walls2 = true;
                GetComponentInChildren<Image>().sprite = on;
                GetComponent<Button>().spriteState = bOn;
            }

        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void playerAids(string s)
    {
        if (s == "P1Aim")
        {
            if (GlobalVariables.aim1 == true)
            {
                GlobalVariables.aim1 = false;
                GetComponentInChildren<Image>().sprite = off;
                GetComponent<Button>().spriteState = bOff;
            }
            else
            {
                GlobalVariables.aim1 = true;
                GetComponentInChildren<Image>().sprite = on;
                GetComponent<Button>().spriteState = bOn;
            }

        }
        else if (s == "P2Aim")
        {
            if (GlobalVariables.aim2 == true)
            {
                GlobalVariables.aim2 = false;
                GetComponentInChildren<Image>().sprite = off;
                GetComponent<Button>().spriteState = bOff;
            }
            else
            {
                GlobalVariables.aim2 = true;
                GetComponentInChildren<Image>().sprite = on;
                GetComponent<Button>().spriteState = bOn;
            }

        }
        EventSystem.current.SetSelectedGameObject(null);
    }
}
