using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveMainMenuWeaponData", menuName = "SaveWeapon", order = 1)]
public class SaveMainMenuWeaponData : ScriptableObject
{
    public bool[] HaveInInventory;

    // You can add more fields as needed
}