using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healing", menuName = "Items/Consumable/Healing", order = 1)]
public class HealingItem : Consumable
{
    [Range(1, 100)]
    public int healing;
    public float healingTime; // If this is 0, it will be used instantly

}
