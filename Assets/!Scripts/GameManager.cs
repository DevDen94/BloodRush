using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject ObjectDisable;
    public bool BrokenEnable;
    public GameObject ZombieContainer;
    public WaypointGroup[] BrokenPaths;
    public WaypointGroup OuterPath;
    public int[] Instainated_Count;
    public WeaponBehavior[] Weapons;
    public Level_Data[] Levels;
    public GameObject[] SpawnPoints;
    public bool ZombieJump_Bool;
    public bool NextWave_Enter;
    public int ZombieDeathCount;
    public Text Wave;
    [Header("Zombies")]
    public GameObject[] NormalZombies;
    public GameObject DoctorZombie;
    public GameObject MuscularZombie;
    public GameObject Miltory_Zombie;
    public GameObject PoliceMenZombie;
    public GameObject Fat_Zombie;
    public GameObject FireManZombie;

    [Header("Zombies_PathArray")]
    public CheckWindow Path1_EndPoint;
    public CheckWindow Path2_EndPoint;
    public CheckWindow Path3_EndPoint;
    public CheckWindow Path4_EndPoint;
    public CheckWindow Path5_EndPoint;

    [Header(" UI Elements")]
    public GameObject LevelComplete;
    public GameObject LevelFailed;
    public GameObject LevelPasued;
    bool isLevelComplete;
    private void Start()
    {
     
        // SoundsManager.instance.PlayGameplayMusic();
        isLevelComplete = false;
        ZombieJump_Bool = false;
        instance = this;
        NextWave_Enter = false;
        if (!PlayerPrefs.HasKey("START"))
        {
            PlayerPrefs.SetFloat("START", 1);
            PlayerPrefs.SetInt("WaveNo", 0);
        }
        ZombieDeathCount = Instainated_Count[PlayerPrefs.GetInt("WaveNo")];
        LoadWeapons_Data(PlayerPrefs.GetInt("WaveNo"));
        Invoke("Instaniate_Zombies",1f);
    }
    void Instaniate_Zombies()
    {
            ZombieContainer.SetActive(true);
            Call_Zombies(Levels[PlayerPrefs.GetInt("WaveNo")]);
             Assign_Paths();
    }
    
    private void Update()
    {
        Wave.text = "Zombies Left :" + ZombieDeathCount;
        if (ZombieDeathCount == 0 && NextWave_Enter && !isLevelComplete  )
        {
            PlayerPrefs.SetInt("WaveUnlock", PlayerPrefs.GetInt("WaveUnlock")+1);
            PlayerPrefs.SetInt("WaveNo", PlayerPrefs.GetInt("WaveNo") + 1);
            LevelComplete.SetActive(true);
            isLevelComplete = true;
        }
    }

    public void Next_Btn()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void Restart_Btn()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Pause_BTn()
    {
        LevelPasued.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        LevelPasued.SetActive(false);
        Time.timeScale = 1;
    }
    void LoadWeapons_Data(int value)
    {
        if (value == 1)
        {
            Weapons[2].haveWeapon = true;
        }
        if (value == 2)
        {
            Weapons[2].haveWeapon = true;
            Weapons[3].haveWeapon = true;
        }
        if (value == 3)
        {
            Weapons[4].haveWeapon = true;
            Weapons[5].haveWeapon = true;
        }
        if (value == 4)
        {
            Weapons[6].haveWeapon = true;
            Weapons[7].haveWeapon = true;
        }
    }

    void Call_Zombies(Level_Data level)
    {
        // Call Normal Zombies
        for (int i = 0; i <= level.NormalZombies_Count; i++)
        {
            int rand = Random.Range(0, NormalZombies.Length);
            GameObject T = NormalZombies[rand];
            GameObject temp = Instantiate(T, SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);
            temp.GetComponent<AI>().waypointGroup = OuterPath;
            temp.transform.SetParent(ZombieContainer.transform);
            instantiatedObjectsList.Add(temp);
            System.Array.Resize(ref AllZombies, AllZombies.Length + 1);
            AllZombies[AllZombies.Length - 1] = temp;


        }
        // Call Doctor Zombies
        if (level.DoctorZombie_Count != 0)
        {
            for (int i = 0; i <= level.DoctorZombie_Count; i++)
            {
                GameObject temp = Instantiate(DoctorZombie, SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);
                temp.transform.SetParent(ZombieContainer.transform);
                temp.GetComponent<AI>().waypointGroup = OuterPath;
                instantiatedObjectsList.Add(temp);
                System.Array.Resize(ref AllZombies, AllZombies.Length + 1);
                AllZombies[AllZombies.Length - 1] = temp;

            }
        }
        // Call Miltary Zombies
        if (level.Miltory_Count != 0)
        {
            for (int i = 0; i <= level.Miltory_Count; i++)
            {
                GameObject temp = Instantiate(Miltory_Zombie, SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);
                temp.transform.SetParent(ZombieContainer.transform);
                temp.GetComponent<AI>().waypointGroup = OuterPath;
                instantiatedObjectsList.Add(temp);
                System.Array.Resize(ref AllZombies, AllZombies.Length + 1);
                AllZombies[AllZombies.Length - 1] = temp;

            }
        }
        // Call Muscular Zombies
        if (level.Muscular_Count != 0)
        {
            for (int i = 0; i <= level.Muscular_Count; i++)
            {
                GameObject temp = Instantiate(MuscularZombie, SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);
                temp.transform.SetParent(ZombieContainer.transform);
                temp.GetComponent<AI>().waypointGroup = OuterPath;
                instantiatedObjectsList.Add(temp);
                System.Array.Resize(ref AllZombies, AllZombies.Length + 1);
                AllZombies[AllZombies.Length - 1] = temp;

            }
        }
        // Call PoliceMen Zombies
        if (level.PoliceMen_Zombies != 0)
        {
            for (int i = 0; i <= level.PoliceMen_Zombies; i++)
            {
                GameObject temp = Instantiate(PoliceMenZombie, SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);
                temp.transform.SetParent(ZombieContainer.transform);
                temp.GetComponent<AI>().waypointGroup = OuterPath;
                instantiatedObjectsList.Add(temp);
                System.Array.Resize(ref AllZombies, AllZombies.Length + 1);
                AllZombies[AllZombies.Length - 1] = temp;

            }
        }
        // Call FAT Zombies
        if (level.FatZombie_Count != 0)
        {
            for (int i = 0; i <= level.FatZombie_Count; i++)
            {
                GameObject temp = Instantiate(Fat_Zombie, SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);
                temp.transform.SetParent(ZombieContainer.transform);
                temp.GetComponent<AI>().waypointGroup = OuterPath;
                instantiatedObjectsList.Add(temp);
                System.Array.Resize(ref AllZombies, AllZombies.Length + 1);
                AllZombies[AllZombies.Length - 1] = temp;

            }
        }

    }

    public List<GameObject> instantiatedObjectsList = new List<GameObject>();
    public  GameObject[] AllZombies;
    public void Assign_Paths()
    {
        int objectsPerArray = AllZombies.Length / 5;
        int remainder = AllZombies.Length % 5;
        int currentIndex = 0;

        for (int i = 0; i <5; i++)
        {
            int count = objectsPerArray + (i < remainder ? 1 : 0);

            if (i == 0)
                Path1_EndPoint.Zombie = new GameObject[count];
            else if (i == 1)
                Path2_EndPoint.Zombie = new GameObject[count];
            else if(i == 2)
               Path3_EndPoint.Zombie = new GameObject[count];
            else if (i == 3)
                Path4_EndPoint.Zombie = new GameObject[count];
            else
                Path5_EndPoint.Zombie = new GameObject[count];

            for (int j = 0; j < count; j++)
            {
                if (i == 0)
                {
                    Path1_EndPoint.Zombie[j] = AllZombies[currentIndex];
                    Path1_EndPoint.Zombie[j].GetComponent<AI>().PathIndex = 0;
                }
                else if (i == 1)
                {
                    Path2_EndPoint.Zombie[j] = AllZombies[currentIndex];
                    Path2_EndPoint.Zombie[j].GetComponent<AI>().PathIndex = 1;
                }
                else if (i == 2)
                {
                    Path3_EndPoint.Zombie[j] = AllZombies[currentIndex];
                    Path3_EndPoint.Zombie[j].GetComponent<AI>().PathIndex = 2;
                }
                else if (i == 3)
                {
                    Path4_EndPoint.Zombie[j] = AllZombies[currentIndex];
                    Path4_EndPoint.Zombie[j].GetComponent<AI>().PathIndex = 3;
                }
                else
                {
                    Path5_EndPoint.Zombie[j] = AllZombies[currentIndex];
                    Path5_EndPoint.Zombie[j].GetComponent<AI>().PathIndex = 4;
                }
                currentIndex++;
            }
        }
    }
}
