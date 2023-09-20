using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WaveManager_ : MonoBehaviour
{
    public static WaveManager_ instance;

    public GameObject ObjectDisable;
    
    public GameObject ZombieContainer;
    public WaypointGroup[] BrokenPaths;
    public WaypointGroup OuterPath;
  
    public WeaponBehavior[] Weapons;
    public WeaponBehavior Gernade;
    public Level_Data[] Levels;
    public GameObject[] SpawnPoints;
    public bool ZombieJump_Bool;
    public int ZombieDeathCount;
    [Header("Zombies")]
    public GameObject[] NormalZombies;
    public GameObject DoctorZombie;
    public GameObject MuscularZombie;
    public GameObject Miltory_Zombie;
    public GameObject PoliceMenZombie;
    public GameObject Fat_Zombie;
    public GameObject FireManZombie;

    public bool AttackModeOn;
    [Header("Zombies_PathArray")]
    public CheckWindow Path5_EndPoint;
    

    [Header(" UI Elements")]
    public GameObject LevelFailed;
    public GameObject LevelPasued;
    [HideInInspector]
    public bool isLevelComplete;
    public GameObject Objective_Panel;
    public GameObject Weapons_Panel;
    public Slider Reloading_Slider;
    public GameObject aimBtn;

    public GameObject[] Weapon_StartImages;
    public GameObject WeaponStartHeader;
    public GameObject[] WeaponWheelImages;

    public AudioSource Bg_Music;
    public AudioSource src;
    public AudioClip Btnclick;
    public AudioClip ReloadingClip;
    public GameObject EmptyPanel;
    public GameObject[] SpawnPoints_P;
    public Transform Player;

    public GameObject AmmoObj;
    public GameObject Gernade_Image;
    private bool is_Gernade;
    private Level_Data Wave_;
    public int Total_Kills;
    public Text Kills;
    public GameObject WaveImage;


    [HideInInspector] public bool Slot1;
    [HideInInspector] public bool Slot2;
    [HideInInspector] public bool Slot3;
    [HideInInspector] public bool Slot4;
    public GameObject Slot1_G;
    public GameObject Slot2_G;
    public GameObject Slot3_G;
    public GameObject Slot4_G;
    private void Start()
    {
        isLevelComplete = false;
        AmmoObj.SetActive(false);
        is_Gernade = false;
        Total_Kills = 0;
        int sp = Random.Range(0, SpawnPoints_P.Length);
        Player.position = SpawnPoints_P[sp].transform.position;
        Player.rotation = SpawnPoints_P[sp].transform.rotation;
        EmptyPanel.SetActive(true);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        instance = this;
        PlayerPrefs.SetInt("Wave_No", 0);
        Invoke("Instaniate_Zombies",1f);
        Invoke("Delay", 1.5f);
        Bg_Music.volume = PlayerPrefs.GetFloat("Music");
        src.volume = PlayerPrefs.GetFloat("Sounds");
     
      //  AdsManager.instance.ShowSmallBanner();
    }
    void TotalCount()
    {
        isLevelComplete = false;
        ZombieDeathCount = Wave_.DoctorZombie_Count +
        Wave_.Fireman_Zombies + Wave_.FatZombie_Count +
        Wave_.Miltory_Count + Wave_.PoliceMen_Zombies + Wave_.Muscular_Count + Wave_.NormalZombies_Count;
        Debug.LogError(ZombieDeathCount);
    }
    void Delay()
    {
        Objective_Panel.SetActive(true);
        Time.timeScale = 0f;
    }
 public void Instaniate_Zombies()
    {

        Wave_ = Levels[PlayerPrefs.GetInt("Wave_No")];
        TotalCount();
        LoadWeapons_Data(PlayerPrefs.GetInt("Wave_No"));
        ZombieContainer.SetActive(true);
        Call_Zombies(Levels[PlayerPrefs.GetInt("Wave_No")]);
        Debug.LogError(Levels[PlayerPrefs.GetInt("Wave_No")]);
     
    }
    public void Off_AvaliableWeaponPanel()
    {
        EmptyPanel.SetActive(false);
        Weapons_Panel.SetActive(false);
        src.PlayOneShot(Btnclick);
        Time.timeScale = 1f;
    }
    public void ContinueGame()
    {
        EmptyPanel.SetActive(false);
        src.PlayOneShot(Btnclick);
        Objective_Panel.SetActive(false);
        Weapons_Panel.SetActive(true);
        
    }

    public void WaveComplete()
    {
        PlayerPrefs.SetInt("Wave_No", PlayerPrefs.GetInt("Wave_No") + 1);
        WaveImage.SetActive(true);
    }
    private void Update()
    {
        if (Gernade.ammo == 0 && is_Gernade==false)
        {
            Gernade_Image.SetActive(false);
            SelectWeapn(2);
            is_Gernade = true;
        }
        Kills.text =  Total_Kills.ToString();
        if (ZombieDeathCount == 0 && isLevelComplete  )
        {
            Invoke("WaveComplete", 1.5f);
            isLevelComplete = false;

        }
        if (AttackModeOn)
        {
            for (int i = 0; i < AllZombies.Length; ++i)
            {
                if (AllZombies[i] == null)
                {
                    print("Zombie dead");
                }
                else
                {
                    AllZombies[i].GetComponent<AI>().AttackMode = true;

                }
            }
            AttackModeOn = false;
        }
    }

    public void Next_Btn()
    {      
       // AdsManager.instance.ShowSmallBanner();
        src.PlayOneShot(Btnclick);
        SceneManager.LoadScene("SurvivalMode");
        //Time.timeScale = 1f;
    }
    public void Restart_Btn()
    {
       // AdsManager.instance.ShowSmallBanner();

        //Time.timeScale = 1f;
        src.PlayOneShot(Btnclick);
        SceneManager.LoadScene("SurvivalMode");
    }
    public void Home()
    {
        //AdsManager.instance.ShowSmallBanner();
        //Time.timeScale = 1f;
        src.PlayOneShot(Btnclick);
        SceneManager.LoadScene("MainMenu");
    }
    public void Pause_BTn()
    {
      //  AdsManager.instance.ShowinterAd();
       // AdsManager.instance.ShowBigBanner();
        src.PlayOneShot(Btnclick);
        LevelPasued.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
       // AdsManager.instance.ShowSmallBanner();

        src.PlayOneShot(Btnclick);
        LevelPasued.SetActive(false);
        Time.timeScale = 1;
    }
    void LoadWeapons_Data(int value)
    {
        Weapons[2].haveWeapon = true;
        for (int i = 0; i < Weapons.Length; ++i)
        {
            if (Weapons[i].haveWeapon == true)
            {
                GameObject a = Instantiate(Weapon_StartImages[i]);
                a.transform.SetParent(WeaponStartHeader.transform);
                a.transform.localScale = new Vector3(1, 1, 1);
                Slot_Weapon_Intialize(WeaponWheelImages[i]);
            }
        }
    }
    void Slot_Weapon_Intialize(GameObject slotImg)
    {
        if (Slot1 && Slot2 && Slot3 && Slot4)
        {
            Destroy(Slot4_G.transform.GetChild(0).gameObject);
        }
        GameObject temp = Instantiate(slotImg);
        if (!Slot1)
        {
            temp.transform.SetParent(Slot1_G.transform);
            temp.transform.localScale = new Vector3(1, 1, 1);
            Slot1 = true;
            return;
        }
        if (!Slot2)
        {
            temp.transform.SetParent(Slot2_G.transform);
            temp.transform.localScale = new Vector3(1, 1, 1);
            Slot2 = true;
            return;
        }
        if (!Slot3)
        {
            temp.transform.SetParent(Slot3_G.transform);
            temp.transform.localScale = new Vector3(1, 1, 1);
            Slot3 = true;
            return;
        }
        if (!Slot4)
        {
            temp.transform.SetParent(Slot4_G.transform);
            temp.transform.localScale = new Vector3(1, 1, 1);
            Slot4 = true;
            return;
        }
     
    }
    public GameObject WeaponWheel;
    public PlayerWeapons p;
    // [HideInInspector]
    public string buttonName = "Fire";
    public WeaponBehavior wp;
    public ControlFreak2.TouchButton myButton;
    public void ThrowGernade()
    {
        wp.WeaponAnimatorComponent.SetTrigger("Pull");
        Invoke("FireGer", 1f);
    }
   public void FireGer()
    {
        wp.Fire();
        p.StartCoroutine(p.SelectWeapon(PlayerPrefs.GetInt("CurrentWeapon")));
    }
    public void SelectWeapn(int i)
    {


        src.PlayOneShot(Btnclick);
        string weapon = "Select Weapon " + i;
        print(weapon);
        p.StartCoroutine(p.SelectWeapon(i));
        if (i == 14)
        {
            PlayerPrefs.SetInt("CurrentWeapon", p.currentWeapon);
            Invoke("ThrowGernade", 0.3f);
            //ThrowGernade();
            return;
        }
        
       
        if(i==3 || i == 4 || i==14)
        {
            aimBtn.SetActive(false);
        }
        else
        {
            aimBtn.SetActive(true);
        }

        if (i == 1)
        {
            AmmoObj.SetActive(false);
        }
        else
        {
            AmmoObj.SetActive(true);
        }
        
       
    }
   
    public void Open_WeaponWheel()
    {
 
        Reloading_Slider.gameObject.SetActive(false);
        WeaponWheel.SetActive(true);
    }
    public void Close_WeaponWheel()
    {
        Time.timeScale = 1;
        WeaponWheel.SetActive(false);
    }
    void Call_Zombies(Level_Data level)
    {
        // Call Normal Zombies
        for (int i = 1; i <= level.NormalZombies_Count; i++)
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
            for (int i = 1; i <= level.DoctorZombie_Count; i++)
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
            for (int i = 1; i <= level.Miltory_Count; i++)
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
            for (int i = 1; i <= level.Muscular_Count; i++)
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
            for (int i = 1; i <= level.PoliceMen_Zombies; i++)
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
            for (int i = 1; i <= level.FatZombie_Count; i++)
            {
                GameObject temp = Instantiate(Fat_Zombie, SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);
                temp.transform.SetParent(ZombieContainer.transform);
                temp.GetComponent<AI>().waypointGroup = OuterPath;
                instantiatedObjectsList.Add(temp);
                System.Array.Resize(ref AllZombies, AllZombies.Length + 1);
                AllZombies[AllZombies.Length - 1] = temp;

            }
        }

        // Call Fireman Zombies
        if (level.Fireman_Zombies != 0)
        {
            for (int i = 1; i <= level.Fireman_Zombies; i++)
            {
                GameObject temp = Instantiate(FireManZombie, SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);
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
    
}
