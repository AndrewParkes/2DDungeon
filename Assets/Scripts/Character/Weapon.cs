using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float damage = 10f;
    private float range = 1.5f;
    readonly float aliveTime = 0.2f;
    readonly float knockBackForce = 3;

    public GameObject projectile;

    public void Attack(Vector3 position, Vector2 direction) {
        GameObject projectileClone = Instantiate(projectile, position + new Vector3(direction.x, direction.y, 0), Quaternion.Euler(0,0,Vector3.Angle(transform.up, new Vector3(direction.x, direction.y, 0))));
        Destroy(projectileClone, aliveTime);
    }

    public float KnockBackForce() {
        return knockBackForce;
    }
}
