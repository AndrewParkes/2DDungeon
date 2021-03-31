using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerWeapon weapon;

    private void Update() {
        CheckForAttack();
    }

    private void CheckForAttack() {
        if(Input.GetButtonDown("Fire1")) {
             //Abs as players direction changes and we cant attack backwards
            float directionX = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
            float directionY = Input.GetAxisRaw("Vertical");

            //If no key pressed attack forward otherwise based on X and Y axis
            Vector2 inputDirection = new Vector2(directionY == 0f ? 1f : directionX, directionY); 

            AttackInDirection(inputDirection);
        }
    }

    private void AttackInDirection(Vector2 direction) {
        weapon.Attack(direction);
    }
}
