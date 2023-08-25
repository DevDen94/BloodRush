using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{
   public static AdManager Instance;
   [HideInInspector] public bool isShowingAd;

   
   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else
      {
         DestroyImmediate(gameObject);
      }
   }

   public void ShowInterstitialAd()
   {
      GoogleAdMobController.instance.ShowInterstitialAd();
    
   }
   
   public void ShowRewardedAd()
   {
      GoogleAdMobController.instance.ShowRewardedAd();
   }

   public void ShowBanner()
   {
      GoogleAdMobController.instance.RequestBannerAd();
   }

   public void HideBanner()
   {
      GoogleAdMobController.instance.DestroyBannerAd();
   }
}
