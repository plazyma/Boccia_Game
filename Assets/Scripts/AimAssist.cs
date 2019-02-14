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
        if(!gameController)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController");
        }
        if(!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
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
        if(!throwScript)
        {
            throwScript = GetComponent<Throw>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CalculateAimIncreased()
    {
        if (throwScript.getPower() < 7)
        {
            for (int i = 0; i < (throwScript.getPower() * 2) + 2; i++)
            {
                aimPlanes[i].SetActive(true);
            }
        }
        else if(throwScript.getPower() < 9)
        {
            for (int i = 0; i < (throwScript.getPower() * 2) + 3; i++)
            {
                aimPlanes[i].SetActive(true);
            }
        }
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
    public void ResetAim()
    {
        for(int i = 0; i < aimPlanes.Count; i++)
        {
            aimPlanes[i].SetActive(false);
        }
    }
}
