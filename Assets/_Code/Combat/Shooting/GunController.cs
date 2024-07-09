using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Current Weapon")]    

    public Weapon currentWeapon;
    public Weapon nullWeapon;
    

    [Space(10)]

    [Header("Weapons")]

    public Weapon pistol;


    [Header("Inventory")]
    // ! You should only be able to hold 5 weapons!
    public  Weapon[] weapons = new Weapon[5];

    void AddPistol(){
        ResetWeapons();
        weapons[0] = pistol;
        currentWeapon = weapons[0];

        if(currentWeapon == null || weapons[0] == null){
            Debug.LogWarning("Either your current weapon or the pistol is null!");
        }
    }

    void ResetWeapons(){

        weapons = new Weapon[5];
        //! Makes sure that the Array can have items removed 
        if(weapons.Length > 0){
            for(int i = 0; i < weapons.Length; i++){
                weapons[i] = null;
            }
        }        
    }

    void Attack(){
        bool isGun = currentWeapon.weapType == weaponType.gun;
        
        if(isGun){
            //! Insert gun logic here
        }

        else{
            //? Insert Melee logic here
        }
    }

    void Start()
    {
        ResetWeapons();
        AddPistol();
    }
    
}
