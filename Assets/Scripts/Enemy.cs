using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    float contactDamage = 1f;
    [SerializeField]
    float attackDamage = 1f;
    [SerializeField]
    private float movementSpeed = 4f;
    [SerializeField]
    private float movementDirection = 0f;
    [SerializeField]
    private float jumpForce = 7f;

    private Rigidbody2D rb;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*animator.SetFloat("XMovement", Mathf.Abs(movementDirection));
        animator.SetFloat("YMovement", rb.velocity.y);*/
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
