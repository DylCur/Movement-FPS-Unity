using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Raycast", menuName = "Weapons/Raycast", order = 2)]
public class Ray : Gun
{
    
    public float range;
    public float[] spread;
    
    private void Awake()
    {
        range = Mathf.Infinity;
        spread = new float[2] {0, 0};
    }
}
