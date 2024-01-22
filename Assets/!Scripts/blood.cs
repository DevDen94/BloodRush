using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blood : MonoBehaviour
{
    public GameObject BloodEffect;
    public static blood Instance;

    private void Awake()
    {
        Instance = this;
    }
}
