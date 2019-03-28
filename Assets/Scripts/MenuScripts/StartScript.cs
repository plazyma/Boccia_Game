using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScript : MonoBehaviour {


    public void Onclick()
    {

        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene("PlayerSelection", LoadSceneMode.Single);
    }

    public void QuickPlay()
    {

        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
