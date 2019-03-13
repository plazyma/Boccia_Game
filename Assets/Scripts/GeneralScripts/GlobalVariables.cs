using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {

    public static string player1;
    public static string player2;
    public static int team1,team2;

    public string debugText1, debugText2;
 

   

	// Use this for initialization
	void Start () {
        //set initial names and set do not destroy for the global object
        player1 = "ONE";
        player2 = "TWO";
        DontDestroyOnLoad(gameObject);
        team1 = 1;
        team2 = 2;

    }
	
	// Update is called once per frame
	void Update () {
        debugText1 = player1;
        debugText2 = player2;
	}
}
