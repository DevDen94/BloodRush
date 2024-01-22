using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaimButtonAutoHider : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("Disabling", 3f);
    }

    void Disabling()
    {
        gameObject.SetActive(false);
    }
}
