using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject ObjectDisable;
    public bool BrokenEnable;
    public GameObject ZombieContainer;
    public WaypointGroup[] BrokenPaths;
    public WaypointGroup OuterPath;
    public GameObject[] Zombies;
 
    public int[] Instainated_Count;

  
    public GameObject[] SpawnPoints;

    public bool NextWave_Enter;
    public int ZombieDeathCount;
    private void Start()
    {
        instance = this;
        NextWave_Enter = false;
        if (!PlayerPrefs.HasKey("START"))
        {
            PlayerPrefs.SetFloat("START", 1);
            PlayerPrefs.SetInt("WaveNo", 0);

        }
        ZombieDeathCount = Instainated_Count[PlayerPrefs.GetInt("WaveNo")];
        Invoke("Instaniate_Zombies",1f);
    }
    void Instaniate_Zombies()
    {
        if (PlayerPrefs.GetInt("WaveNo") ==0)
        {
            ZombieContainer.SetActive(true);

        }
        else
        {
            for (int i = 0; i <= Instainated_Count[PlayerPrefs.GetInt("WaveNo")]; i++)
            {
                GameObject temp = Instantiate(Zombies[i], SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);
                temp.transform.SetParent(ZombieContainer.transform);
            }
        }
    }
    private void Update()
    {
        if (ZombieDeathCount == 0 && NextWave_Enter)
        {
 
            ZombieDeathCount = Instainated_Count[PlayerPrefs.GetInt("WaveNo")];
            Invoke("Instaniate_Zombies", 15f);
            NextWave_Enter = false;
        }
    }
}
