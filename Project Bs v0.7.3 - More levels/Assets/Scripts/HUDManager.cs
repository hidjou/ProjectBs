using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    TipLibrary.CCDInfo randomTip;
    TipLibrary.CCDVideo randomVideo;

    public Transform pauseCanvas;
    public Transform deathCanvas;
    public Transform HUDCanvas;
    public Transform noLivesText;

    public Transform tipText;
    public Transform tipLink;

    public Transform videoText;
    public Transform videoLink;

    public RectTransform newLevelBanner;
    public RectTransform tutorialTipBanner;
    public Text newLevelTitle;
    public Text newLevelTutorialTip;

    public Text didYouKnowText;

    string tipURL;
    string videoURL;

    public Image fadePlane;

    bool showTutorialTips = true;
    bool deathScreen = false;

    float videoWatchingDelay = 30;
    bool watchingVideo = false;
    float timeAtVideoClick;

    private void Awake()
    {
        FindObjectOfType<Player>().OnPlayerDeath += OnGameOver;

        if (PlayerPrefs.GetInt("TutorialTipToggle") == 1) showTutorialTips = true;
        else showTutorialTips = false;
    }

    private void Start()
    {
        OnNewLevel(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseCanvas.gameObject.activeInHierarchy == false)
            {
                HUDCanvas.gameObject.SetActive(false);
                pauseCanvas.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                HUDCanvas.gameObject.SetActive(true);
                pauseCanvas.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }

        if(watchingVideo)
        {
            if(Time.realtimeSinceStartup > (timeAtVideoClick + videoWatchingDelay))
            {
                print("30 seconds elaplsed from ");
                LivesManager.instance.AddLife();
                LivesManager.instance.AddLife();
                LivesManager.instance.AddLife();
                watchingVideo = false;
            }
        }
    }

    public void OpenVideoURL()
    {
        if(!watchingVideo)
        {
            Application.OpenURL(videoURL);
            timeAtVideoClick = Time.realtimeSinceStartup;
            watchingVideo = true;
            print("watching Video !!!");
        }
    }

    public void OnNewLevel(int levelNumber)
    {
        newLevelTitle.text = "-- Level " + levelNumber + " --";
        StartCoroutine(AnimateBanner(newLevelBanner, -140, 0, 1f));
        if (showTutorialTips && (TipLibrary.instance.tutorialTips.Length >= SceneManager.GetActiveScene().buildIndex))
        {
            StartCoroutine(AnimateBanner(tutorialTipBanner, 120, 55, 3f));
            newLevelTutorialTip.text = TipLibrary.instance.tutorialTips[SceneManager.GetActiveScene().buildIndex - 1].tipText;
        }
    }

    public void ResumeGame()
    {
        HUDCanvas.gameObject.SetActive(true);
        pauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void PlayAgain()
    {
        if(LivesManager.lives > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
        else
        {
            // Generate random video
            randomVideo = TipLibrary.instance.GenerateRandomVideo();
            videoURL = randomVideo.videoLink;

            // Change HUD elements
            noLivesText.gameObject.SetActive(true);
            
            tipText.gameObject.SetActive(false);
            tipLink.gameObject.SetActive(false);

            videoText.gameObject.SetActive(true);
            videoLink.gameObject.SetActive(true);
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void OpenTipURL()
    {
        Application.OpenURL(tipURL);
    }

    

    void OnGameOver()
    {
        deathCanvas.gameObject.SetActive(true);
        StartCoroutine(Fade(Color.clear, Color.black, 0.35f));

        randomTip = TipLibrary.instance.GenerateRandomTip();
        didYouKnowText.text = randomTip.tipText;
        tipURL = randomTip.URL;
    }

    IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;

        while(percent < 1)
        {
            percent += Time.deltaTime * speed;
            fadePlane.color = Color.Lerp(from, to, percent);
            yield return null;
        }
    }

    IEnumerator AnimateBanner(RectTransform banner, int fromPos, int toPos, float delayTime)
    {
        float speed = 2.5f;
        float animationPercent = 0;
        int dir = 1;

        float endDelayTime = Time.time + 1 / speed + delayTime;

        while (animationPercent >= 0)
        {
            animationPercent += Time.deltaTime * speed * dir;

            if (animationPercent >= 1)
            {
                animationPercent = 1;
                if (Time.time > endDelayTime)
                {
                    dir = -1;
                }
            }
            banner.anchoredPosition = Vector2.up * Mathf.Lerp(fromPos, toPos, animationPercent);
            yield return null;
        }
    }
}
