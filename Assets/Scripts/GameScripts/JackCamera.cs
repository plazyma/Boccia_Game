using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackCamera : MonoBehaviour {

    public GameObject jack;
    Rigidbody rb;

    // Use this for initialization
    void Start() {

    }

    public void getJack()
    {
    //get the jack
        jack = GameObject.FindGameObjectWithTag("Jack");
        rb = jack.GetComponent<Rigidbody>();

    }

	// Update is called once per frame
	void Update () {
        if(!rb)
        {
            getJack();
        }
        //camera should follow jack
        transform.position = new Vector3(rb.transform.position.x, 3.0f ,rb.transform.position.z);
        
	}
}
