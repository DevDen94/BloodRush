using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;

    
    public List<Transform> _waypoints = new();
    public GameObject[] afterEnableingCutsceneThings;
    public GameObject[] afterDisableingCutsceneThings;
    public Transform _startCamera;
    public float _lerpingSpeed;
    public NPCRegistry _npcRegistry;

    int _currentWaypoint = 0;

    [HideInInspector] public bool _cutsceneDone;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if(PlayerPrefs.GetInt("WaveNo") != 0)
        {
            gameObject.SetActive(false);
            return;
        }
        foreach (GameObject item in afterEnableingCutsceneThings)
        {
            item.SetActive(false);
        }
    }

    void Update()
    {
        if(!_cutsceneDone)
        {


            _cutsceneDone = true;
            GameManager.instance.CutSceneDelay();
            EnablingObjects();
            _npcRegistry.StartWalaKam();
            gameObject.SetActive(false);
            //float distance = Vector3.Distance(_waypoints[_currentWaypoint].position, _startCamera.position);
            //if (distance < 1)
            //{
            //    if (_currentWaypoint < _waypoints.Count - 1)
            //    {
            //        _currentWaypoint++;
            //    }
            //}
            //if (Vector3.Distance(_waypoints[_waypoints.Count - 1].position, _startCamera.position) < 1)
            //{
            //    _cutsceneDone = true;
            //    GameManager.instance.CutSceneDelay();
            //    EnablingObjects();
            //    _npcRegistry.StartWalaKam();
            //    gameObject.SetActive(false);
            //}
            //_startCamera.SetPositionAndRotation(Vector3.Lerp(_startCamera.position, _waypoints[_currentWaypoint].position, _lerpingSpeed), Quaternion.Slerp(_startCamera.rotation, _waypoints[_currentWaypoint].rotation, _lerpingSpeed));
        }
    }

    public void EnablingObjects()
    {
        foreach (GameObject item in afterEnableingCutsceneThings)
        {
            item.SetActive(true);
        }

        foreach (GameObject item in afterDisableingCutsceneThings)
        {
            item.SetActive(false);
        }
    }

}
