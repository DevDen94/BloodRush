using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WaveManager_ : MonoBehaviour
{
    public static WaveManager_ instance;
    public Timer timer_Script;
    public GameObject ObjectDisable;
    
    public GameObject ZombieContainer;
  
  
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
    public Slider Reloading_Slider;
    public GameObject aimBtn;

    public GameObject[] Weapon_StartImages;
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
    public bool is_Gernade;
    private Level_Data Wave_;
    public int Total_Kills;
    public Text Kills;
    public GameObject WaveImage;


     public bool Slot1;
     public bool Slot2;
     public bool Slot3;
     public bool Slot4;
    public GameObject SlotParent;
    public GameObject Pick_WeaponBTN;
    public GameObject Slot1_G;
    public GameObject Slot2_G;
    public GameObject Slot3_G;
    public GameObject Slot4_G;
    public GameObject Coming;
    private void Start()
    {
        LoadWeapons_Data();
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
        GoogleAdMobController.instance.ShowSmallBannerAd();
       
    }
    
    void TotalCount()
    {
        isLevelComplete = false;
        ZombieDeathCount = Wave_.DoctorZombie_Count +
        Wave_.Fireman_Zombies + Wave_.FatZombie_Count +
        Wave_.Miltory_Count + Wave_.PoliceMen_Zombies + Wave_.Muscular_Count + Wave_.NormalZombies_Count;
      
    }
    void Delay()
    {
        Objective_Panel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Stoppp()
    {
        timer_Script.StopTimer();
    }
     public void Instaniate_Zombies()
      {

        Wave_ = Levels[PlayerPrefs.GetInt("Wave_No")];
        Firebase.Analytics.FirebaseAnalytics.LogEvent("SurvivalMode_StartFunction", "SurvivalMode", Wave_.ToString());
        TotalCount();
        ZombieContainer.SetActive(true);
        Call_Zombies(Levels[PlayerPrefs.GetInt("Wave_No")]);
      }
 
    public void ContinueGame()
    {
        EmptyPanel.SetActive(false);
        src.PlayOneShot(Btnclick);
        Objective_Panel.SetActive(false);
        timer_Script.StartTimer();
        Time.timeScale = 1f;
        
    }

    public void WaveComplete()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("SurvivalMode_WaveComplete", "SurvivalMode", Wave_.ToString());
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

       // if (p.totalWeapons > 2) { DropWeapon_BTN.SetActive(true); } else { DropWeapon_BTN.SetActive(false); };
    }

    public void Next_Btn()
    {
        GoogleAdMobController.instance.ShowSmallBannerAd();
        Firebase.Analytics.FirebaseAnalytics.LogEvent("SurvivalMode_NextBtn", "SurvivalMode", Wave_.ToString());
        src.PlayOneShot(Btnclick);
        SceneManager.LoadScene("SurvivalMode");
        //Time.timeScale = 1f;
    }
    public void Restart_Btn()
    {
        // AdsManager.instance.ShowSmallBanner();
        GoogleAdMobController.instance.ShowSmallBannerAd();
        Firebase.Analytics.FirebaseAnalytics.LogEvent("SurvivalMode_RestartBtn", "SurvivalMode", Wave_.ToString());
        src.PlayOneShot(Btnclick);
        SceneManager.LoadScene("SurvivalMode");
    }
    public void Home()
    {
        GoogleAdMobController.instance.ShowSmallBannerAd();
        Firebase.Analytics.FirebaseAnalytics.LogEvent("SurvivalMode_HomeBtn", "SurvivalMode", Wave_.ToString());
        //Time.timeScale = 1f;
        src.PlayOneShot(Btnclick);
        SceneManager.LoadScene("MainMenu");
    }
    public void Pause_BTn()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent("SurvivalMode_PasueBtn", "SurvivalMode", Wave_.ToString());
        src.PlayOneShot(Btnclick);
        LevelPasued.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        // AdsManager.instance.ShowSmallBanner();
        GoogleAdMobController.instance.ShowSmallBannerAd();
        src.PlayOneShot(Btnclick);
        LevelPasued.SetActive(false);
        Time.timeScale = 1;
    }
    public void Slot_bool_Check()
    {
        int i = Slot1_G.transform.childCount;
        if (i == 2) Slot1 = true;

        int j = Slot2_G.transform.childCount;
        if (j == 2) Slot2 = true;

        int k = Slot3_G.transform. childCount;
        if (k == 2) Slot3 = true;

        int l = Slot4_G.transform.childCount;
        if (l == 2) Slot4 = true;
    }
 public  void LoadWeapons_Data()
    {
        Slot_bool_Check();
        for (int i = 0; i < Weapons.Length; ++i)
        {
            if (Weapons[i].haveWeapon == true)
            {
                Slot_Weapon_Intialize(WeaponWheelImages[i]);
            }
        }
    
    }
   public void Slot_Weapon_Intialize(GameObject slotImg) // Spawn the selected weaponwheel image into the empty slot 
    {
        Slot_bool_Check();
        if (!Slot1)
        {
            slotImg.SetActive(true);
            slotImg.transform.SetParent(Slot1_G.transform);
            slotImg.transform.position = Slot1_G.transform.GetChild(0).gameObject.transform.position;
            slotImg.transform.rotation= Slot1_G.transform.GetChild(0).gameObject.transform.rotation;
            slotImg.transform.localScale = new Vector3(1, 1, 1);
            Slot1 = true;
            
            return;
        }
        if (!Slot2)
        {
            slotImg.SetActive(true);
            slotImg.transform.SetParent(Slot2_G.transform);
            slotImg.transform.position = Slot2_G.transform.GetChild(0).gameObject.transform.position;
            slotImg.transform.rotation = Slot2_G.transform.GetChild(0).gameObject.transform.rotation;
            slotImg.transform.localScale = new Vector3(1, 1, 1);
            Slot2 = true;
            return;
        }
        if (!Slot3)
        {
            slotImg.SetActive(true);
            slotImg.transform.SetParent(Slot3_G.transform);
            slotImg.transform.position = Slot3_G.transform.GetChild(0).gameObject.transform.position;
            slotImg.transform.rotation = Slot3_G.transform.GetChild(0).gameObject.transform.rotation;
            slotImg.transform.localScale = new Vector3(1, 1, 1);
            Slot3 = true;
         
            return;
        }
        if (!Slot4)
        {
            slotImg.SetActive(true);
            slotImg.transform.SetParent(Slot4_G.transform);
            slotImg.transform.position = Slot4_G.transform.GetChild(0).gameObject.transform.position;
            slotImg.transform.rotation = Slot4_G.transform.GetChild(0).gameObject.transform.rotation;
            slotImg.transform.localScale = new Vector3(1, 1, 1);
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
    public void SlotImageParent(GameObject g)
    {
        Slot1_G.GetComponent<Image>().enabled = false;
        Slot2_G.GetComponent<Image>().enabled = false;
        Slot3_G.GetComponent<Image>().enabled = false;
        Slot4_G.GetComponent<Image>().enabled = false;

        g.transform.parent.GetComponent<Image>().enabled = true;
    } // Selected slot bg show
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


        if (i==3 || i == 4 || i==14)
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
        
       
    } // Player will grap the selected weapon with specific index in weapon behaviour script
   
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
    void Call_Zombies(Level_Data level)  // Instaniate Zombies
    {
        // Call Normal Zombies
        for (int i = 1; i <= level.NormalZombies_Count; i++)
        {
            int rand = Random.Range(0, NormalZombies.Length);
            GameObject T = NormalZombies[rand];
            GameObject temp = Instantiate(T, SpawnPoints[i].transform.position, SpawnPoints[i].transform.rotation);
         
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
       ;
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

                instantiatedObjectsList.Add(temp);
                System.Array.Resize(ref AllZombies, AllZombies.Length + 1);
                AllZombies[AllZombies.Length - 1] = temp;

            }
        }

    } 

    public List<GameObject> instantiatedObjectsList = new List<GameObject>();
    public  GameObject[] AllZombies;
    
    public void Gernade_Cycle()
    {
        Gernade.cycleSelect = false;
        Invoke("sec", 1f);
    }
    void sec()
    {
        Gernade.cycleSelect = true;
    }
}
