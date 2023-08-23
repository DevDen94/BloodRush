using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public AudioSource MainMenuSound, ButtonClickSound;
    public bool IsPlayMainMenuSound;


    public static SoundsManager instance;

    private void Update()
    {
        MainMenuSound.volume = PlayerPrefs.GetFloat("MusicVoulme");
      //  GamePlaySound.volume = PlayerPrefs.GetFloat("MusicVoulme");
    }



    private void Awake()
    {
        if (instance == null)
        {
            PlayerPrefs.SetFloat("MusicVoulme", 0.5f);
            instance = this;
        }
        else
        {
            //Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);
    }

    public void PlayMainMenuMusic()
    {
        IsPlayMainMenuSound = true;
        MainMenuSound.Play();
     
    }

    public void PlayGameplayMusic()
    {
        IsPlayMainMenuSound = false;
        MainMenuSound.Stop();
      
    }

    public void playBtnClick()
    {
        ButtonClickSound.Play();
    }

}
