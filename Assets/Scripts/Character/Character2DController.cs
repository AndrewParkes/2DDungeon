using System.Collections;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5;
    
    [SerializeField] private float jumpForce = 8f;

    [SerializeField] private float counterJumpForce = 16f;

    private Vector2 counterJump;

    private Rigidbody2D rb;
    public Animator animator;
    private bool canMove = true;

    private float blockInputTime;

    private float movement;
    private bool jumpKeyHeld;
    private bool isJumping;
    private static readonly int XMovement = Animator.StringToHash("XMovement");
    private static readonly int YMovement = Animator.StringToHash("YMovement");
    private static readonly int Hit = Animator.StringToHash("Hit");

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        counterJump = Vector2.down * counterJumpForce * rb.mass;
    }

    private void Update()
    {
        movement = Input.GetAxis("Horizontal"); //left and right control
           
        if(Input.GetButtonDown("Jump"))
        {
            jumpKeyHeld = true;
            if(Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                isJumping = true;
                rb.AddForce(Vector2.up * (jumpForce * rb.mass), ForceMode2D.Impulse);
            }
        }
        else if(Input.GetButtonUp("Jump"))
        {
            jumpKeyHeld = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move() {
        if(canMove) {
            rb.velocity = new Vector2 (movement * movementSpeed, rb.velocity.y);

            if(!Mathf.Approximately(0, movement)) { // Flip direction
                transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            }

            if(isJumping)
            {
                if(!jumpKeyHeld && Vector2.Dot(rb.velocity, Vector2.up) > 0)
                {
                    rb.AddForce(counterJump * rb.mass);
                }
            }

            animator.SetFloat(XMovement, Mathf.Abs(movement));
        }
        animator.SetFloat(YMovement, rb.velocity.y);
    }

    public void BlockMovementForKnockBack(float force) {
        blockInputTime = force/10;
        StartCoroutine(BlockInputCoroutine());
    }

    private IEnumerator BlockInputCoroutine() {
        canMove = false;
        animator.SetBool(Hit, true);
        yield return new WaitForSeconds(blockInputTime);
        animator.SetBool(Hit, false);
        canMove = true;
    }
}
