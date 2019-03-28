using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {
    //globals
    public static string player1;
    public static string player2;
    public static int team1 = 0,team2 = 1;

    public static bool walls1 = true, walls2 = true;
    public static bool aim1 = true, aim2 = true;

    public static float masterVolume = 1.0f, audioVolume = 1.0f, musicVolume = 0.3f;

    public Texture tex;


    //team array
    public const int  TOTALTEAMS = 13;
    public static Sprite [] teamLogos = new Sprite[TOTALTEAMS];
    public Sprite[] logoLoad = new Sprite[TOTALTEAMS];

    //debug stuff
    public string debugText1, debugText2;

    public float mv = masterVolume;
    public float av = audioVolume;
    public float muv = musicVolume;

    public bool wallss1 = true, wallss2 = true;
    public bool aimm1 = true, aimm2 = true;

    public int teams1, teams2;




    // Use this for initialization
    void Start () {
        //set initial names and set do not destroy for the global object
        player1 = "ONE";
        player2 = "TWO";
        DontDestroyOnLoad(gameObject);
        team1 = 1;
        team2 = 2;

        //load all of the teams into static variable
        for (int i = 0; i < TOTALTEAMS; i++)
        {
            teamLogos[i] = logoLoad[i];
        }

        Cursor.SetCursor(null, Vector2.zero,CursorMode.ForceSoftware);
    }
	
	// Update is called once per frame
	void Update () {

        //these all are debug variables
        debugText1 = player1;
        debugText2 = player2;

        mv = masterVolume;
        av = audioVolume;
        muv = musicVolume;

        wallss1 = walls1;
        wallss2 = walls2;
        aimm1 = aim1;
        aimm2 = aim2;

        teams1 = team1;
        teams2 = team2;
     }
}
