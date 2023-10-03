using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevelManager : MonoBehaviour
{
    public static TutorialLevelManager s_Instance;

    public Transform _player;
    public Transform _spawnpoint;
    public GameObject _weaponsToPickup;
    public GameObject _endingPanel;
    public GameObject _zombie;
    public int _bulletCount;

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
    }

    void Start()
    {
        _player.SetPositionAndRotation(_spawnpoint.position, _spawnpoint.rotation);
    }

    public void CountIncreaser()
    {
        _bulletCount+=1;
        if(_bulletCount > 2)
        {
            _zombie.SetActive(false);

            Time.timeScale = 0f;

            TutorialManager.s_Instance.pressToReload.SetActive(true);
        }
    }
}
