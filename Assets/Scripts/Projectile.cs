using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage = 10f;

    [SerializeField]
    public float knockBackForce { get; } = 3f;

    public float GetDamage() {
        return damage;
    }
}
