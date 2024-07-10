using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Projectile", menuName = "Weapons/Projectile", order = 1)]
public class Projectile : Gun
{
    public float velocity;
    public bool gravity;
    public float gravityForce;
    public GameObject proj;

    private void Awake()
    {
        velocity = 10f;
        gravity = false;
        gravityForce = 0; // This will be used to modify the gameobjects rigidbody
        proj = null;
    }
}
