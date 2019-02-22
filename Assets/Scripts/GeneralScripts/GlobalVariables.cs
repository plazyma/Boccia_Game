using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {

    public static string player1;
    public static string player2;

 

   

	// Use this for initialization
	void Start () {
        //set initial names and set do not destroy for the global object
        player1 = "Player 1 name here";
        player2 = "Player 2 name here";
        DontDestroyOnLoad(gameObject);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
