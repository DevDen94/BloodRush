using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_PanelOff : MonoBehaviour
{
    private void OnEnable()
    {
        //Invoke("Disabling", 2f);
    }

    public void CallingOther()
    {
        Invoke("Disabling", 1f);
    }

    void Disabling()
    {
        gameObject.SetActive(false);
        WaveManager_.instance.Coming.SetActive(true);
        WaveManager_.instance.Coming.GetComponent<CountDown>().Start();
    }

}
