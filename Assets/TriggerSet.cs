using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSet : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("WaveNo") >= 1)
        {
            Destroy(gameObject);
        }
        }
        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.BrokenEnable = true;
            gameObject.SetActive(false);
            Debug.LogError("Enterrr");
        }
    }
}
