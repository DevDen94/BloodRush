using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class GoogleMobileAdsController : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    
    private BannerView SmallbannerAd;
    private BannerView BigbannerAd;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;


    private BannerView MediumECPMSmallbannerAd;
    private BannerView MediumECPMBigbannerAd;
    private InterstitialAd MediumECPMInterstitialAd;
    private RewardedAd MediumECPMrewardedAd;

    private BannerView ALLPRICEECPMSmallbannerAd;
    private BannerView ALLPRICEECPMBigbannerAd;
    private InterstitialAd ALLPRICEECPMInterstitialAd;
    private RewardedAd ALLPRICEECPMrewardedAd;



    [Header("HIGH ECPM")]
    // Replace with your actual ad unit IDs
    public string bannerAdUnitId;
    public string interstitialAdUnitId;
    public string rewardedAdUnitId;
    public string BIGbannerAdUnitId;
    [Header("MEDIUM ECPM")]
    // Replace with your actual ad unit IDs
    public string MediumBannerAdUnitId;
    public string MediumInterstitialAdUnitId;
    public string MediumRewardedAdUnitId;
    public string BIGbannerMediumAdUnitId;
    [Header("ALL PRICE ECPM")]
    // Replace with your actual ad unit IDs
    public string ALLPriceBannerAdUnitId;
    public string ALLPriceInterstitialAdUnitId;
    public string ALLPriceRewardedAdUnitId;
    public string BIGbannerAllAdUnitId;


    [Space]

    public AdPosition SmallBannerPosition;
    public AdPosition BigBannerPosition;

    [Space]

    public bool ShowTestAds;
    [Space]
    [Header("Unity Ads")]

    public string UnityAdsID;
    public string BANNER_PLACEMENT;
    public string InterstatialPlacement;
    public string REWARDED_VIDEO_PLACEMENT;


    public static GoogleMobileAdsController Instance;
    public int number = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            // This is the first instance, so make it the singleton
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // An instance already exists, so destroy this one
            Destroy(gameObject);
        }


        if (ShowTestAds)
        {
            bannerAdUnitId = "ca-app-pub-3940256099942544/6300978111";
            interstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712";
            rewardedAdUnitId = "ca-app-pub-3940256099942544/5224354917";
            BIGbannerAdUnitId = "ca-app-pub-3940256099942544/6300978111";


            MediumBannerAdUnitId = "ca-app-pub-3940256099942544/6300978111";
            MediumInterstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712";
            MediumRewardedAdUnitId = "ca-app-pub-3940256099942544/5224354917";
            BIGbannerMediumAdUnitId = "ca-app-pub-3940256099942544/6300978111";


            ALLPriceBannerAdUnitId = "ca-app-pub-3940256099942544/6300978111";
            ALLPriceInterstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712";
            ALLPriceRewardedAdUnitId = "ca-app-pub-3940256099942544/5224354917";
            BIGbannerAllAdUnitId = "ca-app-pub-3940256099942544/6300978111";
        }
    #if UNITY_ANDROID

        BANNER_PLACEMENT = "Banner_Android";
        InterstatialPlacement = "Interstitial_Android";
        REWARDED_VIDEO_PLACEMENT = "Rewarded_Android";

    #elif UNITY_IOS
        BANNER_PLACEMENT = "Banner_iOS";
        InterstatialPlacemen = "Interstitial_iOS";
        REWARDED_VIDEO_PLACEMENT = "Rewarded_iOS";
    #endif



    }
    private void Start()
    {
        number = 0;
        // Initialize AdMob SDK
        MobileAds.Initialize(initStatus => { });

        // Load HIGH ECPM ADS
        LoadSmallBannerAd();
        LoadBigBannerAd();
        LoadInterstitialAd();
        LoadRewardedAd();

        // Load MEDIUM ECPM ADS
        MEDIUMLoadBigBannerAd();
        MEDIUMLoadSmallBannerAd();
        MEDIUMLoadInterstitialAd();
        MEDIUMLoadRewardedAd();


        ALLPRICESLoadSmallBannerAd();
        ALLPRICESLoadBigBannerAd();
        ALLPRICESLoadInterstitialAd();
        ALLPRICESLoadRewardedAd();
        
        SmallbannerAd.Hide();
        BigbannerAd.Hide();
        MediumECPMSmallbannerAd.Hide();
        ALLPRICEECPMSmallbannerAd.Hide();
        MediumECPMBigbannerAd.Hide();
        ALLPRICEECPMBigbannerAd.Hide();





        
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        MediumECPMrewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        ALLPRICEECPMrewardedAd.OnUserEarnedReward += HandleUserEarnedReward;



        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(UnityAdsID, ShowTestAds, this);
            
            Advertisement.Load(REWARDED_VIDEO_PLACEMENT, this);
        }
        Advertisement.Load(InterstatialPlacement, this);
    }
  

    #region HIGH ECPM LOAD ADS PORTION
    private void LoadSmallBannerAd()
    {
        SmallbannerAd = new BannerView(bannerAdUnitId, AdSize.Banner, SmallBannerPosition);
        AdRequest adRequest = new AdRequest.Builder().Build();
        SmallbannerAd.LoadAd(adRequest);
    }
    private void LoadBigBannerAd()
    {
        BigbannerAd = new BannerView(BIGbannerAdUnitId, AdSize.MediumRectangle, BigBannerPosition);
        AdRequest adRequest = new AdRequest.Builder().Build();
        BigbannerAd.LoadAd(adRequest);
    }
    private void LoadInterstitialAd()
    {
        interstitialAd = new InterstitialAd(interstitialAdUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }

    private void LoadRewardedAd()
    {
        rewardedAd = new RewardedAd(rewardedAdUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(adRequest);
    }
    #endregion

    #region MEDIUM ECPM LOAD ADS PORTION
    private void MEDIUMLoadSmallBannerAd()
    {
        MediumECPMSmallbannerAd = new BannerView(MediumBannerAdUnitId, AdSize.Banner, SmallBannerPosition);
        AdRequest adRequest = new AdRequest.Builder().Build();
        MediumECPMSmallbannerAd.LoadAd(adRequest);
    }
    private void MEDIUMLoadBigBannerAd()
    {
        MediumECPMBigbannerAd = new BannerView(BIGbannerMediumAdUnitId, AdSize.MediumRectangle, BigBannerPosition);
        AdRequest adRequest = new AdRequest.Builder().Build();
        MediumECPMBigbannerAd.LoadAd(adRequest);
    }
    private void MEDIUMLoadInterstitialAd()
    {
        MediumECPMInterstitialAd = new InterstitialAd(MediumInterstitialAdUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        MediumECPMInterstitialAd.LoadAd(adRequest);
    }

    private void MEDIUMLoadRewardedAd()
    {
        MediumECPMrewardedAd = new RewardedAd(MediumRewardedAdUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        MediumECPMrewardedAd.LoadAd(adRequest);
    }
    #endregion

    #region ALL PRICES ECPM LOAD ADS PORTION
    private void ALLPRICESLoadSmallBannerAd()
    {
        ALLPRICEECPMSmallbannerAd = new BannerView(ALLPriceBannerAdUnitId, AdSize.Banner, SmallBannerPosition);
        AdRequest adRequest = new AdRequest.Builder().Build();
        ALLPRICEECPMSmallbannerAd.LoadAd(adRequest);
    }
    private void ALLPRICESLoadBigBannerAd()
    {
        ALLPRICEECPMBigbannerAd = new BannerView(BIGbannerAllAdUnitId, AdSize.MediumRectangle, BigBannerPosition);
        AdRequest adRequest = new AdRequest.Builder().Build();
        ALLPRICEECPMBigbannerAd.LoadAd(adRequest);
    }
    private void ALLPRICESLoadInterstitialAd()
    {
        ALLPRICEECPMInterstitialAd = new InterstitialAd(ALLPriceInterstitialAdUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        ALLPRICEECPMInterstitialAd.LoadAd(adRequest);
    }

    private void ALLPRICESLoadRewardedAd()
    {
        ALLPRICEECPMrewardedAd = new RewardedAd(ALLPriceRewardedAdUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        ALLPRICEECPMrewardedAd.LoadAd(adRequest);
    }
    #endregion

    #region SHOW ADS PORTIOn
    public void ShowInterstitialAd()
    {
       
        if (interstitialAd != null && interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
            LoadInterstitialAd();
            MEDIUMLoadInterstitialAd();
            ALLPRICESLoadInterstitialAd();
        }
        else if (MediumECPMInterstitialAd != null && MediumECPMInterstitialAd.IsLoaded())
        {
            MediumECPMInterstitialAd.Show();
            LoadInterstitialAd();
            MEDIUMLoadInterstitialAd();
            ALLPRICESLoadInterstitialAd();
        }
        else if (ALLPRICEECPMInterstitialAd != null && ALLPRICEECPMInterstitialAd.IsLoaded())
        {
            ALLPRICEECPMInterstitialAd.Show();
            LoadInterstitialAd();
            MEDIUMLoadInterstitialAd();
            ALLPRICESLoadInterstitialAd();
        }

        else
        {
            Advertisement.Load(InterstatialPlacement, this);
            Advertisement.Show(InterstatialPlacement, this);
            LoadInterstitialAd();
            MEDIUMLoadInterstitialAd();
            ALLPRICESLoadInterstitialAd();
        }
        
    }

    public void ShowRewardedAd()
    {
        //Debug.Log("Show AD");
        Advertisement.Load(REWARDED_VIDEO_PLACEMENT, this);
        Advertisement.Show(REWARDED_VIDEO_PLACEMENT, this);
        //if (rewardedAd != null && rewardedAd.IsLoaded())
        //{
        //    rewardedAd.Show();

        //    LoadRewardedAd();
        //    MEDIUMLoadRewardedAd();
        //    ALLPRICESLoadRewardedAd();
        //}else if (MediumECPMrewardedAd != null && MediumECPMrewardedAd.IsLoaded())
        //{
        //    MediumECPMrewardedAd.Show();

        //    LoadRewardedAd();
        //    MEDIUMLoadRewardedAd();
        //    ALLPRICESLoadRewardedAd();
        //}
        //else if (ALLPRICEECPMrewardedAd != null && ALLPRICEECPMrewardedAd.IsLoaded())
        //{
        //    ALLPRICEECPMrewardedAd.Show();

        //    LoadRewardedAd();
        //    MEDIUMLoadRewardedAd();
        //    ALLPRICESLoadRewardedAd();
        //}
        //else
        //{
        //    Advertisement.Load(REWARDED_VIDEO_PLACEMENT, this);
        //    Advertisement.Show(REWARDED_VIDEO_PLACEMENT, this);
        //    LoadRewardedAd();
        //    MEDIUMLoadRewardedAd();
        //    ALLPRICESLoadRewardedAd();
        //}
    }

    public void ShowSmallBannerAd()
    {
        if (SmallbannerAd != null)
        {
            SmallbannerAd.Show();
        }
        else if (MediumECPMSmallbannerAd != null)
        {
            MediumECPMSmallbannerAd.Show();
        }
        else if (ALLPRICEECPMSmallbannerAd != null)
        {
            ALLPRICEECPMSmallbannerAd.Show();
        }
        else
        {
            LoadSmallBannerAd();
            MEDIUMLoadSmallBannerAd();
            ALLPRICESLoadSmallBannerAd();
        }

        if (BigbannerAd != null)
        {
            BigbannerAd.Hide();
        }
        else if (MediumECPMBigbannerAd != null)
        {
            MediumECPMBigbannerAd.Hide();
        }else if (ALLPRICEECPMBigbannerAd != null)
        {
            ALLPRICEECPMBigbannerAd.Hide();
        }

    }


    public void ShowBiGBannerAd()
    {
        if (BigbannerAd != null)
        {
            BigbannerAd.Show();

        }else if (MediumECPMBigbannerAd != null)
        {
            MediumECPMBigbannerAd.Show();

        }else if (ALLPRICEECPMBigbannerAd != null)
        {
            ALLPRICEECPMBigbannerAd.Show();

        }
        else
        {
            LoadBigBannerAd();
            MEDIUMLoadBigBannerAd();
            ALLPRICESLoadBigBannerAd();
        }




        if (SmallbannerAd != null)
        {
            SmallbannerAd.Hide();
        }else if (MediumECPMSmallbannerAd != null)
        {
            MediumECPMSmallbannerAd.Hide();
        }else if (ALLPRICEECPMSmallbannerAd != null)
        {
            ALLPRICEECPMSmallbannerAd.Hide();
        }
    }

    #endregion

    #region RewardedAD CallBack
    
    private void HandleUserEarnedReward(object sender, Reward args)
    {
        //string type = args.Type;
        //double amount = args.Amount;
        //Debug.Log("User Win Reward");
        //Debug.Log("User earned reward of " + amount + " " + type);
        //// You can implement your reward logic here
        //Debug.Log("Complete");
        //if (PlayerPrefs.GetInt("SkipLevel") == 1)
        //{
        //    PlayerPrefs.SetInt("SkipLevel", 0);
        //    GameManager.instance.SkippingLevel();
        //}
        //if (PlayerPrefs.GetInt("UnlockAllLevels") > 2)
        //{
        //    PlayerPrefs.SetInt("UnlockAllLevels", 5);
        //    MainMenuScript.instance.UnlockingAllLevels();
        //}
        //if (PlayerPrefs.GetInt("SurvivalModeAd") > 2)
        //{
        //    PlayerPrefs.SetInt("SurvivalModeAd", 1);
        //    MainMenuScript.instance.UnlockingSurvivalModeWithAds();
        //}
        //if (PlayerPrefs.GetInt("HealthReward") == 1)
        //{
        //    PlayerPrefs.SetInt("HealthReward", 0);
        //    WaveManager_.instance._giveawayCounterForAd = 0;
        //    WaveManager_.instance.GiveawayGiving();
        //}
        //if (PlayerPrefs.GetInt("AmmoReward") == 1)
        //{
        //    PlayerPrefs.SetInt("AmmoReward", 0);
        //    WaveManager_.instance.GiveawayGiving();
        //    WaveManager_.instance._giveawayCounterForAd = 0;
        //}
        //if (PlayerPrefs.GetInt("UnlockNextLevel") == 1)
        //{
        //    PlayerPrefs.SetInt("UnlockNextLevel", 0);
        //    MainMenuScript.instance.UnlockingNextLevel();
        //}
    }


    #endregion

    #region Unity Ads

    public bool isRewarded;
    public void OnUnityAdsShowStart(string _adUnitId) { }
    public void OnUnityAdsShowClick(string _adUnitId) { }
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) {

        
            Debug.Log("Complete");
            if (isRewarded)
            {
                if (PlayerPrefs.GetInt("SkipLevel") == 1)
                {
                    PlayerPrefs.SetInt("SkipLevel", 0);
                    GameManager.instance.SkippingLevel();
                }
                if (PlayerPrefs.GetInt("ShowLevelsAd") == 1)
                {
                    PlayerPrefs.SetInt("ShowLevelsAd", 0);
                    MainMenuScript.instance.UnlockingAllLevels();
                }
                if (PlayerPrefs.GetInt("ShowModeAd") == 1)
                {
                    PlayerPrefs.SetInt("ShowModeAd", 0);
                    MainMenuScript.instance.UnlockingSurvivalModeWithAds();
                }
                if (PlayerPrefs.GetInt("HealthReward") == 1)
                {
                    PlayerPrefs.SetInt("HealthReward", 0);
                    WaveManager_.instance._giveawayCounterForAd = 0;
                    WaveManager_.instance.GiveawayGiving();
                }
                if (PlayerPrefs.GetInt("AmmoReward") == 1)
                {
                    PlayerPrefs.SetInt("AmmoReward", 0);
                    WaveManager_.instance.GiveawayGiving();
                    WaveManager_.instance._giveawayCounterForAd = 0;
                }
                if (PlayerPrefs.GetInt("UnlockNextLevel") == 1)
                {
                    PlayerPrefs.SetInt("UnlockNextLevel", 0);
                    MainMenuScript.instance.UnlockingNextLevel();
                }
            }
        
        isRewarded = false;
    }

   

    


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }



    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }







    #endregion



}
