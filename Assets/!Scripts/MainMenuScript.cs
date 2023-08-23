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
    void Start()
    {

        if (!SoundsManager.instance.IsPlayMainMenuSound)
        {
            SoundsManager.instance.PlayGameplayMusic();
        }

        if (!PlayerPrefs.HasKey("Zero"))
        {
            PlayerPrefs.SetInt("Zero", 1);
            PlayerPrefs.SetInt("WaveUnlock", 0);
        }
        DisableAll();
        EnableButtons(PlayerPrefs.GetInt("WaveUnlock"));
    }

    public void ButtonClick()
    {
        //src.PlayOneShot(BtnClickSound);
        SoundsManager.instance.playBtnClick();
    }
    // Update is called once per frame
    void Update()
    {
        
    }



    public void SaveSettings()
    {
        
        PlayerPrefs.SetFloat("MusicVoulme", MusicSlider[0].value);
        PlayerPrefs.SetFloat("SFXVoulme", MusicSlider[1].value);
    }

    public void WayLevel(int way = 0)
    {
        PlayerPrefs.SetInt("WaveNo", way);
        LoaddingPanel.SetActive(true);
    }

    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
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
