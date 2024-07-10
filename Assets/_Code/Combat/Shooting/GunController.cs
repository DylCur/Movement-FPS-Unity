using JetBrains.Annotations;
using System;
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

    [Range(0, 100)]
    [SerializeField] int health;


    void AddWeapon(int index, Consumable item)
    {
        items[index] = item;
    }

    IEnumerator ItemCD(Consumable cons)
    {
        cons.canUse = false;
        yield return new WaitForSeconds(cons.useCD);
        cons.canUse = true;
    }

    IEnumerator MeleeAttack(Melee melee)
    {
        // Will add laters
        yield return null;
    }

    IEnumerator RayAttack(RaycastWeapon rayW){

        

        yield return null;
    }

    void GunAttack(Gun gun)
    {
        if(gun is RaycastWeapon ray){
            StartCoroutine(RayAttack(ray));
        }
    }

    IEnumerator UseHeal(HealingItem healing)
    {
        if(healing.healingTime == 0)
        {
            if(health + healing.healing > 100)
            {
                health = 100;
            }

            else
            {
                health += healing.healing;
            }
        }

        else
        {
            for (int i = 0; i < Mathf.CeilToInt(healing.healingTime); i++)
            {
                if (health + healing.healing / healing.healingTime > 100)
                {           
                    health = 100;
                }

                else
                {
                    health += Mathf.CeilToInt(healing.healing / healing.healingTime);
                }

                // This is a constant (Should be changed but idc)
                yield return new WaitForSeconds(1);
            }
        }
    }

    IEnumerator UseFood(Food food)
    {
        yield return new WaitForSeconds(food.eatTime);

        // Makes sure that the food will increase damage
        if (food.attackIncrease[0] != 0)
        {

            // Increases the damage of each weapon
            foreach (Weapon w in weapons)
            {
                w.damage = Mathf.FloorToInt(w.damage * food.attackIncrease[0]);
            }

            // Waits for the duration of the food
            yield return new WaitForSeconds(food.attackIncrease[1]);

            // Brings the damage back to normal
            foreach (Weapon w in weapons)
            {
                w.damage = Mathf.FloorToInt(w.damage / food.attackIncrease[0]);
            }

        }

        // Makes sure that the food will increase health
        if (food.healthIncrease[0] != 0)
        {
            if (food.healthIncrease[1] == 0)
            {
                if (health + food.healthIncrease[0] > 100)
                {
                    health = 100;
                }

                else
                {
                    health = Mathf.CeilToInt(health + food.healthIncrease[0]);
                }
            }

            else
            {
                /*
                    Lets say that it heals for 30 health over 5 seconds and the player is on 10HP
                    i = 0, and goes to 5.
                    
                0. 10 += 30 / 5 (10 += 6)
                1. 16 += 6
                2. 22 += 6
                3. 28 += 6
                4. 34 += 6
                5. 40 (Since 10 + 30 = 40, this is correct)
                    
                 
                 */


                for (int i = 0; i < Mathf.CeilToInt(food.healthIncrease[1]); i++)
                {
                    if (health + food.healthIncrease[0] / food.healthIncrease[1] > 100)
                    {
                        /*
                           Dont break here! If you break here and the player takes damage after the break,
                           But they have more healing left, they will not get it.
                        */

                        health = 100;
                    }

                    else
                    {
                        health += Mathf.CeilToInt(food.healthIncrease[0] / food.healthIncrease[1]);
                    }

                    // This is a constant (Should be changed but idc)
                    yield return new WaitForSeconds(1);
                }
            }


        }

        if (food.defenceIncrease[0] != 0) { 
            // Might add defence, might not. Havent decided yet!
        }

        // Begins the food cooldown
        ItemCD(food);
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
                    GunAttack(gun);
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
        }
    
}
