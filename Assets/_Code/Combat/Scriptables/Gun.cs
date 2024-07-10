using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ammos{
    any = 0,
    light = 1,
    shotgun = 2,
    heavy = 4
}


public class Gun : Weapon
{
    
    [Header("Gun Parameters")]

    public bool burst;
    public ushort shotsPerBurst;
    public ammos ammoType;
    
    void Awake() {
        weaponName = "Default Gun";    
        description = "Default Gun Description";
        ammoType = ammos.any;
        damage = 10;
        burst = false;
    }

    
}
