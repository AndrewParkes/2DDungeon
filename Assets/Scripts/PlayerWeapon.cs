using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    public List<GameObject> childWeapons;
         
    void Start()
    {
        foreach (Transform child in transform)
        {
            childWeapons.Add(child.gameObject);
        }
    }

    public void Attack(Vector2 direction) {
        Debug.DrawRay(transform.position, transform.TransformDirection(direction) * 1.5f, Color.green, 1f);

        foreach (GameObject childWeapon in childWeapons)
        {
            childWeapon.GetComponent<Weapon>().Attack(transform.position, transform.TransformDirection(direction));
        }
    }
}
