using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumperMovement : MonoBehaviour
{
    [SerializeField] private float jumpTiming = 5f;
    private Enemy enemy;

    private void Start()
    {
        enemy = transform.GetComponent<Enemy>() as Enemy; 
        StartCoroutine(nameof(RandomizedJump));
    }

    private void FixedUpdate()
    {
        Move();
    }
     
    private IEnumerator RandomizedJump()
    {
        while(true)
        {
            yield return new WaitForSeconds(jumpTiming);  
            enemy.SetMovementDirection( Random.Range(-1.0f, 1.0f));
            Jump();
        }
    
    }

    private void Jump() {

        if(!Mathf.Approximately(0, enemy.GetMovementDirection())) {
            transform.rotation = enemy.GetMovementDirection() < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }

        if(Mathf.Abs(enemy.GetRigidBody().velocity.y) < 0.001f) {
            enemy.GetRigidBody().AddForce(new Vector2(0, enemy.GetJumpForce()), ForceMode2D.Impulse);
        }
    }

    private void Move() {
        if(Mathf.Abs(enemy.GetRigidBody().velocity.y) > 0.001f) {
            enemy.GetRigidBody().velocity = new Vector2 (enemy.GetMovementDirection() * enemy.GetMovementSpeed(), enemy.GetRigidBody().velocity.y);
        }
    }
}
