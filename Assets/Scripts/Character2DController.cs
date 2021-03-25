using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    [SerializeField]
    private float MovementSpeed = 5;
    
    [SerializeField]
    private float jumpForce = 8f;

    private Rigidbody2D rb;
    public Animator animator;
    bool canMove = true;

    float blockInputTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move() {
        if(canMove) {
            var movement = Input.GetAxis("Horizontal"); //left and right controll
            rb.velocity = new Vector2 (movement * MovementSpeed, rb.velocity.y);

            if(!Mathf.Approximately(0, movement)) { // Flip direction
                transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            }

            if(Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f) { // jump controll
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
            animator.SetFloat("XMovement", Mathf.Abs(movement));
        }
        animator.SetFloat("YMovement", rb.velocity.y);
    }

    public void BlockMoveMentForKnockBack(float force) {
        blockInputTime = force/10;
        StartCoroutine(BlockInputCoroutine());
    }

    IEnumerator BlockInputCoroutine() {
        canMove = false;
        animator.SetBool("Hit", true);
        yield return new WaitForSeconds(blockInputTime);
        animator.SetBool("Hit", false);
        canMove = true;
    }
}
