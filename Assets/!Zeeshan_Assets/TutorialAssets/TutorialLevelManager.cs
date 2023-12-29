using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour
{
    public static TutorialLevelManager s_Instance;

    public AudioSource[] m_allAudios;

    public Transform _player;
    public GameObject _weaponsToPickup;
    public GameObject _endingPanel;
    public GameObject _loadingPanel;
    public GameObject _zombie;
    public GameObject _weaponWheel;
    public GameObject Reloading_Slider;
    public AudioSource src;
    public AudioClip ReloadingClip;

    public AudioSource[] _chatSounds;

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
    }

    void Start()
    {
        GoogleMobileAdsController.Instance.ShowSmallBannerAd();

        m_allAudios = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audios in m_allAudios)
        {
            audios.volume = PlayerPrefs.GetFloat("Music");
        }

        SoundManage();

        CharacterDamage.zombieDied += ZombieDied;
    }

    void ZombieDied()
    {
        Time.timeScale = 0f;

        TutorialManager.s_Instance.pressToReload.SetActive(true);
    }

    public void WeaponWheelDeactivator()
    {
        _weaponWheel.SetActive(false);
    }

    void SoundManage()
    {
        for(int i = 0; i < _chatSounds.Length; i++)
        {
            _chatSounds[i].volume = PlayerPrefs.GetFloat("Music");
        }
    }
}
