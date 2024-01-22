using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeOff : MonoBehaviour
{
   public void WaveOn()
    {
        gameObject.SetActive(false);
        WaveManager_.instance.Coming.SetActive(true);
        WaveManager_.instance.Coming.GetComponent<CountDown>().Start();
    }

}
