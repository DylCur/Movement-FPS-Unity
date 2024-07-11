using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [Header("Hotbar")]

    [SerializeField] Gun one;
    [SerializeField] Gun two;
    [SerializeField] Food three;
    [SerializeField] Food four;
    [SerializeField] Item currentItem;
    [HideInInspector] public KeyCode shootKey = KeyCode.Mouse0;

    public int health = 100;


    void ShootGun(Gun gun){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, gun.range)){
            Debug.Log($"You hit {hit.transform.name}");
        }

        else{
            Debug.Log("You missed!");
        }
    }

    IEnumerator EatFood(Food food){
        if(food.healingTime == 0){
            if(health + food.healing > 100){
                health = 100;
            }
            else{
                health += food.healing;
            }
        }

    }

    void Start(){
        currentItem = one;
    }

    void Update(){
        if(Input.GetKeyDown(shootKey) && currentItem is Gun){
            ShootGun((Gun)currentItem);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            currentItem = one;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)){
            currentItem = two;
        }

        if(Input.GetKeyDown(KeyCode.Alpha3)){
            currentItem = three;
        }

        if(Input.GetKeyDown(KeyCode.Alpha4)){
            currentItem = four;
        }
    }



}
