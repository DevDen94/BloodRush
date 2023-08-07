using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject ObjectDisable;
    public bool BrokenEnable;
    public GameObject ZombieContainerr;
    public WaypointGroup[] BrokenPaths;
    private void Start()
    {
        instance = this;
        Invoke("After1sec", 3f);
    }
    void After1sec()
    {
        ZombieContainerr.SetActive(true);
    }
}
