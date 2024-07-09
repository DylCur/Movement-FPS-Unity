using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Food", menuName = "Items/Consumable/Food", order = 2)]
public class Food : Consumable
{
    [SerializeField] float eatTime;

    struct attackIncrease
    {
        float increase;
        float duration;
    }

    struct healthIncrease
    {
        float increase;
        float duration; 
    }

    struct defenceIncrease
    {
        float increase;
        float duration;
    }
}
