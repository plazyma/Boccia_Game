using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

    AudioSource audioSource;
    public List<AudioClip> ballCollisionSounds = new List<AudioClip>();

    // Use this for initialization
    void Start () {
        loadSound();
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ballCollisionSounds.Count == 0)
        {
            loadSound();
        }
        if(!audioSource)
        {
            audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
        }
	}

    void loadSound()
    {
        Object[] sounds = Resources.LoadAll("Ball Collision", typeof(AudioClip));

        foreach (Object temp in sounds)
        {
            ballCollisionSounds.Add((AudioClip)temp);
        }
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        int randomSound = Random.Range(0, ballCollisionSounds.Count);
        audioSource.clip = ballCollisionSounds[randomSound];
        audioSource.Play();
    }
}
