using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Gun", menuName = "Items/Gun", order = 0)]
public class Gun : Item
{
    public float range;
    public int damage;
    public float shotCD;

    void Awake(){
        range = Mathf.Infinity;
        damage = 10;
    }
}
