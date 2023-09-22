using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDown : MonoBehaviour
{
    public Text countdownText;

    public void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        int count = 5;

        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1f); // Wait for 1 second
            count--;
        }

        // When the countdown is finished, you can perform any action you want.
        gameObject.SetActive(false);
        WaveManager_.instance.Instaniate_Zombies();
        StopCoroutine(Countdown());
    }
}
