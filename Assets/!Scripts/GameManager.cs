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
   
    public WeaponBehavior[] Weapons;
    public WeaponBehavior Gernade;
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

    public bool AttackModeOn;
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
    public Text Wave_NO;
    private Level_Data Wave_;

    public GameObject TorchLight;
    public GameObject TorchOn;
    public GameObject TorchOff;
    public GameObject _skipLevelButton;

    public FPSRigidBodyWalker _FPSRigidBodyWalker;

    //public LaserBeamManager[] LaserBeams;
    //public GameObject LaserOn;
    //public GameObject LaserOff;

    //bool previouslyActived;

    public Button grenadeButton;

    void TotalCount()
    {
        ZombieDeathCount = Wave_.DoctorZombie_Count +
        Wave_.Fireman_Zombies + Wave_.FatZombie_Count +
        Wave_.Miltory_Count + Wave_.PoliceMen_Zombies + Wave_.Muscular_Count + Wave_.NormalZombies_Count;

    }
    private void Start()
    {
     
        is_Gernade = false;
     
        int sp = Random.Range(0, SpawnPoints_P.Length);
        Player.position = SpawnPoints_P[sp].transform.position;
        Player.rotation = SpawnPoints_P[sp].transform.rotation;
        EmptyPanel.SetActive(true);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        isLevelComplete = false;
        ZombieJump_Bool = false;
        instance = this;
        NextWave_Enter = false;
        if (!PlayerPrefs.HasKey("START"))
        {
            PlayerPrefs.SetFloat("START", 1);
            PlayerPrefs.SetInt("WaveNo", 0);
        }

        Debug.Log("wave pref in gameply : " + PlayerPrefs.GetInt("WaveNo"));

        if(PlayerPrefs.GetInt("WaveNo") != 0)
        {
            CutSceneDelay();
        }

        Bg_Music.volume = PlayerPrefs.GetFloat("Music");
        src.volume = PlayerPrefs.GetFloat("Sounds");
        int wav = PlayerPrefs.GetInt("WaveNo") + 1;
        Wave_NO.text = " WAVE NO : " +wav ;

        //foreach(var beam in LaserBeams)
        //{
        //    beam.gameObject.SetActive(false);
        //}

        GoogleAdMobController.instance.ShowSmallBannerAd();
    }
    void Delay()
    {
        if (PlayerPrefs.GetInt("WaveNo") == 0)
        {
            Objective_Panel.SetActive(true);
        }
        else
        {
            Objective_Panel.SetActive(false);
            Weapons_Panel.SetActive(true);

        }
        
        BrokenEnable = true;
        Time.timeScale = 0f;
    }

    public void CutSceneDelay()
    {
        Debug.Log("Zombiess Coming");
        Wave_ = Levels[PlayerPrefs.GetInt("WaveNo")];
        TotalCount();
        LoadWeapons_Data(PlayerPrefs.GetInt("WaveNo"));
        Invoke("Instaniate_Zombies", 1f);
        Invoke("Delay", 1.5f);
        //Firebase.Analytics.FirebaseAnalytics.LogEvent("WaveMode_GamePlay_Start", "StartFunction", Wave_.ToString());
    }

    void Instaniate_Zombies()
    {
            ZombieContainer.SetActive(true);
            Call_Zombies(Levels[PlayerPrefs.GetInt("WaveNo")]);
         //  Debug.LogError(Levels[PlayerPrefs.GetInt("WaveNo")]);
            Assign_Paths();
        
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
 

    public void LevelCompletee()
    {
        //Firebase.Analytics.FirebaseAnalytics.LogEvent("WaveMode_LevelComplete", "WaveComplete", Wave_.ToString());
        GoogleAdMobController.instance.ShowBigBannerAd();

        if(PlayerPrefs.GetInt("CompAd") % 2 == 0)
        {
            GoogleAdMobController.instance.ShowInterstitialAd();
        }

        PlayerPrefs.SetInt("CompAd", PlayerPrefs.GetInt("CompAd") + 1);
        LevelComplete.SetActive(true);
        if (PlayerPrefs.GetInt("WaveNo") >= 19)
        {
            SceneManager.LoadScene("MainMenu");
            return;
        }
        else
        {
            PlayerPrefs.SetInt("WaveUnlock", PlayerPrefs.GetInt("WaveUnlock") + 1);
            PlayerPrefs.SetInt("WaveNo", PlayerPrefs.GetInt("WaveNo") + 1);
          //  AdsManager.instance.ShowinterAd();
          //  AdsManager.instance.ShowBigBanner();
        }



    }
    private void Update()
    {
        if (Gernade.ammo == 0 && is_Gernade==false)
        {
            Gernade_Image.SetActive(false);
            SelectWeapn(2);
            is_Gernade = true;
        }
        Wave.text =  ZombieDeathCount.ToString();
        if (ZombieDeathCount == 0 && NextWave_Enter && !isLevelComplete  )
        {
            Invoke("LevelCompletee", 1.5f);
            isLevelComplete = true;

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

        //if(ControlFreak2.CF2Input.GetKey(KeyCode.LeftShift))
        //{
        //    Debug.Log("LaserCancel");

        //    foreach (var beam in LaserBeams)
        //    {
        //        beam.gameObject.SetActive(false);
        //    }

        //    LaserOn.SetActive(false);
        //    LaserOff.SetActive(true);
        //}
    }

    public void Next_Btn()
    {

        // AdsManager.instance.ShowSmallBanner();
        //Firebase.Analytics.FirebaseAnalytics.LogEvent("WaveMode_NextBtn_Click", "WaveMode", Wave_.ToString());
        src.PlayOneShot(Btnclick);
        SceneManager.LoadScene("GamePlay");
        //Time.timeScale = 1f;
    }
    public void Restart_Btn()
    {
        // AdsManager.instance.ShowSmallBanner();
        //Firebase.Analytics.FirebaseAnalytics.LogEvent("WaveMode_RestartBtn_Click", "WaveMode", Wave_.ToString());
        //Time.timeScale = 1f;
        src.PlayOneShot(Btnclick);
        SceneManager.LoadScene("GamePlay");
    }
    public void Home()
    {
        //AdsManager.instance.ShowSmallBanner();
        //Firebase.Analytics.FirebaseAnalytics.LogEvent("WaveMode_HomeBtn_Click", "WaveMode", Wave_.ToString());
        //Time.timeScale = 1f;
        src.PlayOneShot(Btnclick);
        SceneManager.LoadScene("MainMenu");
    }
    public void Pause_BTn()
    {
        //GoogleAdMobController.instance.ShowInterstitialAd();
        GoogleAdMobController.instance.ShowBigBannerAd();
        //Firebase.Analytics.FirebaseAnalytics.LogEvent("WaveMode_PasueBtn_Click", "WaveMode", Wave_.ToString());
        //  AdsManager.instance.ShowinterAd();
        // AdsManager.instance.ShowBigBanner();
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
    void LoadWeapons_Data(int value)
    {
        if (value == 1 || value==10)
        {
            Weapons[2].haveWeapon = true;
            
        }
        if (value == 2 || value == 11)
        {
            Debug.Log("Level 2");
            Weapons[2].haveWeapon = true; //2
            Weapons[9].haveWeapon = true; //3
        }
        if (value == 3 || value == 12)
        {
            Weapons[4].haveWeapon = true; //4
            Weapons[5].haveWeapon = true; //5
        }
        if (value == 4 || value == 13)
        {
            Weapons[6].haveWeapon = true;
            Weapons[7].haveWeapon = true;
        }
        if (value == 5 || value == 14)
        {
            Weapons[2].haveWeapon = true;
            Weapons[7].haveWeapon = true;
        }
        if (value == 6 || value == 15)
        {
            Weapons[4].haveWeapon = true;
            Weapons[8].haveWeapon = true;
        }
        if (value == 7 || value == 16)
        {
            Weapons[2].haveWeapon = true;
            Weapons[5].haveWeapon = true;
        }
        if (value == 8 || value == 17)
        {
            Weapons[2].haveWeapon = true;
            Weapons[8].haveWeapon = true;
        }
        if (value == 9 || value == 18)
        {
            Weapons[4].haveWeapon = true;
            Weapons[8].haveWeapon = true;
        }
        if(value == 19)
        {
            Weapons[2].haveWeapon = true;
            Weapons[4].haveWeapon = true;
        }
        for (int i = 0; i < Weapons.Length; ++i)
        {
            if (Weapons[i].haveWeapon == true)
            {
                GameObject a = Instantiate(Weapon_StartImages[i]);
                a.transform.SetParent(WeaponStartHeader.transform);
                a.transform.localScale = new Vector3(1, 1, 1);
                WeaponWheelImages[i].SetActive(true);
            }
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

        Invoke(nameof(GrenadeAgain), 2f);
        p.StartCoroutine(p.SelectWeapon(PlayerPrefs.GetInt("CurrentWeapon")));
    }

    void GrenadeAgain()
    {
        grenadeButton.interactable = true;
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
            grenadeButton.interactable = false;
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

    public void TorchActivity()
    {
        TorchLight.SetActive(!TorchLight.activeInHierarchy);

        TorchOn.SetActive(TorchLight.activeInHierarchy);
        TorchOff.SetActive(!TorchLight.activeInHierarchy);
    }

    public void SkipLevelRewardAd()
    {
        _skipLevelButton.SetActive(false);
        if(Application.internetReachability != NetworkReachability.NotReachable)
        {
            PlayerPrefs.SetInt("SkipLevel", 1);
        }
        GoogleAdMobController.instance.ShowRewardedAd();
    }

    public void SkippingLevel()
    {
        Debug.Log("Skipping Level");

        PlayerPrefs.SetInt("WaveUnlock", PlayerPrefs.GetInt("WaveUnlock") + 1);
        PlayerPrefs.SetInt("WaveNo", PlayerPrefs.GetInt("WaveNo") + 1);

        SceneManager.LoadScene("GamePlay");
    }

    //public void LaserBeamActivity()
    //{
    //    foreach (var beam in LaserBeams)
    //    {
    //        beam.gameObject.SetActive(!beam.gameObject.activeInHierarchy);
    //    }

    //    LaserOn.SetActive(!LaserOn.activeInHierarchy);
    //    LaserOff.SetActive(!LaserOn.activeInHierarchy);
    //}

    //public void TorchControlWithZoom()
    //{
    //    if (TorchLight.activeInHierarchy)
    //    {
    //        previouslyActived = TorchLight.activeInHierarchy;
    //        TorchLight.SetActive(false);
    //    }
    //    if (previouslyActived)
    //    {
    //        TorchLight.SetActive(true);
    //    }
    //}
}
