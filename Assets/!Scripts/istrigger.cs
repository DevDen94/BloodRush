using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class istrigger : MonoBehaviour
{
    public CheckWindow check;
    // Start is called before the first frame update

    private void Start()
    {
      
        }
        private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            check.SliderE();
        }
    }
}
