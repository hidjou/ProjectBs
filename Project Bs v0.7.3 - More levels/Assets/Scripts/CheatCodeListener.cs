using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CheatCodeListener : MonoBehaviour {

    string[] cheatCode;
    private int index;

    void Start()
    {
        cheatCode = TipLibrary.instance.cheatCodes[SceneManager.GetActiveScene().buildIndex - 1].cheatCode;
        index = 0;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(cheatCode[index]))
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }

        // If index reaches the length of the cheatCode string, 
        // the entire code was correctly entered
        if (index == cheatCode.Length)
        {
            // Jump to next level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
