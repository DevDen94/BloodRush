using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolKit_Optimizer : MonoBehaviour
{
   
    [Header("Wait Time To Setting Up Things For Optimization")]
    public float waitTime;
    void Awake() {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        StartCoroutine(wait());
    }
    IEnumerator wait() {
        yield return new WaitForSeconds(waitTime);
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        if (SystemInfo.systemMemorySize > 3500)
        {
            PlayerPrefs.SetInt("Quality", 3);
            QualitySettings.SetQualityLevel(3, true);

        }
        else if (SystemInfo.systemMemorySize > 2500 && SystemInfo.systemMemorySize <= 3500)
        {
            PlayerPrefs.SetInt("Quality", 2);
            QualitySettings.SetQualityLevel(2, true);

        }
        else if (SystemInfo.systemMemorySize <= 2500 && SystemInfo.systemMemorySize > 1500)
        {
            PlayerPrefs.SetInt("Quality", 1);
            QualitySettings.SetQualityLevel(1, true);

        }
        else if (SystemInfo.systemMemorySize < 1500)
        {
            PlayerPrefs.SetInt("Quality", 0);
            QualitySettings.SetQualityLevel(0, true);

        }
    }
}
