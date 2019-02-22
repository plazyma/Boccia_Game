using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist : MonoBehaviour {

    public GameObject player;
    public GameObject gameController;
    public List<GameObject> aimPlanes;
    public Throw throwScript;
	// Use this for initialization
	void Start () {
        //Find player
        if(!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        //Populate list with aim assist objects
		if(aimPlanes.Count == 0)
        {
            foreach(Transform child in transform)
            {
                if(child.CompareTag("AimAssist"))
                {
                    aimPlanes.Add(child.gameObject);
                    child.gameObject.SetActive(false);
                }
            }
        }
        //Get throw script
        if(!throwScript)
        {
            throwScript = GetComponent<Throw>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    //Enable aim assist objects when increasing power
    public void CalculateAimIncreased()
    {
        //When power is less than 7, enable (power *2) +2 objects
        if (throwScript.getPower() < 7)
        {
            for (int i = 0; i < (throwScript.getPower() * 2) + 2; i++)
            {
                aimPlanes[i].SetActive(true);
            }
        }
        //When power is less than 9, enable (power *2) +3 objects
        else if (throwScript.getPower() < 9)
        {
            for (int i = 0; i < (throwScript.getPower() * 2) + 3; i++)
            {
                aimPlanes[i].SetActive(true);
            }
        }
        //Enable power * 2 + 4 objects
        else
        {
            for (int i = 0; i < (throwScript.getPower() * 2) + 4; i++)
            {
                aimPlanes[i].SetActive(true);
            }
        }
    }
    public void CalculateAimReduced()
    {
        //When power is below 6, disable objects between current power *2 +2 and power + 1 *2 +2 (previous power)
        if (throwScript.getPower() < 6)
        {
            for (int i = (int)(throwScript.getPower() * 2) + 2; i < ((throwScript.getPower() + 1) * 2) + 2; i++)
            {
                aimPlanes[i].SetActive(false);
            }
        }
        else if(throwScript.getPower() == 6)
        {
            for (int i = (int)(throwScript.getPower() * 2) + 2; i < ((throwScript.getPower() + 1) * 2) + 3; i++)
            {
                aimPlanes[i].SetActive(false);
            }
        }
        else if(throwScript.getPower() == 7)
        {
            for (int i = (int)(throwScript.getPower() * 2) + 3; i < ((throwScript.getPower() + 1) * 2) + 3; i++)
            {
                aimPlanes[i].SetActive(false);
            }
        }
        else if (throwScript.getPower() == 8)
        {
            for (int i = (int)(throwScript.getPower() * 2) + 3; i < ((throwScript.getPower() + 1) * 2) + 4; i++)
            {
                aimPlanes[i].SetActive(false);
            }
        }
        else
        {
            for (int i = (int)(throwScript.getPower() * 2) + 4; i < ((throwScript.getPower() + 1) * 2) + 4; i++)
            {
                aimPlanes[i].SetActive(false);
            }
        }
    }
    //Disable all objects
    public void ResetAim()
    {
        for(int i = 0; i < aimPlanes.Count; i++)
        {
            aimPlanes[i].SetActive(false);
        }
    }
}
