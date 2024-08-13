using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunSelection : MonoBehaviour
{

    public GameObject[] Guns;
    public int CurrentWeapon;
    public GameObject BuyBtn, NextBtn;
    public int[] Prices;
    public SaveMainMenuWeaponData SaveWeaponData;
    // Start is called before the first frame update
    void Start()
    {
        Guns[CurrentWeapon].SetActive(true);
        PlayerPrefs.SetInt("BuyWeapon" + 0, 1);
        LockUnlockWeapons();
        //SaveWeaponData.HaveInInventory.Length = Guns.Length;
    }

    public void ChangeWeapon(int car)
    {
        Guns[CurrentWeapon].SetActive(false);
       
        CurrentWeapon += car;
        
        if (CurrentWeapon >= Guns.Length)
        {
            CurrentWeapon = 0;
        }
        else if (CurrentWeapon < 0)
        {
            CurrentWeapon = Guns.Length - 1;
        }
        Guns[CurrentWeapon].SetActive(true);
        LockUnlockWeapons();
        PlayerPrefs.SetInt("SelectedWeapon", CurrentWeapon);
    }
    public void LockUnlockWeapons()
    {
        if(PlayerPrefs.GetInt("BuyWeapon" + CurrentWeapon) == 1)
        {
            BuyBtn.SetActive(false);
            NextBtn.SetActive(true);
        }
        else
        {
            BuyBtn.SetActive(true);
            NextBtn.SetActive(false);
            BuyBtn.transform.GetChild(0).GetComponent<Text>().text = "Buy" + Prices[CurrentWeapon].ToString();
        }
    }

    public void BuyWeapon()
    {

        PlayerPrefs.SetInt("BuyWeapon" + CurrentWeapon, 1);
        BuyBtn.SetActive(false);
        NextBtn.SetActive(true);
        SaveWeaponData.HaveInInventory[CurrentWeapon] = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
