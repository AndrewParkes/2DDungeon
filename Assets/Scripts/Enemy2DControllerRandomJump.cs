using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2DControllerRandomJump : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 4f;

    [SerializeField]
    private float movementDirection = 0f;
    
    [SerializeField]
    private float jumpForce = 7f;

    [SerializeField]
    private float jumpTiming = 5f;

    private Rigidbody2D rb;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("RandomizedJump");
    }

    // Update is called once per frame
    void Update()
    {
      Move();
    }
     
    private IEnumerator RandomizedJump()
    {
        while(true)
        {
            yield return new WaitForSeconds(jumpTiming);  
            movementDirection = Random.Range(-1.0f, 1.0f);
            Jump();
        }
    
    }

    void Jump() {

        if(!Mathf.Approximately(0, movementDirection)) {
            transform.rotation = movementDirection < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }

        if(Mathf.Abs(rb.velocity.y) < 0.001f) {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    void Move() {
        if(Mathf.Abs(rb.velocity.y) > 0.001f) {
            transform.position += new Vector3(movementDirection, 0, 0) * Time.deltaTime * movementSpeed;
        }

        /*animator.SetFloat("XMovement", Mathf.Abs(movementDirection));
        animator.SetFloat("YMovement", rb.velocity.y);*/
    }
}
