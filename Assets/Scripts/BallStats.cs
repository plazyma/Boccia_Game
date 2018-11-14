using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStats : MonoBehaviour {

    public int playerThrown;

	void start()
    {

    }

    //return thrown player
    public int getPlayer()
    {
        return playerThrown;
    }
}
