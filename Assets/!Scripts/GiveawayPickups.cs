using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveawayPickups : MonoBehaviour
{
    float _lifeTime = 5f;

    float _slomoTimer = 10f;

    bool _startTimer = false;


    private void Update()
    {
        if (_startTimer)
        {
            Debug.Log("Slomo Timer : " + _slomoTimer);
            SlomoTimer();
        }
        //LifeTimeCode();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(gameObject.name == "Health(Clone)")
            {
                if (PlayerPrefs.GetInt("Health") < 2)
                {
                    FPSPlayer fPSP = FindObjectOfType<FPSPlayer>();

                    fPSP.hitPoints = 100.0f;

                    WaveManager_.instance._healthBar.healthGui = 100.0f;
                    WaveManager_.instance._healthBar.Health.value = WaveManager_.instance._healthBar.healthGui;

                    PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health") + 1);
                }
                else
                {
                    WaveManager_.instance._giveawayClaimAdButton.SetActive(true);
                    WaveManager_.instance._giveawayCounterForAd = 1;
                }

                Destroy(gameObject);
            }
            else if(gameObject.name == "Ammo(Clone)")
            {
                if (PlayerPrefs.GetInt("Ammo") < 2)
                {
                    WeaponBehavior wp = FindObjectOfType<WeaponBehavior>();

                    wp.ammo = wp.maxAmmo;

                    WaveManager_.instance._ammo.ammoGui2 = wp.ammo;

                    PlayerPrefs.SetInt("Ammo", PlayerPrefs.GetInt("Ammo") + 1);
                }
                else
                {
                    WaveManager_.instance._giveawayClaimAdButton.SetActive(true);
                    WaveManager_.instance._giveawayCounterForAd = 2;
                }

                Destroy(gameObject);
            }
            else if(gameObject.name == "Slomo(Clone)")
            {
                Debug.Log("Slomo");

                StartCoroutine(SlomoGiveaway());

                GetComponent<BoxCollider>().enabled = false;

                //Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.gameObject.name == "Health(Clone)" || gameObject.name == "Ammo(Clone)")
            {
                if (PlayerPrefs.GetInt("Health") > 2 || PlayerPrefs.GetInt("Ammo") > 2)
                {
                    WaveManager_.instance._giveawayClaimAdButton.SetActive(false);
                }
            }
        }
    }

    IEnumerator SlomoGiveaway()
    {
        
        yield return new WaitForSeconds(4.5f);
        WaveManager_.instance.ZombieContainer.SetActive(false);

        for (int i = 0; i < WaveManager_.instance._slomoZombieCount; i++)
        {
            int rand = Random.Range(0, WaveManager_.instance._allSlomoZombies.Length);

            Instantiate(WaveManager_.instance._allSlomoZombies[rand], WaveManager_.instance.SpawnPoints[i].transform.position, WaveManager_.instance.SpawnPoints[i].transform.rotation, WaveManager_.instance._slomoZombieParent);
        }

        _startTimer = true;

        WaveManager_.instance._isSlomo = true;
        Debug.Log("Slomoawayyy");
    }

    void SlomoTimer()
    {
        _slomoTimer -= 0.01f;
        if( _slomoTimer <= 0 ) 
        {
            _startTimer = false;
            WaveManager_.instance._isSlomo = false;
            foreach(Transform tr in WaveManager_.instance._slomoZombieParent.transform)
            {
                Destroy(tr.gameObject);
            }
            WaveManager_.instance.ZombieContainer.SetActive(true);
        }
    }
}