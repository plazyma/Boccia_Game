using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaultBoxes : MonoBehaviour {

    GameObject player;
    Throw throwScript;

    bool inArea;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        throwScript = player.GetComponent<Throw>();
    }
 

    private void OnTriggerEnter(Collider other)
    {
        inArea = true;
        //print("Entered area");
    }

    private void OnTriggerExit(Collider other)
    {
        inArea = false;
        //print("Exited area");
    }

    void Update()
    {
        if (inArea)
        {
            print("In Area");
        }
    }

    public bool CheckCollision()
    {
        return inArea;
    }
}
