using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text timerText;
  

    private float timer = 0.0f;
    private bool isRunning = false;
    private float BestTime_Saved = 0.0f;
    public Text Best_Time;
    public Text ActualTime;
    public Text bestkills;
    public Text Actualkills;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BestTime"))
        {
            PlayerPrefs.SetFloat("BestTime", 100f);
            PlayerPrefs.SetInt("BestKills", 0);

        }
        BestTime_Saved = PlayerPrefs.GetFloat("BestTime");
    }

    private void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timeString;
    }

    public void StartTimer()
    {
        isRunning = true;
        timer = 0.0f;
    }

    public void StopTimer()
    {
        isRunning = false;
        CompleteLevel();
    }

    private void CompleteLevel()
    {
        if (timer < BestTime_Saved)
        {
            BestTime_Saved = timer;
            PlayerPrefs.SetFloat("BestTime", BestTime_Saved);
            Debug.LogError("A");
        }
        else
        {
            BestTime_Saved = PlayerPrefs.GetFloat("BestTime");
            Debug.LogError("AB");
        }
        
      
        ActualTime.text = FormatTime(timer);
        Best_Time.text = FormatTime(BestTime_Saved);
        bestkills.text = PlayerPrefs.GetInt("BestKills").ToString();

        Debug.Log("Best Time: " + FormatTime(BestTime_Saved));
        Debug.Log("Actual Time: " + FormatTime(timer));
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
