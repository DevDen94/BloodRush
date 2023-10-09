using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveawayPickups : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(gameObject.name == "Health(Clone)")
            {
                FPSPlayer fPSP = FindObjectOfType<FPSPlayer>();

                fPSP.hitPoints = 100.0f;

                WaveManager_.instance._healthBar.healthGui = 100.0f;
                WaveManager_.instance._healthBar.Health.value = WaveManager_.instance._healthBar.healthGui;
            }
            else if(gameObject.name == "Ammo(Clone)")
            {
                WeaponBehavior wp = FindObjectOfType<WeaponBehavior>();

                wp.ammo = wp.maxAmmo;

                WaveManager_.instance._ammo.ammoGui2 = wp.ammo;
            }

            Destroy(gameObject);
        }
    }
}
