using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("w"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        }
        if (Input.GetKeyDown("s"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        }
        if (Input.GetKeyDown("a"))
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown("d"))
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown("q"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
        if (Input.GetKeyDown("e"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        }
        if(Input.GetKeyDown("[2]"))
        {
            transform.Rotate(1.0f, 0.0f, 0.0f);
        }
        if (Input.GetKeyDown("[8]"))
        {
            transform.Rotate(-1.0f, 0.0f, 0.0f);
        }
        if (Input.GetKeyDown("[4]"))
        {
            transform.Rotate(0.0f, -1.0f, 0.0f);
        }
        if (Input.GetKeyDown("[6]"))
        {
            transform.Rotate(0.0f, 1.0f, 0.0f);
        }
    }
}
