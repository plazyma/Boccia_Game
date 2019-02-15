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

    //Update the slider to be equal to the power
    public void updatePowerBar()
    {
        powerSlider.value = player.GetComponent<Throw>().getPower();
    }
}
