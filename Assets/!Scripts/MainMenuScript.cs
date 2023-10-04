using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuScript : MonoBehaviour
{
    public GameObject LoaddingPanel;
    public AudioSource src;
    public AudioClip BtnClickSound;
    public GameObject[] LockedImages;
    public Button[] LevelBtns;
    public Slider[] MusicSlider;
    public VersionNumber number;
    public Text VersionNumber;

    public GameObject _levelsPanel;

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
        

        Firebase.Analytics.FirebaseAnalytics.LogEvent("Mode_Selection", "mode_Number", mode.ToString());

        if(PlayerPrefs.GetInt("Tut") == 0)
        {
            LoaddingPanel.SetActive(true);
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
        /*   AdsManager.instance.ShowSmallBanner();
           if (AdsManager.instance.isAppOpen)
           {
               AdsManager.instance.ShowAppOpenAd();
               AdsManager.instance.isAppOpen = false;
           }*/

        GoogleAdMobController.instance.ShowSmallBannerAd();

        VersionNumber.text = number.Playstore_Version + " : " + number.Appstore_Version;
        //Screen.sleepTimeout = SleepTimeout.NeverSleep;

       

        if (!PlayerPrefs.HasKey("Zero"))
        {
            PlayerPrefs.SetInt("Zero", 1);
            
            PlayerPrefs.SetFloat("Music", 0.5f);
            PlayerPrefs.SetFloat("Sounds", 0.5f);
        }
        DisableAll();
        EnableButtons(PlayerPrefs.GetInt("WaveUnlock"));
        MusicSlider[0].value = PlayerPrefs.GetFloat("Music");
        MusicSlider[1].value= PlayerPrefs.GetFloat("Sounds");
     
        src.volume= PlayerPrefs.GetFloat("Music");


        if (GoogleAdMobController.instance.IsAppOpen)
        {
            GoogleAdMobController.instance.ShowAppOpenAd();
            GoogleAdMobController.instance.IsAppOpen = false;
        }
     
    }


    public void showInter()
    {
        // AdsManager.instance.ShowinterAd();
        GoogleAdMobController.instance.ShowInterstitialAd();
    }


    public void ButtonClick()
    {
        src.PlayOneShot(BtnClickSound);
       
    }
    // Update is called once per frame
  public void SaveSetting()
    {
        PlayerPrefs.SetFloat("Music", MusicSlider[0].value);
        PlayerPrefs.SetFloat("Sounds", MusicSlider[1].value);
        src.volume = PlayerPrefs.GetFloat("Music");
    }


  
    public void BTN_cLICK()
    {
        src.PlayOneShot(BtnClickSound);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void WayLevel(int way = 0)
    {
        //AdsManager.instance.ShowinterAd();
        PlayerPrefs.SetInt("WaveNo", way);
        LoaddingPanel.SetActive(true);
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

}
