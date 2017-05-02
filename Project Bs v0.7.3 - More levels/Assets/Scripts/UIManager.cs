using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform currentMount;
    public float speedFactor = 0.1f;

    public Slider[] volumeSliders;
    public Toggle tutorialTipsToggle;

    private void Start()
    {
        volumeSliders[0].value = AudioManager.instance.masterVolumePercent;
        volumeSliders[1].value = AudioManager.instance.musicVolumePercent;
        volumeSliders[2].value = AudioManager.instance.sfxVolumePercent;
        if (PlayerPrefs.GetInt("TutorialTipToggle") == 1) tutorialTipsToggle.isOn = true;
        else tutorialTipsToggle.isOn = false;
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            transform.position = Vector3.Lerp(transform.position, currentMount.position, speedFactor);
        }
    }

    public void SetMount(Transform newMount)
    {
        currentMount = newMount;
    }

    public void Play()
    {
        LivesManager.lives = 3;
        SceneManager.LoadScene("Level01");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetMasterVolume(Slider slider)
    {
        AudioManager.instance.SetVolume(slider.value, AudioManager.AudioChannel.MASTER);
    }

    public void SetMusicVolume(Slider slider)
    {
        AudioManager.instance.SetVolume(slider.value, AudioManager.AudioChannel.MUSIC);
    }

    public void SetSfxVolume(Slider slider)
    {
        AudioManager.instance.SetVolume(slider.value, AudioManager.AudioChannel.SFX);
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void TutorialToggle(bool value)
    {
        PlayerPrefs.SetInt("TutorialTipToggle", value?1:0);
        PlayerPrefs.Save();
    }
}
