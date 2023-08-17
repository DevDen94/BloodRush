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
        src.PlayOneShot(BtnClickSound);
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    public void WayLevel(int way = 0)
    {
        PlayerPrefs.SetInt("WaveNo", way);
        LoaddingPanel.SetActive(true);
    }
}
