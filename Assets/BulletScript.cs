using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public enum WeaponType
    {
        Pistol,
        Shotgun,
        // Add more weapon types as needed
    }

    public int damage = 10;
    public WeaponType weaponType = WeaponType.Pistol; // Set default weapon type
    
   /* void OnCollisionEnter(Collider other)
    {
        // Check if the bullet hit a zombie
        if (other.CompareTag("Zombie"))
        {
            // Get the ZombieAI component and apply damage based on weapon type
            DetectPlayer zombie = other.GetComponent<DetectPlayer>();
            if (zombie != null)
            {
                //zombie.TakeDamage(GetDamageForWeaponType());
                //Debug.LogError("collision");
                // Destroy the bullet
                //Destroy(gameObject);
               
            }


        }
    }*/

    int GetDamageForWeaponType()
    {
        // Adjust damage based on weapon type
        switch (weaponType)
        {
            case WeaponType.Pistol:
                return damage; // Use the default damage for the pistol
            case WeaponType.Shotgun:
                return damage * 2; // Example: Shotgun deals double damage
            // Add more cases for additional weapon types
            default:
                return damage; // Use default damage if weapon type is not recognized
        }
    }
}
