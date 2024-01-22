using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeBoxManager : MonoBehaviour
{
    public static DialougeBoxManager instance;

    private void Start()
    {
        //LoopChecking();
    }

    private void Update()
    {
        Debug.Log("Time : " + Time.time);
    }

    void LoopChecking()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(i);
            if (i == 7)
            {
                break;
            }
        }

        Debug.Log("Start");

        for (int i = 0; i < 10; i++)
        {
            Debug.Log(i);
        }
    }
}
