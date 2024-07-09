using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable/Base", order = 0)]
public class Consumable : ScriptableObject
{
    [SerializeField] KeyCode useKey = KeyCode.E;
    [Range(0f, 30f)]
    [SerializeField] float useCD;

    private void Awake()
    {
        useKey = KeyCode.E;
    }
}
