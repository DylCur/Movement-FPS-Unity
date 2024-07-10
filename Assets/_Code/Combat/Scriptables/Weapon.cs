using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Consumable
{
    [Header("Information")]

    public string weaponName;
    [TextArea(10, 100)] public  string description;

    [Header("Stats")]
    public int damage;
    public int armourPiercing;

}
