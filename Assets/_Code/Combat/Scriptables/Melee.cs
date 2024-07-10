using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Default Melee", menuName = "Weapons/Create/Create Melee", order = 2)]
public class Melee : Weapon
{
    
    [Header("Gun Parameters")]
    
    public float range;
    public float arcRange;

    void Awake() {
        weaponName = "Default Melee";    
        description = "Default Melee Description";
        range = 10;
        damage = 10;
        arcRange = 30;
    }

}
