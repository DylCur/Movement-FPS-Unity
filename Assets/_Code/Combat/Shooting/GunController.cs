using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Current Weapon")]    

    public Consumable currentItem;
    
    [Space(10)]

    [Header("Items")]

    public Weapon one; 
    public Weapon two;
    public Consumable three;
    public Consumable four;


    [Header("Inventory")]
    // This is temporary
    public Consumable[] items = new Consumable[4];
    public Weapon[] weapons = new Weapon[2];

    
    void AddWeapon(int index, Consumable item)
    {
        items[index] = item;
    }

    IEnumerator MeleeAttack(Melee melee)
    { 
        
    }

    IEnumerator GunAttack(Gun gun)
    {

    }

    IEnumerator UseHeal(HealingItem healing) 
    {
    
    }

    IEnumerator UseFood(Food food)
    {
        if (food.attackIncrease[0] != 0)
        {
            
            yield return new WaitForSecondsRealtime(food.attackIncrease[1]);

        }
    }

    void UseItem(Consumable item)
    {
        if (item is Weapon weapon)
        {
            if (weapon is Melee melee)
            {
                StartCoroutine(MeleeAttack(melee));
            }

            else if (weapon is Gun gun)
            {
                StartCoroutine(GunAttack(gun));
            }
        }
        else
        {
            if (item is HealingItem healing)
            {
                StartCoroutine(UseHeal(healing));
            }

            else if (item is Food food)
            {
                StartCoroutine(UseFood(food));
            }
        }
        void Start()
        {

        }
    }
    
}
