//GrenadeObject.cs by Azuline Studios© All Rights Reserved
//Detonates grenade after fuse timer has elapsed.
using UnityEngine;
using System.Collections;

public class GrenadeObject : MonoBehaviour {
	
	[HideInInspector]
	public float fuseTimeAmt;
	private float startTime;
	private ExplosiveObject ExplosiveObjectComponent;
	private WeaponBehavior WeaponBehaviorComponent;

	AudioSource explosionSound;

	void Start(){
		ExplosiveObjectComponent = GetComponent<ExplosiveObject>();

		explosionSound = GetComponent<AudioSource>();
	}

	void OnEnable()
	{
		startTime = Time.time;

		explosionSound.volume = PlayerPrefs.GetFloat("Music");
	}


	void Update () {
		if(startTime + fuseTimeAmt < Time.time){
			ExplosiveObjectComponent.ApplyDamage(ExplosiveObjectComponent.hitPoints + 1.0f);//detonate grenade
		}
	}
}
