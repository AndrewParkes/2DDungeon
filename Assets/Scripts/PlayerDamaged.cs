using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : MonoBehaviour
{

    PlayerBase PlayerBase;
    Rigidbody2D rb;
    Character2DController characterController;

    void Start() {
        PlayerBase = GetComponent<PlayerBase>();
        rb = GetComponent<Rigidbody2D>();
        characterController = GetComponent<Character2DController>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.transform.GetComponent<Enemy>();
            
            if (enemy != null)
            {
                Damage(enemy.GetContactDamage());
                KnockBack(rb.transform.position - enemy.transform.position, enemy.GetKnockBackForce());
            }
        }
    }

    void Damage(float damage) {
        PlayerBase.UpdateHealth(-1 * damage);
    }

    void KnockBack(Vector3 knockBackDirection, float force) {
        characterController.BlockMoveMentForKnockBack(force);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f; 
        rb.AddForce( knockBackDirection.normalized * force, ForceMode2D.Impulse);
    }
}
