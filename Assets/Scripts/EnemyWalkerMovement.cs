using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkerMovement : MonoBehaviour
{
    [SerializeField]
    private float jumpTiming = 5f;
    Enemy enemy;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    LayerMask enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.GetComponent<Enemy>() as Enemy; 
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
    }

    void Move() {
        //transform.position += new Vector3(enemy.GetMovementDirection(), 0, 0) * Time.deltaTime * enemy.GetMovementSpeed();
        enemy.GetRigidBody().velocity = new Vector2 (enemy.GetMovementDirection() * enemy.GetMovementSpeed(), enemy.GetRigidBody().velocity.y);
        CheckForTurnAround();
    }

    void CheckForTurnAround() {
        
        
        if ( Physics2D.Raycast(new Vector2(transform.position.x + 0.4f * enemy.GetMovementDirection(), transform.position.y), Vector2.right * enemy.GetMovementDirection(), 0.1f, enemyLayer) || // enemy infront
            Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.45f), Vector2.right * enemy.GetMovementDirection(), 0.5f, groundLayer) || // ground infront
            !Physics2D.Raycast(new Vector2(transform.position.x + 0.5f * enemy.GetMovementDirection(), transform.position.y), Vector2.down, 1f, groundLayer)) { // ledge infront
            transform.rotation = (enemy.GetMovementDirection() == 1) ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            enemy.ReverseMovementDirection();
            
        }
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.45f), Vector2.right, Color.green);
            Debug.DrawRay(new Vector2(transform.position.x + 0.5f, transform.position.y), Vector2.down * 0.5f, Color.green);
        
    }
}
