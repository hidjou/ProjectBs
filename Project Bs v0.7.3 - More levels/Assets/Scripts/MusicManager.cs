using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    int currentSceneIndex;
    static int musicIndex;

    private void Start()
    {
        musicIndex = 1;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        AudioManager.instance.PlayMusic("Level2Music", 2);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > currentSceneIndex)
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if(currentSceneIndex == 13)
            {
                AudioManager.instance.PlayMusic("End", 2);
                return;
            }

            if (musicIndex == 1)
            {
                AudioManager.instance.PlayMusic(("Level" + musicIndex + "Music"), 2);
                musicIndex = 2;
            }
            else
            {
                AudioManager.instance.PlayMusic(("Level" + musicIndex + "Music"), 2);
                musicIndex = 1;
            }
        }
    }
}
