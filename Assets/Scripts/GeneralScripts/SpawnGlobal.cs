using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGlobal : MonoBehaviour {

    public GameObject spawnGlob;
    public GameObject glob;

	// Use this for initialization
	void Start () {
        //Instantiate(glob);
    }
	
	// Update is called once per frame
	void Update () {
        spawnGlob = GameObject.FindGameObjectWithTag ("GlobalObject");
        if (spawnGlob == null)
        {
            Instantiate(glob);
        }

    }
}
