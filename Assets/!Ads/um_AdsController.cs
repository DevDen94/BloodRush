using UnityEngine;

public class um_AdsController : MonoBehaviour {
	static public um_AdsController Instance;
	bool isPlay=false;


	void Awake()
	{
		Instance = this;
		if(!PlayerPrefs.HasKey ("ADSUNLOCK"))
		{
			PlayerPrefs.SetInt ("ADSUNLOCK", 0);
		}


	}


	public void Pause()
	{
		isPlay = !isPlay;
		if (PlayerPrefs.GetInt("ADSUNLOCK") == 0 && isPlay)
		{
			HideBanner();
			AdManager.Instance.ShowInterstitialAd();
		}
	}

	public void CompleteWin()
	{
		if (PlayerPrefs.GetInt("ADSUNLOCK") == 0)
		{
			HideBanner();
			AdManager.Instance.ShowInterstitialAd();
		}
	}

	public void LevelFailed()
	{
		if (PlayerPrefs.GetInt ("ADSUNLOCK") == 0) {
			HideBanner ();
			AdManager.Instance.ShowInterstitialAd();
		}
	}
	public void ExitGame(){
		if (PlayerPrefs.GetInt ("ADSUNLOCK") == 0) { 
			AdManager.Instance.ShowInterstitialAd();
		}
	}
	public void ShowBanner()
	{
		if (PlayerPrefs.GetInt ("ADSUNLOCK") == 0) {
			AdManager.Instance.ShowBanner();
		}
	}
	public void HideBanner(){
		if (PlayerPrefs.GetInt ("ADSUNLOCK") == 0) {
			AdManager.Instance.HideBanner();
		}
	}
	public void LoadingAds()
	{
		if (PlayerPrefs.GetInt ("ADSUNLOCK") == 0) {
			AdManager.Instance.ShowInterstitialAd();
		}
	}

	public void CompleteNextButton()
	{

		if (PlayerPrefs.GetInt("ADSUNLOCK") == 0)
		{
			AdManager.Instance.ShowInterstitialAd();
		}
	}

	public void LevelSelectionBack()
	{
		
	}
	public void HomebuttonClick()
	{
		
	}

}
