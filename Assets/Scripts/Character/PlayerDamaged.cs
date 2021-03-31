using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : MonoBehaviour
{
    private PlayerBase playerBase;
    private Rigidbody2D rb;
    private Character2DController characterController;

    private void Start() {
        playerBase = GetComponent<PlayerBase>();
        rb = GetComponent<Rigidbody2D>();
        characterController = GetComponent<Character2DController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.transform.GetComponent<Enemy>();
            
            if(enemy != null)
            {
                Damage(enemy.GetContactDamage());
                KnockBack(rb.transform.position - enemy.transform.position, enemy.GetKnockBackForce());
            }
        }
    }

    private void Damage(float damage) {
        playerBase.UpdateHealth(-1 * damage);
    }

    private void KnockBack(Vector3 knockBackDirection, float force) {
        characterController.BlockMovementForKnockBack(force);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f; 
        rb.AddForce( knockBackDirection.normalized * force, ForceMode2D.Impulse);
    }
}
