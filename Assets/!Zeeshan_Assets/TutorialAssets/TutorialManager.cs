using ControlFreak2.Demos.SwipeSlasher;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager s_Instance;

    #region Tutorial Things
    [Foldout("Tutorial Icons")]
    public GameObject dragToWalk;
    [Foldout("Tutorial Icons")]
    public GameObject swipeToMove;
    [Foldout("Tutorial Icons")]
    public GameObject pressToSprint;
    [Foldout("Tutorial Icons")]
    public GameObject pressToJump;
    [Foldout("Tutorial Icons")]
    public GameObject pressToShoot;
    [Foldout("Tutorial Icons")]
    public GameObject pressToAim;
    [Foldout("Tutorial Icons")]
    public GameObject pressToReload;
    [Foldout("Tutorial Icons")]
    public GameObject pressToGernade;
    [Foldout("Tutorial Icons")]
    public GameObject pressToChangeWeapon;
    [Foldout("Tutorial Icons")]
    public GameObject pressToPickupWeapon;
    [Foldout("Tutorial Icons")]
    public GameObject mapHighlighter;
    [Foldout("Tutorial Icons")]
    public GameObject healthAndOther;
    #endregion

    #region Game UI Buttons
    [Foldout("Game UI Buttons")]
    public GameObject _joystick;
    [Foldout("Game UI Buttons")]
    public GameObject _lookAroundArea;
    [Foldout("Game UI Buttons")]
    public GameObject jumpButton;
    [Foldout("Game UI Buttons")]
    public GameObject sprintButton;
    [Foldout("Game UI Buttons")]
    public GameObject[] fireButtons;
    [Foldout("Game UI Buttons")]
    public GameObject reloadButton;
    [Foldout("Game UI Buttons")]
    public GameObject aimButton;
    [Foldout("Game UI Buttons")]
    public GameObject changingWeaponsButton;
    [Foldout("Game UI Buttons")]
    public GameObject gernadeButton;
    [Foldout("Game UI Buttons")]
    public GameObject[] healthAndOthers;
    #endregion

    private void Awake()
    {
        if(s_Instance == null)
        {
            s_Instance = this;
        }
    }

    void Start()
    {
        //dragToWalk.SetActive(true);
    }

    public void TutorialBehaviour(string eventName)
    {
        switch (eventName)
        {
            case "End Walk Tut":
                EndingWalkTut();
                return;

            case "End Move Tut":
                EndingMoveTut();
                return;

            case "End Run Tut":
                EndingRunTut();
                return;

            case "End Jump Tut":
                EndingJumpTut();
                return;

            case "End Shoot Tut":
                EndingShootTut();
                return;

            case "End Aim Tut":
                EndingAimTut();
                return;

            case "End Reload Tut":
                EndingReloadTut();
                return;

            case "End Gernade Tut":
                EndingGrenadeTut();
                return;

            case "End Changing Weapons Tut":
                EndingChangeWeaponsTut();
                return;

            case "End Pickup Weapons Tut":
                EndingPickupWeaponsTut();
                return;

            case "End Minimap Tut":
                EndingMapTut(); ;
                return;

            case "End Health Tut":
                EndingHealthTut();
                return;

            case "EndTheTutorial":
                EndingTheTutorial();
                return;
        }
    }

    void EndingWalkTut()
    {
        dragToWalk.SetActive(false);

        _joystick.SetActive(true);

        TutorialLevelManager.s_Instance.WeaponWheelDeactivator();
    }

    void EndingMoveTut()
    {
        swipeToMove.SetActive(false);

        Time.timeScale = 1;

        _lookAroundArea.SetActive(true);

        TutorialLevelManager.s_Instance.WeaponWheelDeactivator();
    }

    void EndingRunTut()
    {
        pressToSprint.SetActive(false);

        sprintButton.SetActive(true);

        Time.timeScale = 1.0f;

        TutorialLevelManager.s_Instance._gunChangeTutTrigger.SetActive(true);

        TutorialLevelManager.s_Instance.WeaponWheelDeactivator();
    }

    void EndingJumpTut()
    {
        pressToJump.SetActive(false);

        jumpButton.SetActive(true);

        Time.timeScale = 1.0f;

        StartCoroutine(SwipeTut());

        TutorialLevelManager.s_Instance.WeaponWheelDeactivator();
    }

    void EndingShootTut()
    {
        pressToShoot.SetActive(false);

        foreach (GameObject firebtns in fireButtons)
        {
            firebtns.SetActive(true);
        }

        Time.timeScale = 1f;

        TutorialLevelManager.s_Instance.WeaponWheelDeactivator();
    }

    void EndingAimTut()
    {
        pressToAim.SetActive(false);

        Time.timeScale = 1f;

        aimButton.SetActive(true);

        TutorialLevelManager.s_Instance.WeaponWheelDeactivator();

        StartCoroutine(ShootTut());
    }

    void EndingReloadTut()
    {
        pressToReload.SetActive(false);

        Time.timeScale = 1f;

        reloadButton.SetActive(true);

        StartCoroutine(MapTut());

        IEnumerator MapTut()
        {
            yield return new WaitForSeconds(4f);

            //Time.timeScale = 0f;

            mapHighlighter.SetActive(true);
        }

        TutorialLevelManager.s_Instance.WeaponWheelDeactivator();
    }

    void EndingMapTut()
    {
        mapHighlighter.SetActive(false);

        healthAndOther.SetActive(true);

        TutorialLevelManager.s_Instance.WeaponWheelDeactivator();
    }

    void EndingHealthTut()
    {
        healthAndOther.SetActive(false);

        foreach(GameObject hlthAothers in healthAndOthers)
        {
            hlthAothers.SetActive(true);
        }

        TutorialLevelManager.s_Instance._endingPanel.SetActive(true);

        TutorialLevelManager.s_Instance.WeaponWheelDeactivator();
    }

    void EndingGrenadeTut()
    {

    }

    void EndingChangeWeaponsTut()
    {
        pressToChangeWeapon.SetActive(false);

        Time.timeScale = 1f;

        changingWeaponsButton.SetActive(true);

        StartCoroutine(AimTut());

        TutorialLevelManager.s_Instance.WeaponWheelDeactivator();
    }

    void EndingPickupWeaponsTut()
    {

    }

    void EndingTheTutorial()
    {
        Time.timeScale = 1f;

        PlayerPrefs.SetInt("Tut", 1);

        if(PlayerPrefs.GetInt("ModeForTut") == 1)
        {
            
            MainMenuScript.instance.WayLevel(0);
        }

        TutorialLevelManager.s_Instance._loadingPanel.SetActive(true);
    }

    #region Coroutines


    IEnumerator SwipeTut()
    {
        yield return new WaitForSeconds(5f);

        swipeToMove.SetActive(true);

        Time.timeScale = 0f;
    }

    IEnumerator ShootTut()
    {
        yield return new WaitForSeconds(5f);

        pressToShoot.SetActive(true);

        Time.timeScale = 0f;
    }


    IEnumerator AimTut()
    {
        yield return new WaitForSeconds(5f);

        pressToAim.SetActive(true);

        TutorialLevelManager.s_Instance.WeaponWheelDeactivator();

        Time.timeScale = 0f;
    }

    #endregion
}
