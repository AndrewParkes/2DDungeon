using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    [SerializeField]
    private float MovementSpeed = 5;
    
    [SerializeField]
    private float jumpForce = 8f;

    [SerializeField]
    private float counterJumpForce = 16f;

    private Vector2 counterJump;

    private Rigidbody2D rb;
    public Animator animator;
    bool canMove = true;

    float blockInputTime = 0;

    float movement;
    bool jumpKeyHeld = false;
    bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        counterJump = Vector2.down * counterJumpForce * rb.mass;
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal"); //left and right controll) {
        //jump = Input.GetButtonDown("Jump");   
        if(Input.GetButtonDown("Jump"))
        {
            jumpKeyHeld = true;
            if(Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                isJumping = true;
                rb.AddForce(Vector2.up * jumpForce * rb.mass, ForceMode2D.Impulse);
            }
        }
        else if(Input.GetButtonUp("Jump"))
        {
            jumpKeyHeld = false;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move() {
        if(canMove) {
            rb.velocity = new Vector2 (movement * MovementSpeed, rb.velocity.y);

            if(!Mathf.Approximately(0, movement)) { // Flip direction
                transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            }

            /*if(jump && Mathf.Abs(rb.velocity.y) < 0.001f) { // jump controll
            Debug.Log("Jump");
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            } else if (!jump && Mathf.Abs(rb.velocity.y) > 0.001f) {
                Vector3 dampeningDirection = rb.velocity.normalized * -1.0f;
                rb.AddForce( dampeningDirection * dampeningRate);
                Debug.Log("Reduce Jump");
            }*/

            if(isJumping)
            {
                if(!jumpKeyHeld && Vector2.Dot(rb.velocity, Vector2.up) > 0)
                {
                    rb.AddForce(counterJump * rb.mass);
                }
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
