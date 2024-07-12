using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public bool canUse;
    public float holsterTime;

      void Awake(){
        canUse = true;
    }
}
