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

	public AudioSource explosionSound;

	void Start(){
		ExplosiveObjectComponent = GetComponent<ExplosiveObject>();

        explosionSound.volume = PlayerPrefs.GetFloat("Music");
    }

	void OnEnable()
	{
		startTime = Time.time;

		
	}


	void Update () {
		if(startTime + fuseTimeAmt < Time.time){
			ExplosiveObjectComponent.ApplyDamage(ExplosiveObjectComponent.hitPoints + 1.0f);//detonate grenade
		}
	}
}
