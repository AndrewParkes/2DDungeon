using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : MonoBehaviour
{

    PlayerBase PlayerBase;

    void Start() {
        PlayerBase = GetComponent<PlayerBase>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.transform.GetComponent<Enemy>();
            
            if (enemy != null)
            {
                Damage(enemy.GetContactDamage());
            }
        }
    }

    void Damage(float damage) {
        PlayerBase.UpdateHealth(-1 * damage);
    }
}
