using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections.Specialized;


public class GunController : MonoBehaviour
{
    [Header("Hotbar")]

    [SerializeField] Gun one;
    [SerializeField] Gun two;
    [SerializeField] Food three;
    [SerializeField] Food four;
    [SerializeField] Item currentItem;
    [HideInInspector] public KeyCode shootKey = KeyCode.Mouse0;

    [SerializeField] bool canSwitch = true;
    
    
    [SerializeField] TMP_Text statusText;

    Rigidbody rb;

    public int health = 100;


    void UText(string str){
        statusText.text = str;
    }


    void ApplyRecoil(Gun gun, RaycastHit hit){
        /*
            I need to make it so that when you shoot based on the camera rotation

        
        */
    
        Vector3 rotation = Camera.main.transform.forward;
        rb.velocity += -rotation * gun.recoil / Mathf.CeilToInt(Mathf.Sqrt(hit.distance / 10));
        
    }

    IEnumerator ShootGun(Gun gun){
        
        gun.canUse = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, gun.range)){
            Debug.Log($"You hit {hit.transform.name}");
            ApplyRecoil(gun, hit);
        }

        else{
            Debug.Log("You missed!");
        }


        
        UText($"Shoot Cooldown for {gun.name}");
        yield return new WaitForSeconds(gun.shotCD);
        UText($"Cooldown over for {gun.name}");

        gun.canUse = true;
    }

    IEnumerator EatFood(Food food){
        
        food.canUse = false;
        
        UText($"Eating {food}");
        yield return new WaitForSeconds(food.eatTime);
        
        if(food.healing != 0){
            if(food.healTime == 0){
                if(health + food.healing > 100){
                    health = 100;
                }
                else{
                    health += food.healing;
                }
            }

            else{
                for(int i = 0; i < food.healTime; i++){
                    if(health + food.healing / food.healTime > 100){
                        health = 100;
                    }

                    else{
                        health += (int)(food.healing / food.healTime);
                    }

                    yield return new WaitForSeconds(food.healTime);
                }
            }
        }
        
        UText($"Cooldown for {food.name}");
        yield return new WaitForSeconds(food.CD);
        UText($"Cooldown over for {food.name}");

        food.canUse = true;
    }

    void Start(){

        if(one != null){
            one.canUse = true;
        }

        if(two != null){
            two.canUse = true;
        }
        
        if(three != null){
            three.canUse = true;
        }

        if(four != null){
            four.canUse = true;
        }
        
        UText($"Currently Using {one.name}");
        currentItem = one;

        rb = GetComponent<Rigidbody>();
    }



    IEnumerator SwitchItem(Item item){
        canSwitch = false;
        yield return new WaitForSeconds(item.holsterTime);
        currentItem = item;
        canSwitch = true;
        UText($"Currently Using {currentItem.name}");
    }

    void Update(){
        if(currentItem.canUse){
            if(Input.GetKeyDown(shootKey) && currentItem is Gun){
                UText($"Shooting {currentItem.name}");
                StartCoroutine(ShootGun((Gun)currentItem));
            }

            if(Input.GetKeyDown(shootKey) && currentItem is Food){
                // Casts the type because i was having bugs with alternate methods, i think it saves memory too!
                UText($"Eating {currentItem.name}");
                StartCoroutine(EatFood((Food)currentItem));

            }
        }
        

        if(Input.GetKeyDown(KeyCode.Alpha1) && canSwitch){
            UText($"Switching to {one.name}");
            StartCoroutine(SwitchItem(one));
        }

        if(Input.GetKeyDown(KeyCode.Alpha2) && canSwitch){
            UText($"Switching to {two.name}");
            StartCoroutine(SwitchItem(two));   
        }

        if(Input.GetKeyDown(KeyCode.Alpha3) && canSwitch){
            UText($"Switching to {two.name}");
            StartCoroutine(SwitchItem(two));
        }

        if(Input.GetKeyDown(KeyCode.Alpha4) && canSwitch){
            UText($"Switching to {two.name}");
            StartCoroutine(SwitchItem(two));
        }
    }



}
