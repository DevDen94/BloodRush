using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{

    public GameObject childPrefab; // Reference to the prefab of the child GameObject
    public Transform parentObject; // Reference to the parent GameObject's Transform component
     
    int Value;
    Scene activeScene;
    private void OnEnable()
    {
       
    }
    private void Start()
    {
        
        Value = 0;
        StartCoroutine(waitforSceneSwitch());
        activeScene = SceneManager.GetActiveScene();
        Invoke(nameof(ShowBig),0.5f);
       // SoundsManager.instance.PlayMainMenuMusic();
    }
    public void ShowBig()
    {
     //   AdsManager.instance.ShowBigBanner();
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
        CheckTime();
    }

    public void CheckTime()
    {
        if (Value >= 71)
        {
            StopCoroutine(waitforSceneSwitch());
            if (activeScene.name == "Splash")
                SceneManager.LoadScene("MainMenu");
            else if (activeScene.name == "MainMenu")
            {
                if (PlayerPrefs.GetInt("Mode") == 1)
                {
                    SceneManager.LoadScene("GamePlay");
                }
                else
                {
                    SceneManager.LoadScene("SurvivalMode");
                }
                
            }
        }
        else
        {
            StartCoroutine(waitforSceneSwitch());
        }
    }

}
