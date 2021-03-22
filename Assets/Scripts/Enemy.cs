using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    float contactDamage = 1f;
    [SerializeField]
    float attackDamage = 20f;
    [SerializeField]
    private float movementSpeed = 4f;
    [SerializeField]
    private float movementDirection = 0f;
    [SerializeField]
    private float jumpForce = 7f;

    Health health;

    private Rigidbody2D rb;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        health.SetHealth(100);
    }

    // Update is called once per frame
    void Update()
    {
        /*animator.SetFloat("XMovement", Mathf.Abs(movementDirection));
        animator.SetFloat("YMovement", rb.velocity.y);*/
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Projectile")
        {
            Projectile projectile = collider.transform.GetComponent<Projectile>();
            if (projectile != null)
            {
                Damage(projectile.GetDamage());
            }
        }
    }

    void Damage(float damage) {
        health.DealDamage(damage);
        CheckDead();
    }

    void CheckDead() {
        if(health.GetHealth() < 0) {
            Destroy(gameObject);
        }
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
}