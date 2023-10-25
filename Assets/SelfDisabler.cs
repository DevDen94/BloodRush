using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisabler : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("Disabling", 2.4f);
    }

    void Disabling()
    {
        gameObject.SetActive(false);
    }
}
