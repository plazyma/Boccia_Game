using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScript : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip buttonClick;

    public void Onclick()
    {
        audioSource.clip = buttonClick;
        audioSource.Play();
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
