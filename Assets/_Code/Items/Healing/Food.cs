using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Food", menuName = "Items/Consumable/Food", order = 2)]
public class Food : Consumable
{
    public float eatTime;

    // 0 is increase, 1 is duration
    public float[] attackIncrease;
    public float[] healthIncrease;
    public float[] defenceIncrease;

}
