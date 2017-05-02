using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndHUDManager : MonoBehaviour {

	public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
