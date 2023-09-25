using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using System.Collections;

public class GoogleAdMobController : MonoBehaviour
{
    public static GoogleAdMobController instance;
    private AppOpenAd _appOpenAd;
    private BannerView _bannerViewSmall;
    private BannerView _bannerViewBig;
    private InterstitialAd _interstitialAd;
    private RewardedAd _rewardedAd;
    private RewardedInterstitialAd _rewardedInterstitialAd;
    private bool _isShowingAppOpenAd;
    private bool _appOpenTime;
    
    public string appId;
    public string bannerId;
    public string interstitialId;
    public string rewardedId;
    public string appOpenId;
    public string rewardedInterstitialId;
    public string BigBannerId;

    [Space]
    
    public bool debug;

    public bool test;
   
    public bool IsBannerLoading { get; private set; }
    public bool IsInterstitialLoading { get; private set; }
    public bool IsRewardedAdLoading { get; private set; }
    public bool IsRewardedInterstitialAdLoading { get; private set; }
    public bool IsAppOpenAdLoading { get; private set; }

    public bool IsAppOpen;
    

    #region UNITY MONOBEHAVIOR METHODS

    private void Awake()
    {

        IsAppOpen = true;
        if (instance == null)
            instance = this;
    }

    public void Start()
    {
        if (test)
        {
            bannerId = "ca-app-pub-3940256099942544/6300978111";
            interstitialId = "ca-app-pub-3940256099942544/1033173712";
            rewardedId = "ca-app-pub-3940256099942544/5224354917";
            rewardedInterstitialId = "ca-app-pub-3940256099942544/5354046379";
            appOpenId = "ca-app-pub-3940256099942544/3419835294";
        }
        MobileAds.SetiOSAppPauseOnBackground(true);

        List<String> deviceIds = new List<String>() {AdRequest.TestDeviceSimulator};

        // Add some test device IDs (replace with your own device IDs).
#if UNITY_IPHONE
        deviceIds.Add("96e23e80653bb28980d3f40beb58915c");
#elif UNITY_ANDROID
        deviceIds.Add("EDDFEB91C1559F89EF37BBE13F57AE8");
        deviceIds.Add("9D2BE52ADA1A881936CB3D501E935136");
        deviceIds.Add("ABF438FEC139C226A468F9F5B08111F5");
#endif

        // Configure TagForChildDirectedTreatment and test device IDs.
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
                .SetTestDeviceIds(deviceIds)
                .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(HandleInitCompleteAction);

        RequestSmallBannerAd();
        RequestBigBannerAd();

    }

    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        // Callbacks from GoogleMobileAds are not guaranteed to be called on
        // main thread.
        // In this example we use MobileAdsEventExecutor to schedule these calls on
        // the next Update() loop.
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (debug)
                Debug.Log("Initialization complete");
            
            
            Dictionary<string, AdapterStatus> map = initstatus.getAdapterStatusMap();
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;
                switch (status.InitializationState)
                {
                    case AdapterState.NotReady:
                        // The adapter initialization did not complete.
                        MonoBehaviour.print("Adapter: " + className + " not ready.");
                        break;
                    case AdapterState.Ready:
                        // The adapter was successfully initialized.
                        MonoBehaviour.print("Adapter: " + className + " is initialized.");
                        break;
                }
            }
            
            if (interstitialId != string.Empty)
                RequestAndLoadInterstitialAd();
            if (rewardedId != string.Empty)
                RequestAndLoadRewardedAd();
            if (rewardedInterstitialId != string.Empty)
                RequestAndLoadRewardedInterstitialAd();
            if(appOpenId !=string.Empty)
                RequestAndLoadAppOpenAd();
            
        });
    }

    
    #endregion

    #region HELPER METHODS

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .Build();
    }

    private bool _firstTime;
    public void OnApplicationPause(bool paused)
    {
        // Display the app open ad when the app is foregrounded.
        if (!_firstTime && !paused)
        {
            _firstTime = true;
            return;
        }

        if (appOpenId == string.Empty)
            return;

        if (AdManager.Instance.isShowingAd && !paused)
        {
            AdManager.Instance.isShowingAd = false;
            return;
        }

        if (!paused)
        {
            ShowAppOpenAd();
        }
    }

    void ResetAppOpenTime()
    {
        _appOpenTime = false;
    }
    #endregion

    #region BANNER ADS


    public void ShowSmallBannerAd()
    {
        _bannerViewBig.Hide();
        _bannerViewSmall.Show();
    }

    public void ShowBigBannerAd()
    {
        _bannerViewBig.Show();
        _bannerViewSmall.Hide();
    }


    public void RequestSmallBannerAd()
    {
        if (!PlayerPrefs.HasKey("IAPDone"))
        {
            if (debug)
                Debug.Log("Requesting Banner Ad.");

            IsBannerLoading = true;

            //if (_bannerViewSmall != null)
            //{
            //    _bannerViewSmall.Destroy();
            //}

            // Create a 320x50 banner at top of the screen
            _bannerViewSmall = new BannerView(bannerId, AdSize.Banner, AdPosition.TopLeft);

            // Add Event Handlers
            _bannerViewSmall.OnAdLoaded += (sender, args) => OnBannerLoaded();
            _bannerViewSmall.OnAdFailedToLoad += (sender, args) => OnBannerFailedToLoad();
            _bannerViewSmall.OnAdOpening += (sender, args) => OnBannerOpening();
            _bannerViewSmall.OnAdClosed += (sender, args) => OnBannerClosed();

            // Load a banner ad
            _bannerViewSmall.LoadAd(CreateAdRequest());
            _bannerViewSmall.Hide();
        }
    }
    public void RequestBigBannerAd()
    {
        if (!PlayerPrefs.HasKey("IAPDone"))
        {
            if (debug)
                Debug.Log("Requesting Banner Ad.");

            IsBannerLoading = true;

            //if (_bannerView != null)
            //{
            //    _bannerView.Destroy();
            //}

            // Create a 320x50 banner at top of the screen
            _bannerViewBig = new BannerView(BigBannerId, AdSize.MediumRectangle, AdPosition.TopRight);

            // Add Event Handlers
            _bannerViewBig.OnAdLoaded += (sender, args) => OnBannerLoaded();
            _bannerViewBig.OnAdFailedToLoad += (sender, args) => OnBannerFailedToLoad();
            _bannerViewBig.OnAdOpening += (sender, args) => OnBannerOpening();
            _bannerViewBig.OnAdClosed += (sender, args) => OnBannerClosed();

            // Load a banner ad
            _bannerViewBig.LoadAd(CreateAdRequest());
            _bannerViewBig.Hide();
        }
    }


    //public void RequestRightBannerAd()
    //{
    //    if (!PlayerPrefs.HasKey("IAPDone"))
    //    {
    //        if (debug)
    //            Debug.Log("Requesting Banner Ad.");

    //        IsBannerLoading = true;

    //        if (_bannerView != null)
    //        {
    //            _bannerView.Destroy();
    //        }

    //        // Create a 320x50 banner at top of the screen
    //        _bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.TopRight);

    //        // Add Event Handlers
    //        _bannerView.OnAdLoaded += (sender, args) => OnBannerLoaded();
    //        _bannerView.OnAdFailedToLoad += (sender, args) => OnBannerFailedToLoad();
    //        _bannerView.OnAdOpening += (sender, args) => OnBannerOpening();
    //        _bannerView.OnAdClosed += (sender, args) => OnBannerClosed();

    //        // Load a banner ad
    //        _bannerView.LoadAd(CreateAdRequest());
    //    }
    //}





    //public void DestroyBannerAd()
    //{
    //    if (_bannerView != null)
    //    {
    //        _bannerView.Destroy();
    //    }
    //}
    
    #endregion

    #region INTERSTITIAL ADS

    public void RequestAndLoadInterstitialAd()
    {
        if (debug)
            Debug.Log("Requesting Interstitial Ad.");
        
        IsInterstitialLoading = true;
        
        // Clean up interstitial before using it
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
        }

        _interstitialAd = new InterstitialAd(interstitialId);

        // Add Event Handlers
        _interstitialAd.OnAdLoaded += (sender, args) => OnInterstitialLoaded();
        _interstitialAd.OnAdFailedToLoad += (sender, args) => OnInterstitialFailedToLoad();
        _interstitialAd.OnAdOpening += (sender, args) => OnInterstitialOpening();
        _interstitialAd.OnAdClosed += (sender, args) => OnInterstitialClosed();

        // Load an interstitial ad
        _interstitialAd.LoadAd(CreateAdRequest());
    }

    public void ShowInterstitialAd()
    {
        if (!PlayerPrefs.HasKey("IAPDone"))
        {
            if (_interstitialAd.IsLoaded())
            {
                AdManager.Instance.isShowingAd = true;
                _interstitialAd.Show();
            }
            else
            {
                if (debug)
                    Debug.Log("Interstitial ad is not ready yet");

                if (!IsInterstitialLoading)
                    RequestAndLoadInterstitialAd();
            }
        }
    }

    public bool IsLoadedInterstitialAd()
    {
        return _interstitialAd.IsLoaded();
    }

    #endregion

    #region REWARDED ADS

    public void RequestAndLoadRewardedAd()
    {
        if (debug)
            Debug.Log("Requesting Rewarded Ad.");
        IsRewardedAdLoading = true;
        // create new rewarded ad instance
        _rewardedAd = new RewardedAd(rewardedId);

        // Add Event Handlers
        _rewardedAd.OnAdLoaded += (sender, args) => OnRewardAdLoaded();
        _rewardedAd.OnAdFailedToLoad += (sender, args) => OnRewardAdFailedToLoad();
        _rewardedAd.OnAdOpening += (sender, args) => OnRewardAdOpening();
        _rewardedAd.OnAdFailedToShow += (sender, args) => OnRewardAdFailedToShow();
        _rewardedAd.OnAdClosed += (sender, args) => OnRewardAdClosed();
        _rewardedAd.OnUserEarnedReward += (sender, args) => OnRewardAdUserEarnedReward();

        // Create empty ad request
        _rewardedAd.LoadAd(CreateAdRequest());
    }

    public void ShowRewardedAd()
    {
        if (_rewardedAd != null)
        {
            AdManager.Instance.isShowingAd = true;
            _rewardedAd.Show();
        }
        else
        {
            
            if (debug)
                Debug.Log("Rewarded ad is not ready yet.");
            if (!IsRewardedAdLoading)
                RequestAndLoadRewardedAd();
        }
    }


   

    public bool IsLoadedRewardedAd()
    {
        return _rewardedAd.IsLoaded();
    }
    #endregion

    #region RewardedInterstitialAd

    public void RequestAndLoadRewardedInterstitialAd()
    {
        if (debug)
            Debug.Log("Requesting Rewarded Interstitial Ad.");
        IsRewardedInterstitialAdLoading = true;
        // Create an interstitial.
        RewardedInterstitialAd.LoadAd(rewardedInterstitialId, CreateAdRequest(), (rewardedInterstitialAd, error) =>
        {
            if (error != null)
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (debug)
                        Debug.Log("RewardedInterstitialAd load failed, error: " + error);
                    IsRewardedInterstitialAdLoading = false;
                });
                return;
            }

            this._rewardedInterstitialAd = rewardedInterstitialAd;
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                if (debug)
                    Debug.Log("RewardedInterstitialAd loaded");
                IsRewardedInterstitialAdLoading = false;
            });
            // Register for ad events.
            this._rewardedInterstitialAd.OnAdDidPresentFullScreenContent += (sender, args) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (debug)
                        Debug.Log("Rewarded Interstitial presented.");
                });
            };
            this._rewardedInterstitialAd.OnAdDidDismissFullScreenContent += (sender, args) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (debug)
                        Debug.Log("Rewarded Interstitial dismissed.");
                    if (_rewardedInterstitialRewardEarned)
                    {
                        
                    }
                    else
                    {
                        
                    }

                    _rewardedInterstitialRewardEarned = false;
                    
                    if (!IsRewardedInterstitialAdLoading)
                        RequestAndLoadRewardedInterstitialAd();
                });
                this._rewardedInterstitialAd = null;
            };
            this._rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (debug)
                        Debug.Log("Rewarded Interstitial failed to present.");
                });
                this._rewardedInterstitialAd = null;
                if (!IsRewardedInterstitialAdLoading)
                    RequestAndLoadRewardedInterstitialAd();
            };
        });
    }

    private bool _rewardedInterstitialRewardEarned;
    public void ShowRewardedInterstitialAd()
    {
        if (_rewardedInterstitialAd != null)
        {
            AdManager.Instance.isShowingAd = true;
            _rewardedInterstitialAd.Show((reward) =>
            {
                _rewardedInterstitialRewardEarned = true;
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (debug)
                        Debug.Log("User Rewarded: " + reward.Amount);
                });
            });
        }
        else
        {
            if (debug)
                Debug.Log("Rewarded ad is not ready yet.");
            
            if (!IsRewardedInterstitialAdLoading)
                RequestAndLoadRewardedInterstitialAd();
        }
    }

    #endregion

    #region APPOPEN ADS

    public void RequestAndLoadAppOpenAd()
    {
        if (debug)
            Debug.Log("Requesting App Open Ad.");
        
        IsAppOpenAdLoading = true;

        AppOpenAd.LoadAd(appOpenId, ScreenOrientation.LandscapeRight, CreateAdRequest(), (appOpenAd, error) =>
        {
            if (error != null)
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (debug)
                        Debug.Log("AppOpenAd load failed, error: " + error);
                    IsAppOpenAdLoading = false;
                });
                return;
            }

            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                if (debug)
                    Debug.Log("AppOpenAd loaded. Please background the app and return.");
                IsAppOpenAdLoading = false;
            });
            this._appOpenAd = appOpenAd;
        });
    }

    public void ShowAppOpenAd()
    {

        if (!PlayerPrefs.HasKey("IAPDone"))
        {
            if (_isShowingAppOpenAd)
            {
                return;
            }

            if (_appOpenTime) return;
            _appOpenTime = true;
            Invoke(nameof(ResetAppOpenTime), 15);

            if (_appOpenAd == null)
            {
                if (!IsAppOpenAdLoading)
                    RequestAndLoadAppOpenAd();
                return;
            }

            // Register for ad events.
            this._appOpenAd.OnAdDidDismissFullScreenContent += (sender, args) =>
            {
                _isShowingAppOpenAd = false;

                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    Debug.Log("AppOpenAd dismissed.");
                    if (this._appOpenAd != null)
                    {
                        this._appOpenAd.Destroy();
                        this._appOpenAd = null;
                    }
                });

                if (!IsAppOpenAdLoading)
                    RequestAndLoadAppOpenAd();
            };
            this._appOpenAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
            {
                _isShowingAppOpenAd = false;
                var msg = args.AdError.GetMessage();
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (debug)
                        Debug.Log("AppOpenAd present failed, error: " + msg);
                    if (this._appOpenAd != null)
                    {
                        this._appOpenAd.Destroy();
                        this._appOpenAd = null;
                    }
                });

                if (!IsAppOpenAdLoading)
                    RequestAndLoadAppOpenAd();
            };
            this._appOpenAd.OnAdDidPresentFullScreenContent += (sender, args) =>
            {
                _isShowingAppOpenAd = true;
                MobileAdsEventExecutor.ExecuteInUpdate(() => { Debug.Log("AppOpenAd presented."); });
            };
            this._appOpenAd.OnAdDidRecordImpression += (sender, args) =>
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() => { Debug.Log("AppOpenAd recorded an impression."); });
            };
            this._appOpenAd.OnPaidEvent += (sender, args) =>
            {
                string currencyCode = args.AdValue.CurrencyCode;
                long adValue = args.AdValue.Value;
                string suffix = "AppOpenAd received a paid event.";
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    string msg = string.Format("{0} (currency: {1}, value: {2}", suffix, currencyCode, adValue);
                    if (debug)
                        Debug.Log(msg);
                });
            };
            _appOpenAd.Show();
        }
    }

    #endregion


    
    
    #region Banner Callbacks

    private void OnBannerLoaded()
    {
        if (debug)
            Debug.Log("OnBannerLoaded");
        IsBannerLoading = false;
    }

    private void OnBannerFailedToLoad()
    {
        if (debug)
            Debug.Log("OnBannerFailedToLoad");
        IsBannerLoading = false;
    }

    private void OnBannerOpening()
    {
        if (debug)
            Debug.Log("OnBannerOpening");
    }

    private void OnBannerClosed()
    {
        if (debug)
            Debug.Log("OnBannerClosed");
    }

    #endregion

    #region Interstitial Callbacks

    private void OnInterstitialLoaded()
    {
       if (debug)
           Debug.Log("OnInterstitialLoaded");
       IsInterstitialLoading = false;
    }

    private void OnInterstitialFailedToLoad()
    {
        if (debug)
            Debug.Log("OnInterstitialFailedToLoad");
        IsInterstitialLoading = false;
    }
    
    private void OnInterstitialOpening()
    {
        if (debug)
            Debug.Log("OnInterstitialOpening");
    }
    
    private void OnInterstitialClosed()
    {
        if (debug)
            Debug.Log("OnInterstitialClosed");
        
        if (!IsInterstitialLoading)
            RequestAndLoadInterstitialAd();

        //Debug.Log("assa");

        //if(SceneManager.GetActiveScene().name == "Gameplay")
        //{
        //    if(GameplayHandler.instance.PausePanel.activeInHierarchy)
        //    {
        //        Debug.Log("Ingameplay pause");
        //        Time.timeScale = 0f;
        //    }
        //}
    }
    #endregion

    #region RewaredAd Callbacks

    private void OnRewardAdLoaded()
    {
        if (debug)
            Debug.Log("OnRewardAdLoaded");
        IsRewardedAdLoading = false;
    }
    
    private void OnRewardAdFailedToLoad()
    {
        if (debug)
            Debug.Log("OnRewardAdFailedToLoad");
        IsRewardedAdLoading = false;
    }
    
    private void OnRewardAdOpening()
    {
        if (debug)
            Debug.Log("OnRewardAdOpening");
    }
    
    private void OnRewardAdFailedToShow()
    {
        if (debug)
            Debug.Log("OnRewardAdFailedToShow");

        IsRewardedAdLoading = false;
        
        if (!IsRewardedAdLoading)
            RequestAndLoadRewardedAd();
    }
    
    private void OnRewardAdClosed()
    {

        Debug.Log("OnRewardAdClosed");
        

        if (debug)
            Debug.Log("OnRewardAdClosed");
        if (_earnedReward)
        {
           //um_WatchVideo.instance.RewadFunction.Invoke();
        }
        
        _earnedReward = false;
        if (!IsRewardedAdLoading)
            RequestAndLoadRewardedAd();
    }

    private bool _earnedReward;
    private void OnRewardAdUserEarnedReward()
    {
        if (debug)
            Debug.Log("OnRewardAdUserEarnedReward");
        _earnedReward = true;
    }

    #endregion
    
    public void adAfterSomeTime()
    {
        Invoke(nameof(ShowInterstitialAd), 3f);
    }
    
    #region AD INSPECTOR

    public void OpenAdInspector()
    {
        if (debug)
            Debug.Log("Open Ad Inspector.");

        MobileAds.OpenAdInspector((error) =>
        {
            if (error != null)
            {
                string errorMessage = error.GetMessage();
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (debug)
                        Debug.Log("Ad Inspector failed to open, error: " + errorMessage);
                });
            }
            else
            {
                MobileAdsEventExecutor.ExecuteInUpdate(() =>
                {
                    if (debug)
                        Debug.Log("Ad Inspector closed.");
                });
            }
        });
    }

    #endregion
}
