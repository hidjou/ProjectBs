using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TipLibrary : MonoBehaviour {

    public CCDInfo[] CCPInfoTips;
    public CCDVideo[] CCDVideoLibrary;
    public TutorialTip[] tutorialTips;
    public CheatCode[] cheatCodes;

    public static TipLibrary instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public CCDInfo GenerateRandomTip()
    {
        return CCPInfoTips[Random.Range(0, CCPInfoTips.Length)];
    }

    public CCDVideo GenerateRandomVideo()
    {
        return CCDVideoLibrary[Random.Range(0, CCDVideoLibrary.Length)];
    }

    [System.Serializable]
    public class CCDInfo
    {
        public string tipText;
        public string URL;
    }

    [System.Serializable]
    public class TutorialTip
    {
        public int levelIndex;
        public string tipText;
    }

    [System.Serializable]
    public class CheatCode
    {
        public int levelIndex;
        public string[] cheatCode;
    }

    [System.Serializable]
    public class CCDVideo
    {
        public string videoLink;
    }
}
