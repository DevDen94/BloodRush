using ControlFreak2.Demos.SwipeSlasher;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Foldout("Tutorial Icons")]
    public GameObject swipeToWalk;
    [Foldout("Tutorial Icons")]
    public GameObject swipeToMove;
    [Foldout("Tutorial Icons")]
    public GameObject pressToSprint;
    [Foldout("Tutorial Icons")]
    public GameObject pressToJump;
    [Foldout("Tutorial Icons")]
    public GameObject pressToShoot;
    [Foldout("Game UI Buttons")]
    public GameObject _joystick;
    [Foldout("Game UI Buttons")]
    public GameObject jumpButton;
    [Foldout("Game UI Buttons")]
    public GameObject sprintButton;
    [Foldout("Game UI Buttons")]
    public GameObject[] fireButtons;
    [Foldout("Game UI Buttons")]
    public GameObject relodButton;
    [Foldout("Game UI Buttons")]
    public GameObject aimButton;
    [Foldout("Game UI Buttons")]
    public GameObject changingWeaponsButton;
    [Foldout("Game UI Buttons")]
    public GameObject gernadeButton;
    [Foldout("Game UI Buttons")]
    public GameObject[] healthAndOthers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
