using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeBoxManager : MonoBehaviour
{
    public static DialougeBoxManager instance;

    private void Start()
    {
        LoopChecking();
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

        return;

        Debug.Log("Start");

        for (int i = 0; i < 10; i++)
        {
            Debug.Log(i);
        }
    }
}
