using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private float health = 120f;

    public void DealDamage(float damage) {
        health -= damage;
    }

    public float GetHealth() {
        return health;
    }

    public void SetHealth(float healthIn) {
        health = healthIn;
    }
}
