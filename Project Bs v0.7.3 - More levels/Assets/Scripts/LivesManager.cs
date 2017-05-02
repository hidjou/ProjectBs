using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour {

    public Text lifeNumberText;

    public static int lives;
    
    public static LivesManager instance;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        lifeNumberText.text = "x " + lives;
    }

    public void AddLife()
    {
        lives++;
    }

    public void SubtractLife()
    {
        lives--;
    }
}
