using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float contactDamage = 1f;
    [SerializeField] private float attackDamage = 20f;
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float movementDirection = 1f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float knockBackForce = 3f;
    [SerializeField] private GameObject player;

    private Health health;

    private Rigidbody2D rb;
    public Animator animator;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        health.SetHealth(100);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Projectile"))
        {
            Projectile projectile = collider.transform.GetComponent<Projectile>();
            if (projectile != null)
            {
                Damage(projectile.GetDamage());
                KnockBack(rb.transform.position - player.transform.position, projectile.knockBackForce);
            }
        }
    }

    private void Damage(float damage) {
        health.DealDamage(damage);
        CheckDead();
    }

    private void CheckDead() {
        if(health.GetHealth() < 0) {
            Destroy(gameObject);
        }
    }

    private void KnockBack(Vector3 knockBackDirection, float force) {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f; 
        rb.AddForce( knockBackDirection.normalized * force, ForceMode2D.Impulse);
    }

    public float GetAttackDamage() {
        return attackDamage;
    }
    
    public float GetContactDamage() {
        return contactDamage;
    }

    public float GetMovementSpeed() {
        return movementSpeed;
    }
    
    public float GetMovementDirection() {
        return movementDirection;
    }

    public void SetMovementDirection(float movementDirectionIn) {
        movementDirection = movementDirectionIn;
    }

    public float GetJumpForce() {
        return jumpForce;
    }

    public Rigidbody2D GetRigidBody() {
        return rb;
    }

    public float GetKnockBackForce() {
        return knockBackForce;
    }

    public void ReverseMovementDirection() {
        movementDirection *= -1;
    }
}
