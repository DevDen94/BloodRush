using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ZombieDamage : MonoBehaviourPunCallbacks
{
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void TakeDamage(float damage, int GunId)
    {
        // Apply damage to the zombie
        DetectPlayer.Instance.MaxHealth -= (int)damage * GunId;

        // Check if the zombie's health is zero or below
        if (DetectPlayer.Instance.MaxHealth <= 0)
        {
            // Zombie is dead, perform appropriate logic (e.g., play death animation, deactivate)
            // For demonstration, let's just destroy the zombie
            PhotonNetwork.Destroy(gameObject);
        }
        Debug.LogError("damage");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
