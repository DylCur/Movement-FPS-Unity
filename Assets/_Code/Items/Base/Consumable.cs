using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable/Base", order = 0)]
public class Consumable : ScriptableObject
{
    public KeyCode useKey = KeyCode.E;
    [Range(0f, 30f)]
    public float useCD;
    public bool canUse;

    private void Awake()
    {
        useKey = KeyCode.E;
    }
}
