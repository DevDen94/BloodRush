using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject ObjectDisable;
    private void Start()
    {
        instance = this;
    }
}
