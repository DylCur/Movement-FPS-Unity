using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    none,
    melee,
    ranged
}

public class Weapon : ScriptableObject
{
    [Header("Information")]

    public string weaponName;
    [TextArea(10, 100)] public  string description;

    [Header("Stats")]
    public int damage;
    public int armourPiercing;
    public weaponType weapType;
}
