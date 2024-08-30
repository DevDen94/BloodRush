using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Dan.Demo;
public class MainMenuScript : MonoBehaviour
{
    public static MainMenuScript instance;

    public GameObject LoaddingPanel;
    public AudioSource src;
    public AudioSource _buttonClickSrc;
    public AudioSource _buttonClickSrc2;
    public AudioSource _buttonClickSrc3;
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

    public GameObject _unlockAllLevelButton;
    [HideInInspector] public Text _UnlockAllLevelCounterText;

    public Button _survivalModeButton;
    public Button _adButtonForSurvivalMode;
    public Text _survivalModeUnlockAdsCounterText;
    public GameObject NamePanel,MainMenuPanel;
    public Text PlayerNameTXt;
    public InputField UserNameTXt;

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
        if(a > 3)
        {
            _adButtonForSurvivalMode.gameObject.SetActive(false);
            _survivalModeButton.interactable = true;
        }

        for(int i = 0; i <= a; i++)
        {
            LockedImages[i].SetActive(false);
            LevelBtns[i].interactable = true;
        }
    }
    public void ModeSelect(int mode)
    {

        //Firebase.Analytics.FirebaseAnalytics.LogEvent("Mode_Selection", "mode_Number", mode.ToString());

        if(PlayerPrefs.GetInt("Tut") == 3)//0
        {
            LoaddingPanel.SetActive(true);
            PlayerPrefs.SetInt("ModeForTut", mode);
        }
        else
        {
            PlayerPrefs.SetInt("Mode", mode);
           
            if (mode == 1)
            {
                if(PlayerPrefs.GetInt("WaveUnlock") < LevelBtns.Length - 2)
                {
                    CheckingAdButton();
                }
                _levelsPanel.SetActive(true);
            }
            else if (mode == 2)
            {
                //Admob.Instance.ShowInterstitialAd();
                
                LoaddingPanel.SetActive(true);
            }
            else if (mode == 3)
            {
               
               
                LoaddingPanel.SetActive(true);
            }
            else if (mode == 4)
            {


                LoaddingPanel.SetActive(true);
            }
        }
    }
    void Start()
    {
        //Admob.Instance.ShowSmallBanner();
        



        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;

        if (PlayerPrefs.GetInt("WaveUnlock") < LevelBtns.Length - 2)
        {
            CheckingAdButton();
        }

        if (PlayerPrefs.GetInt("SurvivalModeAd") > 2)
        {
            _adButtonForSurvivalMode.gameObject.SetActive(false);
        }

        _survivalModeUnlockAdsCounterText.text = PlayerPrefs.GetInt("SurvivalModeAd") + "/3";

        _UnlockAllLevelCounterText.text = PlayerPrefs.GetInt("UnlockAllLevels") + "/3";

        if(PlayerPrefs.GetInt("UnlockAllLevels") >= 2)
        {
            _unlockAllLevelButton.SetActive(false);
        }

        _levelPanels[_currentLevelPanelsIndex].SetActive(true);

        Time.timeScale = 1f;

        VersionNumber.text = number.Playstore_Version + " : " + number.Appstore_Version;
        //Screen.sleepTimeout = SleepTimeout.NeverSleep;

       

        if (!PlayerPrefs.HasKey("Zero"))
        {
           
            
            PlayerPrefs.SetFloat("Music", 0.5f);
            PlayerPrefs.SetFloat("Sounds", 0.5f);
            NamePanel.SetActive(true);
            MainMenuPanel.SetActive(false);
            OnInputFieldSelected(UserNameTXt.text);
        }
        else
        {
            NamePanel.SetActive(false);
            PlayerNameTXt.text = PlayerPrefs.GetString("PlayerName").ToString();
            MainMenuPanel.SetActive(true);
            LeaderboardShowcase.Instance.Submit();
        }
        DisableAll();
        Debug.Log("wave unlock " + PlayerPrefs.GetInt("WaveUnlock")); 
        EnableButtons(PlayerPrefs.GetInt("WaveUnlock"));

        MusicSlider[0].value = PlayerPrefs.GetFloat("Music");
        MusicSlider[1].value= PlayerPrefs.GetFloat("Sounds");
     
        src.volume= PlayerPrefs.GetFloat("Music");
        _buttonClickSrc.volume = PlayerPrefs.GetFloat("Sounds");
        _buttonClickSrc2.volume = PlayerPrefs.GetFloat("Sounds");
        _buttonClickSrc3.volume = PlayerPrefs.GetFloat("Sounds");
        


    }

    public void ShowInterPlay()
    {
        if(PlayerPrefs.GetInt("PlayAd") % 2 == 0)
        {
            
        }

        PlayerPrefs.SetInt("PlayAd", PlayerPrefs.GetInt("PlayAd") + 1);
    }

    public void showInter()
    {
        
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
        _buttonClickSrc2.volume = PlayerPrefs.GetFloat("Sounds");
        _buttonClickSrc3.volume = PlayerPrefs.GetFloat("Sounds");
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
        _buttonClickSrc2.volume = PlayerPrefs.GetFloat("Sounds");
        _buttonClickSrc3.volume = PlayerPrefs.GetFloat("Sounds");

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
        Debug.Log("wave pref is : " + way);
        //AdsManager.instance.ShowinterAd();
        //Admob.Instance.ShowInterstitialAd();
        

        PlayerPrefs.SetInt("WaveNo", way);

        if(SceneManager.GetActiveScene().name != "Tutorial")
        {
            LoaddingPanel.SetActive(true);
        }
    }


    public void SaveName()
    {
        /*if (string.IsNullOrEmpty(UserNameTXt.text))
            return;*/
       
        NamePanel.SetActive(false);
       MainMenuPanel.SetActive(true);
        PlayerPrefs.SetString("PlayerName", UserNameTXt.text);
        PlayerPrefs.SetInt("Zero", 1);
        PlayerNameTXt.text = PlayerPrefs.GetString("PlayerName").ToString();
        
    }
    public void ModeSelection()
    {

    }

    public void OnInputFieldSelected(string text)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            // Trigger Unity's built-in keyboard for WebGL on mobile browsers
            TouchScreenKeyboard.Open(UserNameTXt.text, TouchScreenKeyboardType.Default);
#endif
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

    public void ResetLevelsPanel()
    {
        foreach(GameObject L_panels in _levelPanels)
        {
            L_panels.SetActive(false);
        }
        _levelPanels[0].SetActive(true);
        _currentLevelPanelsIndex = 0;
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

    #region Rewarded Ads Integration

    public void CheckingAdButton()
    {
        foreach (Button b in LevelBtns)
        {
            b.transform.GetChild(2).gameObject.SetActive(false);
        }

        int adButtonToshow = PlayerPrefs.GetInt("WaveUnlock") + 1;
        LevelBtns[adButtonToshow].transform.GetChild(2).gameObject.SetActive(true);

        EnableButtons(adButtonToshow - 1);
    }

    public void UnlockAllLevelsRewardAd()
    {
       // Admob.Instance.isRewarded = true;
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
           

            PlayerPrefs.SetInt("ShowLevelsAd", 1);
        }


        //Admob.Instance.ShowRewardedAd();
       
    }

    public void UnlockingAllLevels()
    {
        
        PlayerPrefs.SetInt("UnlockAllLevels", PlayerPrefs.GetInt("UnlockAllLevels") + 1);
        /*if (PlayerPrefs.GetInt("UnlockAllLevels") > 2)
        {
            _unlockAllLevelButton.SetActive(false);
        }*/
        _UnlockAllLevelCounterText.text = PlayerPrefs.GetInt("UnlockAllLevels") + "/3";
       /* PlayerPrefs.SetInt("UnlockAllLevels", PlayerPrefs.GetInt("UnlockAllLevels") + 1);
        _UnlockAllLevelCounterText.text = PlayerPrefs.GetInt("UnlockAllLevels") + "/3";*/
        if (PlayerPrefs.GetInt("UnlockAllLevels") > 2)
        {
            _unlockAllLevelButton.SetActive(false);
            PlayerPrefs.SetInt("WaveUnlock", LevelBtns.Length - 1);

            foreach (Button b in LevelBtns)
            {
                b.transform.GetChild(2).gameObject.SetActive(false);
            }

            PlayerPrefs.SetFloat("START", 1);
            Debug.Log(PlayerPrefs.GetInt("WaveUnlock"));
            EnableButtons(PlayerPrefs.GetInt("WaveUnlock"));
        }
        //Admob.Instance.isRewarded = false;

    }

    public void UnlockNextLevelRewardedAd()
    {
        if(Application.internetReachability != NetworkReachability.NotReachable)
        {
            PlayerPrefs.SetInt("UnlockNextLevel", 1);
        }
       // Admob.Instance.ShowRewardedAd();
       
       // Admob.Instance.isRewarded = true;
    }

    public void UnlockingNextLevel()
    {
        PlayerPrefs.SetInt("WaveUnlock", PlayerPrefs.GetInt("WaveUnlock") + 1);

        foreach(Button b in LevelBtns)
        {
            b.transform.GetChild(2).gameObject.SetActive(false);
        }

        int adButtonToshow = PlayerPrefs.GetInt("WaveUnlock") + 1;
        LevelBtns[adButtonToshow].transform.GetChild(2).gameObject.SetActive(true);
        EnableButtons(adButtonToshow - 1);
    }

    public void UnlockSurvivalMode()
    {
       // Admob.Instance.ShowRewardedAd();
        //GoogleMobileAdsController.Instance.ShowRewardedAd_();
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            //PlayerPrefs.SetInt("SurvivalModeAd", PlayerPrefs.GetInt("SurvivalModeAd") + 1);
            //if(PlayerPrefs.GetInt("SurvivalModeAd") > 2)
            //{
            //    _adButtonForSurvivalMode.gameObject.SetActive(false);
            //}
            //_survivalModeUnlockAdsCounterText.text = PlayerPrefs.GetInt("SurvivalModeAd") + "/3";
            PlayerPrefs.SetInt("ShowModeAd", 1);
        }


        
        //Admob.Instance.isRewarded = true;
    }

    public void UnlockingSurvivalModeWithAds()
    {
        PlayerPrefs.SetInt("SurvivalModeAd", PlayerPrefs.GetInt("SurvivalModeAd") + 1);
        _survivalModeUnlockAdsCounterText.text = PlayerPrefs.GetInt("SurvivalModeAd") + "/3";
        if (PlayerPrefs.GetInt("SurvivalModeAd") > 2)
        {
            _adButtonForSurvivalMode.gameObject.SetActive(false);
            _survivalModeButton.interactable = true;
        }
        //_adButtonForSurvivalMode.gameObject.SetActive(false);
        //PlayerPrefs.SetInt("Tut", 1);
    }

    #endregion 
}
