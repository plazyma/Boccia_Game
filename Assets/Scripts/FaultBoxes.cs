using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaultBoxes : MonoBehaviour {

    bool jackInArea;

    void Start()
    {
        jackInArea = false;
    }
 
    //Object has entered a faulty area
    private void OnTriggerEnter(Collider other)
    {
        //Check if the object is the jack
        if(other.CompareTag("Jack"))
        {
            jackInArea = true;
        }
    }

    //Object has exited faulty area
    private void OnTriggerExit(Collider other)
    {
        //Check if the object is the jack
        if (other.CompareTag("Jack"))
        {
            jackInArea = false;
        }
    }

    //Return whether the jack is in a faulty area or not
    public bool GetJackInArea()
    {
        return jackInArea;
    }

    //Force jack has left the area
    public void SetJackInArea()
    {
        jackInArea = false;
    }
}
