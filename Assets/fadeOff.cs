using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeOff : MonoBehaviour
{
   public void WaveOn()
    {
        gameObject.SetActive(false);
        WaveManager_.instance.Instaniate_Zombies();
    }
}
