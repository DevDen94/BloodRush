using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class istrigger : MonoBehaviour
{
    public CheckWindow check;
    // Start is called before the first frame update

    private void Start()
    {
        if (PlayerPrefs.GetInt("WaveNo") >= 1)
        {
            check.Windowslider.value = 0;
            check.SliderE();
        }
        }
        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            check.SliderE();
        }
    }
}
