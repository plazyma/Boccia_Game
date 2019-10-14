using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdCheerAudio : MonoBehaviour {

    public AudioClip crowdCheerLoop;
    public AudioClip crowdCheerHigh;
    public AudioSource crowdAudioSource;
	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetCrowdHigh()
    {
        crowdAudioSource.volume = 1.0f;
        crowdAudioSource.clip = crowdCheerHigh;
        crowdAudioSource.Play();

    }

    public void SetCrowdLoop()
    {
        crowdAudioSource.volume = 0.15f;
        crowdAudioSource.clip = crowdCheerLoop;
        crowdAudioSource.Play();
    }
}
