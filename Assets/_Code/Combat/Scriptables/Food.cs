using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Items/Food", order = 1)]
public class Food : Item
{
    public int healing;
    public int healingTime;
    
    public float damage;
    public int damageTime;

    public bool canEat;

    void Awake(){
        canEat = true;
    }
}