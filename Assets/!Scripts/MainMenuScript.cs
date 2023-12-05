using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour
{
    public static MainMenuScript instance;

    public GameObject LoaddingPanel;
    public AudioSource src;
    public AudioSource _buttonClickSrc;
    public AudioClip BtnClickSound;
    public GameObject[] LockedImages;
    public Button[] LevelBtns;
    public Slider[] MusicSlider;
    public VersionNumber number;
    public Text VersionNumber;

    public GameObject _levelsPanel;

    public GameObject[] _levelPanels;
    public GameObject _levelRightBtn;
    public GameObject _levelLeftBtn;
    int _currentLevelPanelsIndex;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void DisableAll()
    {
        foreach(GameObject a in LockedImages)
        {
            a.SetActive(true);
        }
        foreach(Button a in LevelBtns)
        {
            a.interactable = false;
        }
    }
    void EnableButtons(int a)
    {
        for(int i = 0; i <= a; i++)
        {
            LockedImages[i].SetActive(false);
            LevelBtns[i].interactable = true;
        }
    }
    public void ModeSelect(int mode)
    {

        //Firebase.Analytics.FirebaseAnalytics.LogEvent("Mode_Selection", "mode_Number", mode.ToString());

        if(PlayerPrefs.GetInt("Tut") == 0)
        {
            LoaddingPanel.SetActive(true);
            PlayerPrefs.SetInt("ModeForTut", mode);
        }
        else
        {
            PlayerPrefs.SetInt("Mode", mode);
            if (mode == 1)
            {
                _levelsPanel.SetActive(true);
            }
            else if (mode == 2)
            {
                LoaddingPanel.SetActive(true);
            }
        }
    }
    void Start()
    {
        //PlayerPrefs.SetInt("Tut", 1);

        /*   AdsManager.instance.ShowSmallBanner();
           if (AdsManager.instance.isAppOpen)
           {
               AdsManager.instance.ShowAppOpenAd();
               AdsManager.instance.isAppOpen = false;
           }*/

        GoogleAdMobController.instance.ShowSmallBannerAd();

        _levelPanels[_currentLevelPanelsIndex].SetActive(true);

        Time.timeScale = 1f;

        VersionNumber.text = number.Playstore_Version + " : " + number.Appstore_Version;
        //Screen.sleepTimeout = SleepTimeout.NeverSleep;

       

        if (!PlayerPrefs.HasKey("Zero"))
        {
            PlayerPrefs.SetInt("Zero", 1);
            
            PlayerPrefs.SetFloat("Music", 0.5f);
            PlayerPrefs.SetFloat("Sounds", 0.5f);
        }
        DisableAll();
        Debug.Log("wave unlock " + PlayerPrefs.GetInt("WaveUnlock")); 
        EnableButtons(PlayerPrefs.GetInt("WaveUnlock"));

        MusicSlider[0].value = PlayerPrefs.GetFloat("Music");
        MusicSlider[1].value= PlayerPrefs.GetFloat("Sounds");
     
        src.volume= PlayerPrefs.GetFloat("Music");
        _buttonClickSrc.volume = PlayerPrefs.GetFloat("Sounds");

        //if (GoogleAdMobController.instance.IsAppOpen)
        //{
        //    GoogleAdMobController.instance.ShowAppOpenAd();
        //    GoogleAdMobController.instance.IsAppOpen = false;
        //}

    }


    public void showInter()
    {
        // AdsManager.instance.ShowinterAd();
        GoogleAdMobController.instance.ShowInterstitialAd();
    }


    public void ButtonClick()
    {
        //src.PlayOneShot(BtnClickSound);

        _buttonClickSrc.Play();
    }
    // Update is called once per frame
  public void SaveSetting()
    {
        PlayerPrefs.SetFloat("Music", MusicSlider[0].value);
        PlayerPrefs.SetFloat("Sounds", MusicSlider[1].value);
        src.volume = PlayerPrefs.GetFloat("Music");
        _buttonClickSrc.volume = PlayerPrefs.GetFloat("Sounds");
    }

    public void SoundBehaviour()
    {
        src.volume = MusicSlider[0].value;
        _buttonClickSrc.volume = MusicSlider[1].value;
    }
  
    public void SettingsBack()
    {
        src.volume = PlayerPrefs.GetFloat("Music");
        _buttonClickSrc.volume = PlayerPrefs.GetFloat("Sounds");

        MusicSlider[0].value = PlayerPrefs.GetFloat("Music");
        MusicSlider[1].value = PlayerPrefs.GetFloat("Sounds");
    }

    public void BTN_cLICK()
    {
        _buttonClickSrc.Play();
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void WayLevel(int way = 0)
    {
        //AdsManager.instance.ShowinterAd();
        PlayerPrefs.SetInt("WaveNo", way);
        if(SceneManager.GetActiveScene().name != "Tutorial")
        {
            LoaddingPanel.SetActive(true);
        }
    }

    public void ModeSelection()
    {

    }

    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.darwin.zombie.shooting");
    }

    public void MoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Darwin+Games");
    }

    public void PP()
    {
        Application.OpenURL("https://darwingames1.blogspot.com/2023/06/privacy-policy.html");
    }

    public void exit()
    {
        Application.Quit();
    }

    public void LevelsRight()
    {
        _levelPanels[_currentLevelPanelsIndex].SetActive(false);
        _currentLevelPanelsIndex++;
        _levelLeftBtn.SetActive(true);

        if(_currentLevelPanelsIndex >= _levelPanels.Length - 1)
        {
            _levelRightBtn.SetActive(false);
            _currentLevelPanelsIndex = _levelPanels.Length - 1;
        }

        _levelPanels[_currentLevelPanelsIndex].SetActive(true);
    }

    public void LevelsLeft()
    {
        _levelPanels[_currentLevelPanelsIndex].SetActive(false);
        _currentLevelPanelsIndex--;
        _levelRightBtn.SetActive(true);

        if (_currentLevelPanelsIndex <= 0)
        {
            _levelLeftBtn.SetActive(false);
            _currentLevelPanelsIndex = 0;
        }

        _levelPanels[_currentLevelPanelsIndex].SetActive(true);
    }
}
