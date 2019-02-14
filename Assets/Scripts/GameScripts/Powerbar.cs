using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Powerbar : MonoBehaviour {

    Slider powerSlider;
    public GameObject player;  
	// Use this for initialization
	void Start () {
        powerSlider = GetComponentInChildren<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        powerSlider.value = player.GetComponent<Throw>().getPower();
	}
}
