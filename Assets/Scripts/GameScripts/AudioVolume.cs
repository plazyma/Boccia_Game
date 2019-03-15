using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolume : MonoBehaviour {
    public AudioSource audioSource;
    public AudioSource musicSource;
	// Use this for initialization
	void Awake () {
        //Find audio source
		if(!audioSource)
        {
            audioSource = GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<AudioSource>();
        }
        //Find music source
        if(!musicSource)
        {
            musicSource = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<AudioSource>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetVolumeLevels()
    {
        //If audio volume is greater than master volume - set it to master
        if(GlobalVariables.audioVolume >= GlobalVariables.masterVolume)
        {
            audioSource.volume = GlobalVariables.masterVolume;
        }
        //Otherwise set it to audio
        else
        {
            audioSource.volume = GlobalVariables.audioVolume;
        }

        //If music volume is greater than master volume - set it to master
        if (GlobalVariables.musicVolume >= GlobalVariables.masterVolume)
        {
            musicSource.volume = GlobalVariables.masterVolume;
        }
        //Otherwise set it to music
        else
        {
            musicSource.volume = GlobalVariables.musicVolume;
        }
    }
}
