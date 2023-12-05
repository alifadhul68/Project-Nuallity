using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // TODO: Add logic for player taking damage, e.g., play a hurt animation

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // TODO: Implement player death logic, e.g., restart the level
        Debug.Log("Player died");
    }
}