using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour
{
    public static TutorialLevelManager s_Instance;

    public Transform _player;
    public GameObject _weaponsToPickup;
    public GameObject _endingPanel;
    public GameObject _loadingPanel;
    public GameObject _zombie;

   

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }


    }

    void Start()
    {

        CharacterDamage.zombieDied += ZombieDied;
    }

    void ZombieDied()
    {
        Time.timeScale = 0f;

        TutorialManager.s_Instance.pressToReload.SetActive(true);
    }
}
