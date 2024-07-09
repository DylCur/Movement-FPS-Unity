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

    public Weapon primary;
    public Weapon secondary;
    public Weapon tertiary;


    [Header("Inventory")]
    // ! You should only be able to hold 3 weapons!
    public  Weapon[] weapons = new Weapon[3];

    
    void AddWeapon(int index, Weapon weapon)
    {
        weapons[index] = weapon;
    }

    void Start()
    {
        
    }
    
}
