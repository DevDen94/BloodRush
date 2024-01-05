using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{

    public GameObject childPrefab; // Reference to the prefab of the child GameObject
    public Transform parentObject; // Reference to the parent GameObject's Transform component
    public Data data;
   public int Value;
    Scene activeScene;

    AsyncOperation asyncOperation;
    public float Wait;
    public Image FillBar;

    private void Start()
    {
        PlayerPrefs.SetInt("Tut", 1);

        if (data.data == Application.identifier)
        {
            Value = 0;
            StartCoroutine(waitforSceneSwitch());

            //StartCoroutine(LoadingScene());
            Debug.LogError("AUA");
            activeScene = SceneManager.GetActiveScene();
            Invoke(nameof(ShowBig), 1f);
        }
        else
        {
            Application.Quit();
        }
        //StartCoroutine(waitforSceneSwitch());
        // SoundsManager.instance.PlayMainMenuMusic();
    }

    public void ShowBig()
    {
        //GoogleAdMobController.instance.ShowBigBannerAd();
        GoogleMobileAdsController.Instance.ShowBiGBannerAd();
    }


    IEnumerator waitforSceneSwitch()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.1f);
        
        // Instantiate the child GameObject from the prefab
        GameObject instantiatedChild = Instantiate(childPrefab, childPrefab.transform.position, childPrefab.transform.rotation);
        
        // Set the parent of the instantiated child to the desired parent GameObject
        instantiatedChild.transform.parent = parentObject;
        instantiatedChild.transform.localScale = new Vector3(1f, 1f, 1f);
        Value =Value + 1;

        FillBar.fillAmount = Value / 50f;

        CheckTime();
    }

    public void CheckTime()
    {
        if (Value >= 70)//71
        {
            StopCoroutine(waitforSceneSwitch());
            if (activeScene.name == "Splash")
            {
                SceneManager.LoadSceneAsync("MainMenu");
            }
            else if (activeScene.name == "MainMenu")
            {
                if (PlayerPrefs.GetInt("Tut") == 0)
                {
                    SceneManager.LoadSceneAsync("Tutorial");
                }
                else
                {
                    if (PlayerPrefs.GetInt("Mode") == 1)
                    {
                        SceneManager.LoadSceneAsync("GamePlay");
                    }
                    else
                    {
                        SceneManager.LoadSceneAsync("SurvivalMode");
                    }
                }
            }
            else if (activeScene.name == "Tutorial")
            {
                PlayerPrefs.SetInt("Mode", PlayerPrefs.GetInt("ModeForTut"));
                if (PlayerPrefs.GetInt("ModeForTut") == 1)
                {
                    SceneManager.LoadSceneAsync("GamePlay");
                }
                else if (PlayerPrefs.GetInt("ModeForTut") == 2)
                {
                    SceneManager.LoadSceneAsync("SurvivalMode");
                }
            }
        }
        else
        {
            StartCoroutine(waitforSceneSwitch());
        }
    }

    IEnumerator LoadingScene()
    {
        yield return new WaitForSeconds(Wait);

        if (activeScene.name == "Splash")
        {
            asyncOperation = SceneManager.LoadSceneAsync("MainMenu");
        }
        else if (activeScene.name == "MainMenu")
        {
            if (PlayerPrefs.GetInt("Tut") == 0)
            {
                asyncOperation = SceneManager.LoadSceneAsync("Tutorial");
            }
            else
            {
                if (PlayerPrefs.GetInt("Mode") == 1)
                {
                    asyncOperation = SceneManager.LoadSceneAsync("GamePlay");
                }
                else
                {
                    asyncOperation = SceneManager.LoadSceneAsync("SurvivalMode");
                }
            }
        }
        else if (activeScene.name == "Tutorial")
        {
            PlayerPrefs.SetInt("Mode", PlayerPrefs.GetInt("ModeForTut"));
            if (PlayerPrefs.GetInt("ModeForTut") == 1)
            {
                asyncOperation = SceneManager.LoadSceneAsync("GamePlay");
            }
            else if (PlayerPrefs.GetInt("ModeForTut") == 2)
            {
                asyncOperation = SceneManager.LoadSceneAsync("SurvivalMode");
            }
        }

        while (!asyncOperation.isDone)
        {

            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            FillBar.fillAmount = progress;
            yield return null;
        }

    }
}
