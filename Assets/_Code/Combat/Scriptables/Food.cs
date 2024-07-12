using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Items/Food", order = 1)]
public class Food : Item
{
    public float eatTime;
    public float CD;

    public int healing;
    public float healTime;
    
    public float damage;
    public int damageTime;

}