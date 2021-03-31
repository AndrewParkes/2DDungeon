using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private HealthUI healthUI;

    private float healthMax = 120f;
    private float health = 120f;

    public float GetHealth() {
        return health;
    }

    public float GetHealthMax() {
        return healthMax;
    }

    public void SetHealth(float healthIn) {
        health = healthIn;
    }

    public void UpdateHealth(float amount) {
        health += amount;
        healthUI.UpdateHearts(healthMax, health);
    }
}
